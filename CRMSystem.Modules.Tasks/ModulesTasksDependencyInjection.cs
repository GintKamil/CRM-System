using CRMSystem.Modules.Tasks.Application;

namespace CRMSystem.Modules.Tasks
{
    public static class ModulesTasksDependencyInjection
    {
        public static IServiceCollection AddTaskModule(
        this IServiceCollection services)
        {
            services.AddScoped(typeof(ITaskService), typeof(TaskService));
            return services;
        }
    }
}
