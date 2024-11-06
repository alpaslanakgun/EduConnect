using EduConnect.Core.DTOs;
using EduConnect.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        //api/auth/
        [HttpPost("create-token")]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.CreateTokenAsync(loginDto);

            if (result.Success)
            {
                return Ok(result.Data); 
            }

            return BadRequest(result.Message); 
        }

        [HttpPost("create-token-by-client")]
        public async Task<IActionResult> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.CreateTokenByClient(clientLoginDto);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(refreshTokenDto.Token))
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.Token);

            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result.Message);
        }

        [HttpPost("create-token-by-refresh-token")]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(refreshTokenDto.Token))
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

    }
}
