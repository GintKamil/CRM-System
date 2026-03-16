using CRMSystem.Shared.DTO.Report;

namespace CRMSystem.Shared.Entities
{
    public interface IReportRepository
    {
        public Task<List<TasksByStatusDto>> GetTasksByStatus();
        public Task<List<TasksByPriorityDto>> GetTasksByPriority();
        public Task<List<TasksByUserDto>> GetTasksByUser();
        public Task<List<TasksByCustomerDto>> GetTasksByCustomer();
        public Task<DashboardStatsDto> GetDashboardStats();
    }
}
