using DesafioATS.WebAPI.Identidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioATS.WebAPI.Configuration
{
    public static class ContextConfiguration
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.DesafioATSContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("DefaultContext"));
            });

            services.AddDbContext<IdentityATSContext>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("IdentityContext"));
            });

            return services;
        }
    }
}
