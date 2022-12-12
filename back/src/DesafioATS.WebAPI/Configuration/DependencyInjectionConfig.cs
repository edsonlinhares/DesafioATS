using DesafioATS.Data.Query;
using DesafioATS.Data.Repository;
using DesafioATS.Domain.Candidaturas;
using DesafioATS.Domain.Core.Notifications;
using DesafioATS.Domain.Curriculos;
using DesafioATS.Domain.Handlers;
using DesafioATS.Domain.Interfaces;
using DesafioATS.Domain.Vagas;
using DesafioATS.EventSources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioATS.WebAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<Identidade.AuthenticationService>();

            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<EventSourceContext>(provider => 
            new EventSourceContext(configuration.GetConnectionString("MongoDB"), 
            configuration.GetSection("DataBaseMongo").Value));
            services.AddScoped<IEventStore, EventStoreRepository>();

            services.AddScoped<IVagaQuery, VagaQuery>();
            services.AddScoped<IVagaRepository, VagaRepository>();

            services.AddScoped<ICandidaturaQuery, CandidaturaQuery>();
            services.AddScoped<ICandidaturaRepository, CandidaturaRepository>();

            services.AddScoped<ICurriculoQuery, CurriculoQuery>();
            services.AddScoped<ICurriculoRepository, CurriculoRepository>();

            return services;
        }
    }
}
