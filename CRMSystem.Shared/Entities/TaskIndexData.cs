namespace CRMSystem.Shared.Entities
{
    public class TaskIndexData
    {
        public List<TaskM> AllTasks { get; set; } = new();
        public List<TaskM> CreatedByMe { get; set; } = new();
        public List<TaskM> AssignedToMe { get; set; } = new();
    }
}
