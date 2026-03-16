using CRMSystem.Modules.Auth.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Shared.Entities
{
    public class TaskCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        public TaskStatus Status { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        public int CustomerId { get; set; }
        public int AssignedToId { get; set; }


        public List<SelectListItem> Customers { get; set; } = new();
        public List<SelectListItem> Users { get; set; } = new();
        
        public TaskCreateViewModel() { }

        public TaskCreateViewModel(string title, string description, TaskStatus status, TaskPriority priority, DateTime dueDate, int customerId, int assignedToId)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            DueDate = dueDate;
            CustomerId = customerId;
            AssignedToId = assignedToId;
        }
    }
}
