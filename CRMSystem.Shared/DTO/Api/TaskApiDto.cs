using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.Entities;

namespace CRMSystem.Shared.DTO.Api
{
    public class TaskApiDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Entities.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public CustomerApiDto Customer { get; private set; }
        public UserDto AssignedTo { get; private set; }
        public UserDto CreatedBy { get; private set; }

        public TaskApiDto(string title, string description, Entities.TaskStatus status, TaskPriority priority, DateTime dueData, CustomerApiDto customer, UserDto assignedTo, UserDto createdBy)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            DueDate = dueData;
            Customer = customer;
            AssignedTo = assignedTo;
            CreatedBy = createdBy;
            CreatedBy = createdBy;
        }
    }
}
