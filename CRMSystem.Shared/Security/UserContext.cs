using CRMSystem.Modules.Auth.Domain.Entities;
using System.Data;
using System.Security.Claims;

namespace CRMSystem.Shared.Security
{
    public class UserContext
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Role { get; }

        public bool IsAdmin => Role == Roles.Admin;
        public bool IsManager => Role == Roles.Manager;
        public bool IsEmployees => Role == Roles.Employees;

        private UserContext(int id, string name, string email, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }

        public static UserContext FromClaims(ClaimsPrincipal user)
        {
            if(user?.Identity?.IsAuthenticated == false)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var id = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var name = user.FindFirst(ClaimTypes.Name)!.Value;
            var email = user.FindFirst(ClaimTypes.Email)!.Value;
            var role = user.FindFirst(ClaimTypes.Role)!.Value;
            return new UserContext(id, name, email, role);
        }
    }
}
