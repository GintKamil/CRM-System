namespace CRMSystem.Shared.DTO.Report
{
    public class DashboardStatsDto
    {
        public int TotalTasks { get; private set; }

        public int CompletedTasks { get; private set; }

        public int InProgressTasks { get; private set; }

        public int NewTasks { get; private set; }

        public int TotalCustomers { get; private set; }

        public int TotalUsers { get; private set; }

        public DashboardStatsDto(
            int totalTasks,
            int completedTasks,
            int inProgressTasks,
            int newTasks,
            int totalCustomers,
            int totalUsers)
        {
            TotalTasks = totalTasks;
            CompletedTasks = completedTasks;
            InProgressTasks = inProgressTasks;
            NewTasks = newTasks;
            TotalCustomers = totalCustomers;
            TotalUsers = totalUsers;
        }
    }
}
