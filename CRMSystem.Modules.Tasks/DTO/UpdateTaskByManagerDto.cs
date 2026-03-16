using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRMSystem.Modules.Tasks.DTO
{
    public class UpdateTaskByManagerDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int AssignedToId { get; set; }

        public List<SelectListItem> Users { get; set; } = new();
    }
}
