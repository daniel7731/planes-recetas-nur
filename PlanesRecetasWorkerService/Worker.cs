using MediatR;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.infraestructure.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PlanesRecetasWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;
  
        private readonly IRabbitMQConnection _rabbit;
     
        private readonly RabbitMQSettings _options;
        public Worker(IConfiguration configuration , IMediator mediator , ILogger<Worker> logger, IRabbitMQConnection rabbit)
        {
            _logger = logger;
            _options = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>()
                   ?? new RabbitMQSettings();
            _rabbit = rabbit;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            var consumer = new AsyncEventingBasicConsumer(_rabbit.Channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var messageString = Encoding.UTF8.GetString(body);

                    var message = JsonSerializer.Deserialize<string>(messageString);
                    _logger.LogInformation("Received message: {Message}", message);
                    var paciente = JsonSerializer.Deserialize<PacienteCreatedMessage>(
                    message,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                    if (paciente is null)
                    {
                       
                       throw new Exception("Invalid message");
                    }
                

                 

                    CreatePacienteComand command = new CreatePacienteComand()
                    {
                        Guid = paciente.Id,
                        Nombre = paciente.Nombre,
                        FechaNacimiento = paciente.FechaNacimiento,
                        Altura = paciente.Altura,
                        Peso = paciente.Peso

                    };
                    var result = await _mediator.Send(command);
                         
                    _logger.LogInformation("Received message: {Message}", message);
                    // delay
                    await Task.Delay(500, stoppingToken);

                    await _rabbit.Channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                    await _rabbit.Channel.BasicAckAsync(ea.DeliveryTag, false);
                }
            };

            await _rabbit.Channel. BasicConsumeAsync(
                queue: _options.QueueName,
                autoAck: false,
                consumer: consumer);

            _logger.LogInformation("RabbitMQ consumer started.");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

    
    }
}