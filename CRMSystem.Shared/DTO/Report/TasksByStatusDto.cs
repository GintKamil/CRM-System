using CRMSystem.Shared.Entities;

namespace CRMSystem.Shared.DTO.Report
{
    public class TasksByStatusDto
    {
        public Entities.TaskStatus Status { get; set; }
        public int Count { get; set; }
        public TasksByStatusDto(Entities.TaskStatus status, int count)
        {
            Status = status;
            Count = count;
        }
    }
}
