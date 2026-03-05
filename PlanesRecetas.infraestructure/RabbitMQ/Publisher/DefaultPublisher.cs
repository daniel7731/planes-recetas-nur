
using Joseco.Communication.External.Contracts.Message;

using Microsoft.Extensions.Options;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.infraestructure.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlanesRecetas.infraestructure.RabbitMQ.Publisher
{
    public class DefaultPublisher : IMPublisher 
    {
        private readonly RabbitMQSettings _settings;
     
        public DefaultPublisher(IOptions<RabbitMQSettings> options)
        {
          _settings = options.Value;
         
        }
        public async Task PublishAsync<T>(T messageData, string exchangeName, string routingKey) 
        {
            Console.Write("sending");
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password,
                Ssl = new SslOption { Enabled = false }
            };

            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                // 1. Declare the Exchange as "direct" instead of fanout
           
                await channel.ExchangeDeclareAsync(
                    exchange: exchangeName,
                    type: ExchangeType.Direct, // This enables routing key logic
                    durable: true);       

                // 3. Serialize the body
                var message = JsonSerializer.Serialize(messageData);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = new BasicProperties { Persistent = true };

                // 4. Publish using the specific routingKey
                await channel.BasicPublishAsync(
                    exchange: exchangeName,
                    routingKey: routingKey, // The message only goes to queues bound with this key
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            }

          
        }

        public Task PublishAsync<T>(T message, string? destination = null, bool declareDestination = false) where T : IntegrationMessage
        {
            throw new NotImplementedException();
        }
    }
}
