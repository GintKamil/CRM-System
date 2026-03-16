using CRMSystem.Infrastructure.DB;
using CRMSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataBaseContext _context;

        public TaskRepository(DataBaseContext context)
        {
            _context = context;
        }
        
        public async Task<TaskM> CreateTask(TaskM task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка - " + ex.ToString());
            }
            return task;
        }

        public async Task<TaskM> UpdateTask(TaskM task)
        {
            Console.WriteLine("Репозиторий1");
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            Console.WriteLine("Репозиторий2");
            return task;
        }
        public async Task<TaskM> GetTask(int id)
        {
            TaskM? task = await _context.Tasks
                .Include(t => t.Customer)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }
        public async Task<List<TaskM>> GetAllTask()
        {
            return await _context.Tasks
                .Include(c => c.Customer)
                .Include(c => c.AssignedTo)
                .Include(c => c.CreatedBy)
                .ToListAsync();
        }
        public async Task<List<TaskM>> GetAllTaskById(int id)
        {
            return await _context.Tasks
                .Where(t => t.CreatedById == id || t.AssignedToId == id)
                .Select(t => new TaskM(t.Title, t.Description, t.Status, t.Priority, t.DueDate, t.CustomerId, t.AssignedToId, t.CreatedById, t.CreatedAt))
                .ToListAsync();
        }
        public async Task<TaskM> DeleteTask(int id)
        {
            TaskM? task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<List<TaskM>> GetCreatedByUser(int userId)
        {
            return await _context.Tasks
                .Include(t => t.Customer)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .Where(t => t.CreatedById == userId)
                .ToListAsync();
        }
        public async Task<List<TaskM>> GetAssignedByUser(int userId)
        {
            return await _context.Tasks
                .Include(t => t.Customer)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .Where(t => t.AssignedToId == userId)
                .ToListAsync();
        }
    }
}
