using EduConnect.Core.DTOs;
using EduConnect.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var userResult = await _userService.CreateUserAsync(createUserDto);

            return userResult.Success
           ? Ok(userResult.Message)
           : BadRequest(userResult.Message);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
             var getUser =await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            if (getUser.Success)
            {
                return Ok(getUser.Data);
            }
            return BadRequest(getUser.Message);

        }
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDto)
        {
            var result = await _userService.AssignRoleToUserAsync(assignRoleDto.UserEmail, assignRoleDto.Role);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

    }
}
