using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using MediatR;
using PlanesRecetas.application.Plan.Evento;
using PlanesRecetas.domain.Plan.Events;

public class PublishPlanCreated : INotificationHandler<OutboxMessage<PlanCreated>>
{
    private readonly IExternalPublisher _publisher;

    private readonly string exchangeName = "plan-created";
    public PublishPlanCreated(IExternalPublisher publisher) {
        _publisher = publisher;
    }

    public async Task Handle(OutboxMessage<PlanCreated> outboxMessage, CancellationToken cancellationToken)
    {
        PlanCreated content = outboxMessage.Content;
        PlanMessage message = new()
        {
            PacienteId = content.PacienteId,
            NutricionistaId = content.NutricionistaId,
            FechaInicio = content.FechaInicio,
            Duracion = content.Duracion,
            PlanId = content.Id
        };



        await _publisher.PublishAsync(message, exchangeName);
    }
}
