using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRMSystem.Shared.DTO.Api
{
    public class TaskCreateApiDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Entities.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int CustomerId { get; private set; }
        public int AssignedToId { get; private set; }

        public TaskCreateApiDto(string title, string description, Entities.TaskStatus status, TaskPriority priority, DateTime dueData, int customerId, int assignedToId)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            DueDate = dueData;
            CustomerId = customerId;
            AssignedToId = assignedToId;
        }
    }
}
