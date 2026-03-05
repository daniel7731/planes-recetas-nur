

namespace PlanesRecetas.application.Messaging
{
    public interface IMPublisher
    {
        public Task PublishAsync<T>(T message, string exchangeName , string routingkey);
    }
}
