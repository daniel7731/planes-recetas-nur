using Joseco.Communication.External.Contracts.Services;
using MediatR;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.Evento;

namespace PlanesRecetas.infraestructure.RabbitMQ.Consumers
{
    //consume los objetos paciente create desde un cola ver dependencie injection
    public class PacienteCreatedConsumer(IMediator mediator): 
        IIntegrationMessageConsumer<PacienteCreated>
    {
        public async Task HandleAsync(PacienteCreated message, CancellationToken cancellationToken)
        {
            CreatePacienteComand command = new()
            {
                Guid = message.Id,
                Nombre = message.Nombre,
                Apellido = message.Apellido,
                FechaNacimiento = message.FechaNacimiento,

            };

            await mediator.Send(command, cancellationToken);
        }
    }
}
