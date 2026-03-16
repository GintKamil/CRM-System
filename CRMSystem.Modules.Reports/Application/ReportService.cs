using CRMSystem.Shared.DTO.Report;
using CRMSystem.Shared.Entities;

namespace CRMSystem.Modules.Reports.Application
{
    public interface IReportService
    {
        public Task<List<TasksByStatusDto>> GetTasksByStatus();
        public Task<List<TasksByPriorityDto>> GetTasksByPriority();
        public Task<List<TasksByUserDto>> GetTasksByUser();
        public Task<List<TasksByCustomerDto>> GetTasksByCustomer();
        public Task<DashboardStatsDto> GetDashboardStats();
    }
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<List<TasksByStatusDto>> GetTasksByStatus()
        {
            return await _reportRepository.GetTasksByStatus();
        }
        public Task<List<TasksByPriorityDto>> GetTasksByPriority()
        {
            return _reportRepository.GetTasksByPriority();
        }
        public Task<List<TasksByUserDto>> GetTasksByUser()
        {
            return _reportRepository.GetTasksByUser();
        }
        public Task<List<TasksByCustomerDto>> GetTasksByCustomer()
        {
            return _reportRepository.GetTasksByCustomer();
        }
        public async Task<DashboardStatsDto> GetDashboardStats()
        {
            return await _reportRepository.GetDashboardStats();
        }
    }
}
