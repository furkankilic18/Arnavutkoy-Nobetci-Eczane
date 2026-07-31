using Repository.Contrat;
using Repository.EF_Core;
using Microsoft.EntityFrameworkCore;
using Services.Contract;
using Services;

namespace Web_API.Extensions
{
    public static class ServiceExtension
    {
       
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager , ServiceManager>();
        }
    }
}
