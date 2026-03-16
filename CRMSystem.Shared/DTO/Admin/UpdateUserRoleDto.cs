using CRMSystem.Modules.Auth.Domain.Entities;

namespace CRMSystem.Shared.DTO.Admin
{
    public class UpdateUserRoleDto
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
