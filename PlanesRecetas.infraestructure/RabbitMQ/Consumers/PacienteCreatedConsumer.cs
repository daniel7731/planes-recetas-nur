
using MediatR;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.Evento;


namespace PlanesRecetas.infraestructure.RabbitMQ.Consumers
{
    //consume los objetos paciente create desde un cola ver dependencie injection
    public class PacienteCreatedConsumer:INotificationHandler<PacienteCreated>
    {
        private readonly IMediator _mediator;

        public PacienteCreatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(PacienteCreated notification, CancellationToken cancellationToken)
        {
            CreatePacienteComand command = new()
            {
                Guid = notification.Id,
                Nombre = notification.FirstName,
                Apellido = notification.MiddleName,
                FechaNacimiento = notification.DateOfBirth
            };

            await _mediator.Send(command, cancellationToken);
        }
    }



}
