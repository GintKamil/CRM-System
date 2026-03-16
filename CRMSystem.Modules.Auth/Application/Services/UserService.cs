using CRMSystem.Infrastructure.DB;
using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Modules.Auth.Infrastructure.Security;
using CRMSystem.Shared.DTO.Admin;
using CRMSystem.Shared.DTO.Api;
using CRMSystem.Shared.Entities;

namespace CRMSystem.Modules.Auth.Application.Services
{
    public interface IUserService
    {
        public Task<User> Create(User model);
        public Task<User> GetEmail(string email);
        public Task<User> GetId(int id);
        public Task<List<UserDto>> GetAll();
        public Task<bool> ValidateUser(string email, string password);
        public Task<string> PasswordHashing(string password);
        public UserDto UserConv(User user);
        public Task UpdateUserRole(UpdateUserRoleDto userInfo);
        public Task DeleteUser(int userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _context;
        private readonly IPasswordHashingService _passwordHashingService;

        public UserService(IUserRepository context, IPasswordHashingService passwordHashingService)
        {
            _context = context;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<User> Create(User model)
        {
            model.SetRole(UserRole.Employees);
            await _context.Create(model);
            return model;
        }

        public async Task<User> GetEmail(string email)
        {
            return await _context.GetEmail(email);
        }
        public async Task<User> GetId(int id)
        {
            return await _context.GetId(id);
        }
        public async Task<List<UserDto>> GetAll()
        {
            return await _context.GetAll();
        }
        public async Task<bool> ValidateUser(string email, string password)
        {
            var user = await GetEmail(email);

            if (user != null)
            {
                if(_passwordHashingService.PasswordCheck(user.Password, password))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<string> PasswordHashing(string password)
        {
            return _passwordHashingService.PasswordHashing(password);
        }

        public UserDto UserConv(User user)
        {
            return new UserDto(
                user.Id,
                user.Name,
                user.Role
                );
        }
        public async Task UpdateUserRole(UpdateUserRoleDto userInfo)
        {
            await _context.UpdateUserRole(userInfo);
        }
        public async Task DeleteUser(int userId)
        {
            await _context.DeleteUser(userId);
        }
    }
}
