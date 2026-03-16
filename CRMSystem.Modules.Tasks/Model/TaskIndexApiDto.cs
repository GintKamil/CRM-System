using CRMSystem.Shared.DTO.Api;

namespace CRMSystem.Modules.Tasks.Model
{
    public class TaskIndexApiDto
    {
        public List<TaskApiDto> AllTasks { get; set; } = new();
        public List<TaskApiDto> AssignedToMe { get; set; } = new();
        public List<TaskApiDto> CreatedByMe { get; set; } = new();
    }
}
