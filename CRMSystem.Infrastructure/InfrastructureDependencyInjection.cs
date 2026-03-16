using CRMSystem.Infrastructure.DB;
using CRMSystem.Infrastructure.Repositories;
using CRMSystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        {
            string connection = "User ID=asptest;Password=1234;Host=localhost;Port=5432;Database=CRMSystem;Pooling=true";
            services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(connection));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            return services;
        }
    }
}
