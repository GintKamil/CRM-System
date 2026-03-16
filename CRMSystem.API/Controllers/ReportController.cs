using CRMSystem.Modules.Reports.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> Stats()
        {
            var stats = await _reportService.GetDashboardStats();
            return Ok(stats);
        }

        [HttpGet("status")]
        public async Task<IActionResult> Status()
        {
            var tasksByStatus = await _reportService.GetTasksByStatus();
            return Ok(tasksByStatus);
        }

        [HttpGet("priority")]
        public async Task<IActionResult> Priority()
        {
            var tasksByPriority = await _reportService.GetTasksByPriority();
            return Ok(tasksByPriority);
        }

        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            var tasksByUser = await _reportService.GetTasksByUser();
            return Ok(tasksByUser);
        }

        [HttpGet("customer")]
        public async Task<IActionResult> Customer()
        {
            var tasksByCustomer = await _reportService.GetTasksByCustomer();
            return Ok(tasksByCustomer);
        }
    }
}
