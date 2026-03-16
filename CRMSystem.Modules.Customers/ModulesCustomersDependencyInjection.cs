using CRMSystem.Modules.Customers.Application;

namespace CRMSystem.Modules.Customers
{
    public static class ModulesCustomersDependencyInjection
    {
        public static IServiceCollection AddCustomersModule(
        this IServiceCollection services)
        {
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            return services;
        }
    }
}
