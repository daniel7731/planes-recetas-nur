using Consul;
using Inventory.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nur.Store2025.Observability;
using PlanesRecetas.infraestructure.Extensions;



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
       .AddObservability(environment, serviceName)
       .AddDatabase(configuration)
       .AddSecurity(environment)
       .AddRabbitMQ(configuration, environment);


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
