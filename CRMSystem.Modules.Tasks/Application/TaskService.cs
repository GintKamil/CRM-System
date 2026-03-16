using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Modules.Tasks.DTO;
using CRMSystem.Modules.Tasks.Model;
using CRMSystem.Shared.DTO.Api;
using CRMSystem.Shared.Entities;
using CRMSystem.Shared.Security;
using System.Reflection.Metadata.Ecma335;

namespace CRMSystem.Modules.Tasks.Application
{
    public interface ITaskService
    {
        public Task<TaskM> Create(TaskM task);
        public Task<TaskM> Delete(int id);
        public Task<TaskM> GetTask(int id);
        public Task<List<TaskM>> GetAllForId(int id, string role);
        public Task<TaskIndexData> GetTasksForIndex(UserContext user);
        public Task UpdateByExecutor(UpdateTaskByExecutorDto dto);
        public Task UpdateByManager(UpdateTaskByManagerDto dto);
        public TaskM TaskConversion(TaskCreateViewModel taskData, int customerId, int assignedToId, int createdById);
        public TaskM TaskConversion(TaskCreateApiDto taskData, int userId);
        public TaskViewModel TaskConversion(TaskM taskData);
        public UpdateTaskByExecutorDto TaskConvEditExecutor(TaskM taskData);
        public UpdateTaskByManagerDto TaskConvEditManager(TaskM taskData);
        public TaskApiDto MapToDto(TaskM task);
    }

    public class TaskService : ITaskService
    {
        public ITaskRepository _context;
        public TaskService(ITaskRepository context) {
            _context = context; 
        }

        public async Task<TaskM> Create(TaskM task)
        {
            return await _context.CreateTask(task);
        }

        public async Task<TaskM> Delete(int id) {
            return await _context.DeleteTask(id);
        }

        public async Task<TaskM> GetTask(int id)
        {
            return await _context.GetTask(id);
        }

        public async Task<List<TaskM>> GetAllForId(int id, string role)
        {
            if (role == UserRole.Admin.ToString())
                return await _context.GetAllTask();

            return await _context.GetAllTaskById(id);
        }

        public async Task UpdateByExecutor(UpdateTaskByExecutorDto dto)
        {
            var task = await _context.GetTask(dto.TaskId);

            if (task.AssignedToId != dto.CurrentUserId)
                throw new Exception();

            task.Status = dto.Status;
            task.Description = dto.Description;

            await _context.UpdateTask(task);
        }

        public async Task UpdateByManager(UpdateTaskByManagerDto dto)
        {
            var task = await _context.GetTask(dto.TaskId);
            task.Title = dto.Title;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate.ToUniversalTime();
            task.AssignedToId = dto.AssignedToId;
            await _context.UpdateTask(task);
        }

        public TaskM TaskConversion(TaskCreateViewModel taskData, int customerId, int assignedToId, int createdById)
        {
            return new TaskM(
                taskData.Title,
                taskData.Description,
                taskData.Status,
                taskData.Priority,
                taskData.DueDate.ToUniversalTime(),
                customerId,
                assignedToId,
                createdById,
                DateTime.UtcNow
                );
        }
        public TaskM TaskConversion(TaskCreateApiDto taskData, int userId)
        {
            return new TaskM(
                taskData.Title,
                taskData.Description,
                taskData.Status,
                taskData.Priority,
                taskData.DueDate.ToUniversalTime(),
                taskData.CustomerId,
                taskData.AssignedToId,
                userId,
                DateTime.UtcNow
                );
        }

        public TaskViewModel TaskConversion(TaskM taskData)
        {
            return new TaskViewModel(
                taskData.Title,
                taskData.Description,
                taskData.Status,
                taskData.Priority,
                taskData.DueDate,
                taskData.Customer,
                taskData.AssignedTo
                );
        }
        public async Task<TaskIndexData> GetTasksForIndex(UserContext user)
        {
            if (user.IsAdmin)
            {
                return new TaskIndexData
                {
                    AllTasks = await _context.GetAllTask(),
                    AssignedToMe = await _context.GetAssignedByUser(user.Id)
                };
            }

            if (user.IsManager)
            {
                return new TaskIndexData
                {
                    CreatedByMe = await _context.GetCreatedByUser(user.Id),
                    AssignedToMe = await _context.GetAssignedByUser(user.Id)
                };
            }

            return new TaskIndexData
            {
                AssignedToMe = await _context.GetAssignedByUser(user.Id)
            };
        }
        public UpdateTaskByExecutorDto TaskConvEditExecutor(TaskM taskData)
        {
            return new UpdateTaskByExecutorDto()
            {
                Status = taskData.Status,
                Description = taskData.Description
            };
        }
        public UpdateTaskByManagerDto TaskConvEditManager(TaskM taskData)
        {
            return new UpdateTaskByManagerDto()
            {
                Title = taskData.Title,
                Priority = taskData.Priority,
                DueDate = taskData.DueDate,
                AssignedToId = taskData.AssignedToId
            };
        }
        public TaskApiDto MapToDto(TaskM task)
        {
            return new TaskApiDto(
                task.Title,
                task.Description,
                task.Status,
                task.Priority,
                task.DueDate,
                new CustomerApiDto(
                    task.Customer.Id,
                    task.Customer.Name,
                    task.Customer.Email,
                    task.Customer.Phone,
                    task.Customer.Company,
                    task.Customer.Status,
                    task.Customer.CreatedAt,
                    new UserDto(
                        task.Customer.CreatedBy.Id,
                        task.Customer.CreatedBy.Name,
                        task.Customer.CreatedBy.Role
                    )
                ),
                new UserDto(
                    task.AssignedTo.Id,
                    task.AssignedTo.Name,
                    task.AssignedTo.Role
                ),
                new UserDto(
                    task.CreatedBy.Id,
                    task.CreatedBy.Name,
                    task.CreatedBy.Role
                )
            );
        }
        public TaskM TaskConvApi(TaskCreateApiDto taskCreateApiDto)
        {
            return null;
        }
    }
}
