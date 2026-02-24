using Microsoft.Extensions.Options;
using PlanesRecetas.infraestructure.Messaging;
using RabbitMQ.Client;

public class RabbitMQConnection : IRabbitMQConnection
{
    private readonly RabbitMQSettings _settings;
    private IConnection? _connection;
    public IChannel Channel { get; private set; } = default!;

    public RabbitMQConnection(IOptions<RabbitMQSettings> options)
    {
        _settings = options.Value;
    }

    public async Task InitializeAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
  
        };

        _connection = await factory.CreateConnectionAsync();
        Channel = await _connection.CreateChannelAsync();

        await Channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        await Channel.BasicQosAsync(0, 1, false);
    }

    public async ValueTask DisposeAsync()
    {
        if (Channel != null)
            await Channel.CloseAsync();

        if (_connection != null)
            await _connection.CloseAsync();
    }
}