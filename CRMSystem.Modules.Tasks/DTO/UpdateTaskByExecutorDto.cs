using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Modules.Tasks.DTO
{
    public class UpdateTaskByExecutorDto
    {
        public int TaskId { get; set; }
        public int CurrentUserId { get; set; }
        public CRMSystem.Shared.Entities.TaskStatus Status { get ; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
    }
}
