

namespace PlanesRecetas.application.Messaging
{
    public interface IExternalPublisher
    {
        Task PublishAsync<T>(T message, string? destination = null, bool declareDestination = false) where T : IntegrationMessage;
        Task PublishAsync<T>(T messageData, string exchangeName, string routingKey) where T : IntegrationMessage;
    }
}
