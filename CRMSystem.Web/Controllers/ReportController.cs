using CRMSystem.Modules.Reports.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService) 
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var stats = await _reportService.GetDashboardStats();
            return View(stats);
        }

        public async Task<IActionResult> ByStatus()
        {
            var stats = await _reportService.GetTasksByStatus();
            return View(stats);
        }

        public async Task<IActionResult> ByPriority()
        {
            var stats = await _reportService.GetTasksByPriority();
            return View(stats);
        }

        public async Task<IActionResult> ByUsers()
        {
            var stats = await _reportService.GetTasksByUser();
            return View(stats);
        }
        public async Task<IActionResult> ByCustomers()
        {
            var stats = await _reportService.GetTasksByCustomer();
            return View(stats);
        }

    }
}
