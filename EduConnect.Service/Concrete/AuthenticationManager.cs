using AutoMapper;
using EduConnect.Core.Configuration;
using EduConnect.Core.DTOs;
using EduConnect.Core.Models;
using EduConnect.Core.Repositories;
using EduConnect.Core.Results.Abstract;
using EduConnect.Core.Results.ComplexType;
using EduConnect.Core.UnitOfWorks;
using EduConnect.Services.Abstract;
using EduConnect.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduConnect.Services.Concrete
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager; 
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;
        private readonly IMapper _mapper;

        public AuthenticationManager(IOptions<List<Client>> optionsClient,
            ITokenService tokenService,
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IGenericRepository<UserRefreshToken> userRefreshTokenService,
            IMapper mapper)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
            _mapper = mapper;
        }

        public async Task<IDataResult<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return new ErrorDataResult<TokenDto>(UserAuthenticationMessageConstant.EmailOrPasswordWrong);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return new ErrorDataResult<TokenDto>(UserAuthenticationMessageConstant.EmailOrPasswordWrong);

            // Kullanıcının rollerini al
            var roles = await _userManager.GetRolesAsync(user);

            // UserDto'yu oluştururken rollerini ekle
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = roles.ToList(); // Roller burada atanır

            var tokenDto = _tokenService.CreateToken(userDto); // Token burada oluşturulur

            var userRefreshToken = await _userRefreshTokenService
                .Where(x => x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Code = tokenDto.RefreshToken,
                    Expiration = tokenDto.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.Code = tokenDto.RefreshToken;
                userRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<TokenDto>(tokenDto);
        }


        public async Task<IDataResult<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x =>
                x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null) return new ErrorDataResult<ClientTokenDto>(UserAuthenticationMessageConstant.ClientIdOrSecretNotFound);

            var token = _tokenService.CreateTokenByClient(client);

            return new SuccessDataResult<ClientTokenDto>(token);
        }

        public async Task<IDataResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService
                .Where(x => x.Code == refreshToken)
                .SingleOrDefaultAsync();

            if (existRefreshToken == null)
                return new ErrorDataResult<TokenDto>(AuthenticationMessageConstant.RefreshTokenNotFound);
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
                return new ErrorDataResult<TokenDto>(AuthenticationMessageConstant.UserIdNotFound);
            var userDto = _mapper.Map<UserDto>(user);
            var tokenDto = _tokenService.CreateToken(userDto);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<TokenDto>(tokenDto);
        }



        public async Task<IDataResult<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken  =await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null) return new ErrorDataResult<NoDataDto>("Refresh token not found");

            await _userRefreshTokenService.DeleteAsync(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<NoDataDto>();
        }
    }
}
