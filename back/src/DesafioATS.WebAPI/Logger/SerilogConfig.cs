using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

namespace DesafioATS.WebAPI.Logger
{
    public static partial class SerilogConfig
    {
        public static LoggerConfiguration ConfigLog(IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("MongoDB");

            var client = new MongoClient(connStr);
            var db = client.GetDatabase(configuration.GetSection("DataBaseMongo").Value);

            var excludeInfo = new StringBuilder();

            excludeInfo.Append("@Level = 'Error' ");
            excludeInfo.Append("or @Level = 'Fatal' ");
            excludeInfo.Append("or @Level = 'Warning' ");
            excludeInfo.Append("or EventId.Id = 1000 ");
            excludeInfo.Append("or EventId.Id = 2000 ");

            return new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
               .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
               .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)

               .MinimumLevel.Override("Microsoft.Extensions.Diagnostics.HealthChecks.DefaultHealthCheckService", Serilog.Events.LogEventLevel.Fatal)

               .Enrich.FromLogContext()

               .WriteTo.Logger(lc => lc
                   .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}")
               )

               .WriteTo.Logger(lc => lc
                   .Filter.ByExcluding(excludeInfo.ToString())
                   .WriteTo.MongoDB(db, Serilog.Events.LogEventLevel.Information, collectionName: "Info")
               )

               .WriteTo.Logger(lc => lc
                   /*.Filter.ByExcluding("EventId.Id = 1000 or EventId.Id = 2000 or EventId.Id = 3000")*/
                   .WriteTo.MongoDB(db, Serilog.Events.LogEventLevel.Error, collectionName: "Error")
               )

               .WriteTo.Logger(lc => lc
                   .Filter.ByIncludingOnly("EventId.Id = 1000")
                   .WriteTo.MongoDB(db, Serilog.Events.LogEventLevel.Information, collectionName: "Api")
               )

               .WriteTo.Logger(lc => lc
                   .Filter.ByIncludingOnly("EventId.Id = 2000")
                   .WriteTo.MongoDB(db, Serilog.Events.LogEventLevel.Information, collectionName: "Bus")
               )

               ;
        }
    }
}
