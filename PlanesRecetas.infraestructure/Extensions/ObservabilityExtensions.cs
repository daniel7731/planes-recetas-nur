using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Nur.Store2025.Observability;
using Nur.Store2025.Observability.Config;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Npgsql;
using PlanesRecetas.infraestructure.Persistence;

namespace Inventory.Infrastructure.Extensions;

public static class ObservabilityExtensions
{

    public static IServiceCollection AddObservability(this IServiceCollection services,
        IHostEnvironment environment, string serviceName)
    {

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing =>
            {
                tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation();

                tracing.AddOtlpExporter();

            }

       );

        return services;

          
        /*var jaegerSettings = services.BuildServiceProvider().GetRequiredService<JeagerSettings>();
        bool isWebApp = environment is IWebHostEnvironment;

        services.AddObservability(serviceName, jaegerSettings,
            builder =>
            {
                builder.AddSource("Joseco.Outbox")
                    .AddSqlClientInstrumentation();
            },
            shouldIncludeHttpInstrumentation: isWebApp);

        if (isWebApp)
        {
            services.AddServicesHealthChecks();
        }

        return services;*/

    }

    private static IServiceCollection AddServicesHealthChecks(this IServiceCollection services)
    {
        var databaseSettings = services.BuildServiceProvider().GetRequiredService<DataBaseSettings>();
        string? dbConnectionString = databaseSettings.ConnectionString;

        services
            .AddHealthChecks()
            .AddNpgSql(dbConnectionString , name:"postgres", tags: ["ready"]);
        //.AddRabbitMqHealthCheck();

        return services;
    }
}
