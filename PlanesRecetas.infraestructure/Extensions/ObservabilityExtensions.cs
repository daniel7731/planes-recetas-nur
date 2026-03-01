using Joseco.Communication.External.RabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Observability;
using Nur.Store2025.Observability.Config;
using OpenTelemetry.Trace;
using PlanesRecetas.infraestructure.Extensions;
using PlanesRecetas.infraestructure.Persistence;

namespace PlanesRecetas.infraestructure.Extensions;

public static class ObservabilityExtensions
{
    public static IServiceCollection AddObservability(this IServiceCollection services, 
        IHostEnvironment environment, string serviceName)
    {
        bool isWebApp = environment is IWebHostEnvironment;

        // OpenTelemetry setup usually requires settings immediately. 
        // If your 'AddObservability' custom method doesn't support a factory, 
        // you can fetch the settings from the Configuration object directly:
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var jaegerSettings = config.GetSection("JeagerSettings").Get<JeagerSettings>();

        services.AddObservability(serviceName, jaegerSettings,
            builder =>
            {
                builder.AddSource("Joseco.Outbox")
                       .AddSource("Joseco.Communication.RabbitMQ")
                       .AddSqlClientInstrumentation();
            },
            shouldIncludeHttpInstrumentation: isWebApp);

        if (isWebApp)
        {
            services.AddServicesHealthChecks();
        }

        return services;
    }

    private static IServiceCollection AddServicesHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddSqlServer(sp =>
            {
                var settings = sp.GetRequiredService<DataBaseSettings>();
                return settings.ConnectionString;
            }, name: "sqlserver-check") // Corrected for SQL Server
            .AddRabbitMqHealthCheck();

        return services;
    }
}