using DesafioATS.Data;
using DesafioATS.WebAPI.Configuration;
using DesafioATS.WebAPI.Identidade;
using DesafioATS.WebAPI.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DesafioATS.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Serilog.Log.Information("Iniciando Web Host.");

                var host = CreateHostBuilder(args).Build();

                host.MigrateDbContext<IdentityATSContext>((context, services) => { });
                host.MigrateDbContext<DesafioATSContext>((context, services) => { });

                host.Run();
            }
            catch (System.Exception ex)
            {
                Serilog.Log.Fatal(ex, "Host encerrado inesperadamente.");
                throw;
            }
            finally
            {
                Serilog.Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    Serilog.Log.Logger = SerilogConfig.ConfigLog(config.Build()).CreateLogger();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
