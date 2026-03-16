using CRMSystem.Modules.Auth.Domain.Entities;

namespace CRMSystem.Shared.DTO.Api
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }

        public UserDto(int id, string name, UserRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
