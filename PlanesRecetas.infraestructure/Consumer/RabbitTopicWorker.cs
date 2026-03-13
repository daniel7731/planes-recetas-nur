using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlanesRecetas.infraestructure.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PlanesRecetas.infraestructure.Consumer
{



    public class RabbitTopicWorker<T> : BackgroundService
    {
        private readonly ILogger<RabbitTopicWorker<T>> _logger;

        private IConnection? _connection;
        private IChannel? _channel;
        private string? _queueName;


        private const string ExchangeName = "patients";
        private const string RoutingKey = "patient.*";
        private const string QueueName = "ms-meal-plans-queue";
        private RabbitMQSettings _settings;
        private readonly IServiceScopeFactory _scopeFactory;
        public RabbitTopicWorker(
              IOptions<RabbitMQSettings> options, ILogger<RabbitTopicWorker<T>> logger,
              IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _settings = options.Value;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.Host,
                UserName = _settings.UserName,
                Password = _settings.Password,
                Ssl = new SslOption { Enabled = false }

            };

            _connection = await factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync();

            // Declare Exchange
            await _channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false,
                cancellationToken: stoppingToken);

            // Declare Queue
            var queueResult = await _channel.QueueDeclareAsync(
                durable: false,
                exclusive: true,
                autoDelete: true,
                cancellationToken: stoppingToken);



            // Bind queue to exchange
            await _channel.QueueBindAsync(
                queue: QueueName,
                exchange: ExchangeName,
                routingKey: RoutingKey,
                cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);



                    _logger.LogInformation(
                        "Message received. RoutingKey: {RoutingKey}",
                        ea.RoutingKey);

                    if (json != null)
                    {
                        await HandleMessage(json);
                    }

                    await Task.Delay(100, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing RabbitMQ message");
                }
            };

            await _channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: true,
                consumer: consumer,
                cancellationToken: stoppingToken);

            _logger.LogInformation(
                "RabbitMQ Worker started. Listening on exchange '{Exchange}' with routing '{RoutingKey}'",
                ExchangeName,
                RoutingKey);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ Worker stopping");

            if (_channel != null && _channel.IsOpen)
                await _channel.CloseAsync(cancellationToken);

            if (_connection != null && _connection.IsOpen)
                await _connection.CloseAsync(cancellationToken);

            await base.StopAsync(cancellationToken);
        }
        protected virtual Task HandleMessage(string json)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                using var scope = _scopeFactory.CreateScope();

                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var message = JsonSerializer.Deserialize<T>(json, options);

                if (message == null)
                {
                    _logger.LogWarning("Failed to deserialize message to {Type}", typeof(T).Name);

                }

                _logger.LogInformation("Successfully deserialized {Type}", typeof(T).Name);
                mediator.Publish(message);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Invalid JSON received");
            }
            return Task.CompletedTask;
        }
    }

}
