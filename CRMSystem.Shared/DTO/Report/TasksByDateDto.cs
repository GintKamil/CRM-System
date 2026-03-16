namespace CRMSystem.Shared.DTO.Report
{
    public class TasksByDateDto
    {
        public DateTime Date { get; set; }
        public int TasksCount { get; set; }
        public TasksByDateDto(DateTime date, int tasksCount)
        {
            Date = date;
            TasksCount = tasksCount;
        }
    }
}
