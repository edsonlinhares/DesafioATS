using DesafioATS.WebAPI.Configuration;
using DesafioATS.WebAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetDevPack.Security.Jwt.AspNetCore;
using Newtonsoft.Json.Converters;

namespace DesafioATS.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ResolveDependencies(Configuration);
            services.ConfigureAPI();
            services.ConfigureContext(Configuration);

            services.AddMediatR(typeof(DesafioATS.Domain.TipoContrato));

            services.AddAutoMapper(typeof(Startup));

            services.ConfigureIdentity(Configuration);

            services.AddTokenAuthenticationServices(Configuration);

            services.AddSwaggerConfig();

            services.AddCors();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(ValidateModelStateFilter));
                options.Filters.Add(typeof(ApiActionFilter));
            })
            .AddApplicationPart(typeof(Startup).Assembly)
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(option =>
            {
                option.AllowAnyOrigin();
                option.AllowAnyMethod();
                option.AllowAnyHeader();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            IdentityConfiguration.CreateRoles(app.ApplicationServices).Wait();

            app.UseJwksDiscovery();

            app.UseSwaggerConfig(provider, Configuration.GetSection("SwaggerPrefix").Value);

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
