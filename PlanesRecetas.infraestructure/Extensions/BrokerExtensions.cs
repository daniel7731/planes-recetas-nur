using Joseco.Communication.External.Contracts.Services;
using Joseco.Communication.External.RabbitMQ;
using Joseco.Communication.External.RabbitMQ.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanesRecetas.application.Pacientes.Evento;
using PlanesRecetas.domain.Plan.Events;
using PlanesRecetas.infraestructure.Extensions;
using PlanesRecetas.infraestructure.Observability;
using PlanesRecetas.infraestructure.RabbitMQ.Consumers;

namespace PlanesRecetas.infraestructure.Extensions
{
    public static class BrokerExtensions
    {

        //agregar aqui los consumers
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IHostEnvironment environment)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var rabbitMqSettings = serviceProvider.GetRequiredService<RabbitMqSettings>();

            bool isWebApp = environment is IWebHostEnvironment;
            services.AddRabbitMQ(rabbitMqSettings);
            if (isWebApp)
            {
                return services;
            }
            //string exchangeName= "hello-created";
            string queeName= "hello-quee";
            services.AddRabbitMqConsumer<PacienteCreated, PacienteCreatedConsumer>(queeName);
            //consumer 1
            services.Decorate<IIntegrationMessageConsumer<PacienteCreated>, TracingConsumer<PacienteCreated>>();

            return services;
        }
    }
}
