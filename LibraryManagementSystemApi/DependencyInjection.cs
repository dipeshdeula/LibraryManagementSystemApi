using Application;
using Domain;
using Infrastructure;

namespace LibraryManagementSystemApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLMSDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
               .AddInfrastructureDI(configuration)
               .AddDomainDI(configuration);

            return services;
        }
    }
}
