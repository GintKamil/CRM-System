using CRMSystem.Modules.Auth.Application.DTO;
using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (await _userService.ValidateUser(dto.Email, dto.Password)) 
            {
                var user = await _userService.GetEmail(dto.Email);

                var token = _jwtService.GenerateToken(user);

                return Ok(
                    new
                    {
                        access_token = token,
                    }
                );
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User model)
        {
            Console.WriteLine("ok");
            if (ModelState.IsValid)
            {
                model.Password = await _userService.PasswordHashing(model.Password);

                await _userService.Create(model);

                var token = _jwtService.GenerateToken(model);

                return Ok(
                    new
                    {
                        access_token = token
                    }
                );
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> UserInfo()
        {
            var user = UserContext.FromClaims(User);
            return Ok(user);
        }
    }
}
