using CRMSystem.Infrastructure.Repositories;
using CRMSystem.Modules.Auth.Application.Services;
using CRMSystem.Modules.Auth.Infrastructure.Mail;
using CRMSystem.Modules.Auth.Infrastructure.Security;
using CRMSystem.Shared.Entities;
using DotNetEnv;

namespace CRMSystem.Modules.Auth
{
    public static class ModulesAuthDependencyInjection
    {
        public static IServiceCollection AddAuthModule(
        this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IPasswordHashingService), typeof(PasswordHashingService));
            services.AddScoped(typeof(IRandomCodeService), typeof(RandomCodeService));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            return services;
        }

        public static IServiceCollection AddAuthModuleApi(
        this IServiceCollection services)
        {
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IPasswordHashingService), typeof(PasswordHashingService));
            services.AddScoped(typeof(IRandomCodeService), typeof(RandomCodeService));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
            services.AddScoped(typeof(IJwtService), typeof(JwtService));
            return services;
        }
    }
}
