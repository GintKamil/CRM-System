using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.DTO.Admin;
using CRMSystem.Shared.DTO.Api;

namespace CRMSystem.Shared.Entities
{
    public interface IUserRepository
    {
        public Task<User> Create(User model);
        public Task<User> GetEmail(string email);
        public Task<User> GetId(int id);
        public Task<List<UserDto>> GetAll();
        public Task UpdateUserRole(UpdateUserRoleDto userInfo);
        public Task DeleteUser(int userId);
    }
}
