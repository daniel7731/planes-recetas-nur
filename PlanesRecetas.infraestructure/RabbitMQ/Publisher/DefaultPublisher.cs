
using Microsoft.Extensions.Options;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.infraestructure.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlanesRecetas.infraestructure.RabbitMQ.Publisher
{
    public class DefaultPublisher : IExternalPublisher
    {
        private readonly RabbitMQSettings _settings;

        public DefaultPublisher(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
        }

        // Implementation for the specific Exchange/RoutingKey version
        public async Task PublishAsync<T>(T messageData, string exchangeName, string routingKey) where T : IntegrationMessage
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password,
                // Ensure Ssl is configured correctly based on your environment
                Ssl = new SslOption { Enabled = false }
            };

            // Using await for modern RabbitMQ.Client (v7+)
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            // 1. Declare the Exchange
            await channel.ExchangeDeclareAsync(
                exchange: exchangeName,
                type: ExchangeType.Topic,
                durable: true);

            // 2. Serialize the body
            var messageJson = JsonSerializer.Serialize(messageData);
            var body = Encoding.UTF8.GetBytes(messageJson);

            // 3. Set Properties (Persistent ensures message survives broker restart)
            var properties = new BasicProperties { Persistent = true };

            // 4. Publish
            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: properties,
                body: body);

            Console.WriteLine($" [x] Sent '{routingKey}':'{messageJson}'");
        }

        // Implementation of the interface method (routing based on 'destination')
        public async Task PublishAsync<T>(T message, string? destination = null, bool declareDestination = false) where T : IntegrationMessage
        {
            // Use the destination as the exchange name, and the message type as the routing key
            string exchange = destination ?? "default_exchange";
            string routingKey = "";
            string name = message.GetType().Name;
            if (name.Equals("PlanCreated", StringComparison.OrdinalIgnoreCase))
            {
                routingKey = "plans.created";
            }
            await PublishAsync(message, exchange, routingKey);
        }
    }
}
