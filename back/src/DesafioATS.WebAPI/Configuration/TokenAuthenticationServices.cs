using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NetDevPack.Security.JwtExtensions;

namespace DesafioATS.WebAPI.Configuration
{
    public static class TokenAuthenticationServices
    {
        public static IServiceCollection AddTokenAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationProviderKey = "DesafioATSKey";

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = authenticationProviderKey;
                o.DefaultChallengeScheme = authenticationProviderKey;
            })
            .AddJwtBearer(authenticationProviderKey, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.IncludeErrorDetails = true;
                x.SetJwksOptions(new JwkOptions(configuration.GetSection("UrlJwks").Value));
            });

            return services;
        }
    }
}
