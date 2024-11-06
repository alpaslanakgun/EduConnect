using EduConnect.Core.DTOs;
using EduConnect.Core.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<IDataResult<UserDto>> GetUserByNameAsync(string userName);
        Task<IDataResult<UserDto>> AssignRoleToUserAsync(string userEmail, string role);
        Task<IDataResult<List<string>>> GetUserRolesAsync(string userEmail);
    }
}
