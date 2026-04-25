using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PlanesRecetas.infraestructure.Persistence;

namespace PlanesRecetas.infraestructure.Extensions;

public static class ObservabilityExtensions
{

    public static IServiceCollection AddObservability(this IServiceCollection services,
        IHostEnvironment environment, IConfiguration configuration, string serviceName)
    {

        // This will automatically map Telemetry__ServiceName to ServiceName
        // This binds the environment variables/JSON to a new instance immediately
        var telemetrySettings = configuration.GetSection("Telemetry").Get<TelemetrySettings>();

        string otlpEndpoint = telemetrySettings.OtlpEndpoint;

        services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(o => o.Endpoint = new Uri(otlpEndpoint)))
            .WithMetrics(metrics => metrics
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddPrometheusExporter())
            .AddRabbitMqInstrumentation();

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
    public static OpenTelemetryBuilder AddRabbitMqInstrumentation(this OpenTelemetryBuilder builder)
    {
        return builder.WithTracing(delegate (TracerProviderBuilder tracing)
        {
            tracing.AddSource("Joselct.Communication.RabbitMQ");
        }).WithMetrics(delegate (MeterProviderBuilder metrics)
        {
            metrics.AddMeter("Joselct.Communication.RabbitMQ");
        });
    }
}
