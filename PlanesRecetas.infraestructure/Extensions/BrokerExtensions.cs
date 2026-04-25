using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.Evento;
using PlanesRecetas.infraestructure.Consumer;
using PlanesRecetas.infraestructure.Messaging;
using PlanesRecetas.infraestructure.RabbitMQ.Consumers;
using PlanesRecetas.infraestructure.RabbitMQ.Publisher;

namespace PlanesRecetas.infraestructure.Extensions
{
    public static class BrokerExtensions
    {

        //agregar aqui los consumers
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            using var serviceProvider = services.BuildServiceProvider();
            //var rabbitMqSettings = serviceProvider.GetRequiredService<RabbitMQSettings>();
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQSettings"));
            bool isWebApp = environment is IWebHostEnvironment;
            services.AddScoped<IExternalPublisher, DefaultPublisher>();

          
            return services;
        }
    }
}
