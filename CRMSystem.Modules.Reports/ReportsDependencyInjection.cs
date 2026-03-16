using CRMSystem.Infrastructure.DB;
using CRMSystem.Infrastructure.Repositories;
using CRMSystem.Modules.Reports.Application;
using CRMSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Modules.Reports
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddReportModules(
            this IServiceCollection services)
        {
            services.AddScoped(typeof(IReportService), typeof(ReportService));
            return services;
        }
    }
}
