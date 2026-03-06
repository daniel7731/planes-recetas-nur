using Joseco.Outbox.Contracts.Model;
using MediatR;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.application.Plan.Evento;
using PlanesRecetas.domain.Plan.Events;

public class PublishPlanCreated : INotificationHandler<OutboxMessage<PlanCreated>>
{
    private readonly IExternalPublisher _publisher;

    private readonly string exchangeName = "meal-plans";
    private readonly string exchangePatiens = "foodplan";
    public PublishPlanCreated(IExternalPublisher publisher) {
        _publisher = publisher;
    }

    public async Task Handle(OutboxMessage<PlanCreated> outboxMessage, CancellationToken cancellationToken)
    {
        PlanCreated content = outboxMessage.Content;
        string routingKey = "meal-plan.created";
        PlanMessage message = new()
        {
            PacienteId = content.PacienteId,
            NutricionistaId = content.NutricionistaId,
            FechaInicio = content.FechaInicio,
            Duracion = content.Duracion,
            PlanId = content.Id
        };

        await _publisher.PublishAsync(message, exchangePatiens, "foodplan.created");

        await _publisher.PublishAsync(message, exchangeName,routingKey);
    }
}
