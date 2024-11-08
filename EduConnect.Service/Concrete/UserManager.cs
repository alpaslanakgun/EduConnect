﻿using AutoMapper;
using EduConnect.Core.DTOs;
using EduConnect.Core.Models;
using EduConnect.Core.Results.Abstract;
using EduConnect.Core.Results.ComplexType;
using EduConnect.Services.Abstract;
using EduConnect.Services.Common;
using Microsoft.AspNetCore.Identity;


namespace EduConnect.Services.Concrete
{
    public class UserManagerService : IUserService
    {
        private readonly UserManager<User> _identityUserManager; 
        private readonly IMapper _mapper;

        public UserManagerService(UserManager<User> userManager, IMapper mapper)
        {
            _identityUserManager = userManager;
            _mapper = mapper;
        }
        public async Task<IDataResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName
            };

            var result = await _identityUserManager.CreateAsync(user, createUserDto.Password);

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
            var user = await _identityUserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new ErrorDataResult<UserDto>(UserAuthenticationMessageConstant.UserIdNotFound);
            }
            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto);
        }
        public async Task<IDataResult<UserDto>> AssignRoleToUserAsync(string userEmail, string role)
        {
            var user = await _identityUserManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return new ErrorDataResult<UserDto>(UserAuthenticationMessageConstant.UserNotFound);
            }

            var result = await _identityUserManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return new ErrorDataResult<UserDto>(string.Join(", ", errors));
            }

            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto, UserAuthenticationMessageConstant.RoleAdded);
        }
        public async Task<IDataResult<List<string>>> GetUserRolesAsync(string userEmail)
        {
            var user = await _identityUserManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return new ErrorDataResult<List<string>>(UserAuthenticationMessageConstant.UserNotFound);
            }

            var roles = await _identityUserManager.GetRolesAsync(user);
            return new SuccessDataResult<List<string>>(roles.ToList());
        }

    }
}
