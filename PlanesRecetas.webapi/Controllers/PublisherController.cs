using Joseco.Communication.External.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using PlanesRecetas.application.Pacientes.Evento;
using PlanesRecetas.webapi.Controllers;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PublisherController : ControllerBase
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "hello-quee";
    private SenderTest sender;
    private PacienteSender pacienteSender;
    private readonly IExternalPublisher _externalPublisher;
    public PublisherController(IExternalPublisher externalPublisher)
    {
        _externalPublisher = externalPublisher;
        sender = new SenderTest(externalPublisher);
        pacienteSender = new PacienteSender(externalPublisher);

    }
    [HttpPost("[action]")]
    public async Task<IActionResult> PublishMessage([FromBody] object messageData)
    {
        var factory = new ConnectionFactory() { HostName = _hostname, 
            Port = 5672,
            UserName = "planRecetaUser",
            Password = "planRecetaPassword",
            Ssl = new SslOption
            {
                Enabled = false
            }
        };

        using (var connection = await factory.CreateConnectionAsync())
        using (var channel =  await connection.CreateChannelAsync())
        {
            // 1. Declare the queue (it won't be recreated if it exists)
            await channel.QueueDeclareAsync(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // 2. Serialize the body
            var message = JsonSerializer.Serialize(messageData);
            var body = Encoding.UTF8.GetBytes(message);

            // 3. Set message as persistent (saved to disk)
            var properties = new BasicProperties
            {
                Persistent = true
            };

            // 4. Publish to the Default Exchange
            await channel.BasicPublishAsync(exchange: "hello-created",
                                 routingKey: _queueName,
                                 mandatory: true,
                                 basicProperties: properties,
                                 body: body);
        }

        return Ok(new { status = "Message sent to RabbitMQ", data = messageData });
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> PublishMessageTest([FromBody] TestMessage messageData) { 
        // Implement other properties and methods as needed
        Guid guid = Guid.NewGuid();
        messageData.Id = guid;
        await sender.SendMessageAsync(messageData);
        return Ok(new { status = "Message sent to RabbitMQ", data = messageData });
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> PublishMessagePacienteTest([FromBody] PacienteCreated messageData)
    {
        // Implement other properties and methods as needed
        Guid guid = Guid.NewGuid();
        messageData.Id = guid;
        await pacienteSender.SendMessageAsync(messageData);
        return Ok(new { status = "Message sent to RabbitMQ", data = messageData });
    }
}
