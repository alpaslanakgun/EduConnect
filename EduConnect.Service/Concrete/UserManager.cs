using AutoMapper;
using EduConnect.Core.DTOs;
using EduConnect.Core.Models;
using EduConnect.Core.Results.Abstract;
using EduConnect.Core.Results.ComplexType;
using EduConnect.Services.Abstract;
using EduConnect.Services.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Concrete
{
    public class UserManager: IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserManager(UserManager<User> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IDataResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
        
            var user = new User
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName
            };

       
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return new ErrorDataResult<UserDto>(string.Join(", ", errors)); 
            }

            var userDto = _mapper.Map<UserDto>(user);

            return new SuccessDataResult<UserDto>(userDto, UserAuthenticationMessageConstant.UserSuccessful); 
        }


        public async Task<IDataResult<UserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new ErrorDataResult<UserDto>(UserAuthenticationMessageConstant.UserIdNotFound); 
            }
            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto);
        }

    }
}
