using Consul;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nur.Store2025.Observability;
using PlanesRecetas.application.Pacientes.Evento;
using PlanesRecetas.infraestructure.Consumer;
using PlanesRecetas.infraestructure.Extensions;
using PlanesRecetas.infraestructure.RabbitMQ.Consumers;



namespace PlanesRecetas.infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment,
        string serviceName)
    {
        services.AddSecrets(configuration, environment)
       .AddObservability(environment,configuration, serviceName)
       .AddDatabase(configuration)
       .AddSecurity(environment)
       .AddRabbitMQ(configuration, environment);
        services.AddScoped<INotificationHandler<PacienteCreated>, PacienteCreatedConsumer>();
        services.AddHostedService<RabbitTopicWorker<PacienteCreated>>();

        return services;
    }
    public static IServiceCollection AddConsulServiceDiscovery(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment
    )
    {
     
        services.Configure<ConsulOptions>(configuration.GetSection(ConsulOptions.SectionName));

        services.AddSingleton<IConsulClient, ConsulClient>(sp => {
            ConsulOptions options = sp.GetRequiredService<IOptions<ConsulOptions>>().Value;
            return new ConsulClient(config => config.Address = new Uri(options.Host));
        });

        services.AddHostedService<ConsulHostedService>();

        return services;
    }

}
