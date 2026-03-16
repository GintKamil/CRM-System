using CRMSystem.Modules.Auth.Domain.Entities;
using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Modules.Tasks.Model
{
    public class TaskViewModel
    {
        [Required]
        public string Title { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        public CRMSystem.Shared.Entities.TaskStatus Status { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public User AssignedTo { get; set; }

        public TaskViewModel() { }

        public TaskViewModel(string title, string description, CRMSystem.Shared.Entities.TaskStatus status, TaskPriority priority, DateTime dueDate, Customer customer, User assignedTo)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            DueDate = dueDate;
            Customer = customer;
            AssignedTo = assignedTo;
        }
    }
}
