using CRMSystem.Modules.Auth.Domain.Entities;
using Microsoft.VisualBasic;
using System.Data;

namespace CRMSystem.Shared.Entities
{
    public class TaskM : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }

        // Связь с клиентом
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        
        // Кто выполняет
        public User AssignedTo { get; set; }
        public int AssignedToId { get; set; }
        
        // Кто создал
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public TaskM() { }
        public TaskM(string title, string description, TaskStatus status, TaskPriority priority, DateTime dueDate, int customerId, int assignedToId, int createdById, DateTime createdAt)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            DueDate = dueDate;
            CustomerId = customerId;
            AssignedToId = assignedToId;
            CreatedById = createdById;
            CreatedAt = createdAt;
        }
    }

    public enum TaskStatus
    {
        New = 1, 
        AtWork = 2, 
        Completed = 3
    }

    public enum TaskPriority
    {
        Low = 1, 
        Medium = 2, 
        High = 3
    }
}
