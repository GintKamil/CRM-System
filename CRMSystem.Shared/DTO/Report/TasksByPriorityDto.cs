using CRMSystem.Shared.Entities;

namespace CRMSystem.Shared.DTO.Report
{
    public class TasksByPriorityDto
    {
        public TaskPriority Priority { get; set; }
        public int Count { get; set; }
        public TasksByPriorityDto(TaskPriority priority, int count)
        {
            Priority = priority;
            Count = count;
        }
    }
}
