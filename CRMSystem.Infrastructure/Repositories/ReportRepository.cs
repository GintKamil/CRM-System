using CRMSystem.Infrastructure.DB;
using CRMSystem.Shared.DTO.Report;
using CRMSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CRMSystem.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataBaseContext _context;
        public ReportRepository(DataBaseContext context) 
        {
            _context = context;
        }

        public async Task<List<TasksByStatusDto>> GetTasksByStatus()
        {
            return await _context.Tasks
                .GroupBy(t => t.Status)
                .Select(g => new TasksByStatusDto(
                        g.Key,
                        g.Count()
                    ))
                .ToListAsync();
        }
        public async Task<List<TasksByPriorityDto>> GetTasksByPriority()
        {
            return await _context.Tasks
                .GroupBy(t => t.Priority)
                .Select(g => new TasksByPriorityDto(
                        g.Key,
                        g.Count()
                    ))
                .ToListAsync();
        }
        public async Task<List<TasksByUserDto>> GetTasksByUser()
        {
            return await _context.Tasks
                .Include(t => t.AssignedTo)
                .GroupBy(t => t.AssignedToId)
                .Select(g => new TasksByUserDto(
                        new Shared.DTO.Api.UserDto(
                                g.First().AssignedTo.Id,
                                g.First().AssignedTo.Name,
                                g.First().AssignedTo.Role
                            ),
                        g.Count()
                    ))
                .ToListAsync();
        }
        public async Task<List<TasksByCustomerDto>> GetTasksByCustomer()
        {
            return await _context.Tasks
                .Include(t => t.Customer)
                .GroupBy(t => t.CustomerId)
                .Select(g => new TasksByCustomerDto(
                        new Shared.DTO.Api.CustomerApiDto(
                            g.First().Customer.Id,
                            g.First().Customer.Name,
                            g.First().Customer.Email,
                            g.First().Customer.Phone,
                            g.First().Customer.Company,
                            g.First().Customer.Status,
                            g.First().Customer.CreatedAt,
                            new Shared.DTO.Api.UserDto(
                                g.First().Customer.CreatedBy.Id,
                                g.First().Customer.CreatedBy.Name,
                                g.First().Customer.CreatedBy.Role
                            )
                            ),
                        g.Count()
                    ))
                .ToListAsync();
        }
        public async Task<DashboardStatsDto> GetDashboardStats()
        {
            var totalTasks = await _context.Tasks.CountAsync();
            
            var completedTasks = await _context.Tasks.CountAsync(t => t.Status == Shared.Entities.TaskStatus.Completed);
            var inProgressTasks = await _context.Tasks.CountAsync(t => t.Status == Shared.Entities.TaskStatus.AtWork);
            var newTasks = await _context.Tasks.CountAsync(t => t.Status == Shared.Entities.TaskStatus.New);

            var totalCustomers = await _context.Customers.CountAsync();
            
            var totalUsers = await _context.Users.CountAsync();

            return new DashboardStatsDto(
                totalTasks,
                completedTasks,
                inProgressTasks,
                newTasks,
                totalCustomers,
                totalUsers
                );
        }
    }
}
