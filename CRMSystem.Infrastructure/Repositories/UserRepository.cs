using CRMSystem.Infrastructure.DB;
using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.DTO.Admin;
using CRMSystem.Shared.DTO.Api;
using CRMSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }
        
        public async Task<User> Create(User model)
        {
            if (!await _context.Users.AnyAsync())
                model.SetRole(UserRole.Admin);

            try
            {
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Пользователь с почтой {model.Email} добавлен в БД");
            }
            catch (Exception ex) 
                { Console.WriteLine("Ошибка - " + ex.Message); }
            return model;
        }

        public async Task<User> GetEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetId(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserDto>> GetAll()
        {
            return await _context.Users.Select(u => new UserDto(u.Id, u.Name, u.Role)).ToListAsync();
        }

        public async Task UpdateUserRole(UpdateUserRoleDto userInfo)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userInfo.UserId);
            if (user == null)
                throw new Exception("Пользователя не найдено");

            user.SetRole(userInfo.Role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new Exception("Пользователя не найдено");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
