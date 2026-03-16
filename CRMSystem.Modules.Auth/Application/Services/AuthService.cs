using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRMSystem.Modules.Auth.Application.Services
{
    public interface IAuthService
    {
        Task<ClaimsIdentity> AuthenticateAsync(string email);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(string email)
        {
            var user = await _userRepository.GetEmail(email);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                );
        }
    }
}
