using EmployeeManagement.Repositories.Implementation;
using EmployeeManagement.Repositories.Interfaces;

namespace EmployeeManagement.Services
{
    public static class DependencyService
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<JwtTokenService>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            return services;
        }

    }
}
