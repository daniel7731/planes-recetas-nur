using Joseco.Outbox.Contracts.Model;
using MediatR;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.application.Plan.Evento;
using PlanesRecetas.domain.Plan.Events;

public class PublishPlanCreated : INotificationHandler<OutboxMessage<EventPlanCreated>>
{
    private readonly IExternalPublisher _publisher;

    private readonly string exchangeName = "meal-plans";
    public PublishPlanCreated(IExternalPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(OutboxMessage<EventPlanCreated> outboxMessage, CancellationToken cancellationToken)
    {
        EventPlanCreated content = outboxMessage.Content;
        string routingKey = "meal-plan.plan";
        PlanMessage message = new()
        {
            PacienteId = content.PacienteId,
            NutricionistaId = content.NutricionistaId,
            FechaInicio = content.FechaInicio,
            Duracion = content.Duracion,
            PlanId = content.Id,
            Requerido = content.Requerido,
            Dietas = content.Dietas.Select(d => new MessageItemDieta
            {
                DietaId = d.DietaId,
                FechaConsumo = d.FechaConsumo,
                Recetas = d.Recetas.Select(r => new MessageItemDietaReceta
                {
                    RecetaId = r.RecetaId,
                    TiempoId = r.TiempoId,
                    Orden = r.Orden
                }).ToList()
            }).ToList()
        };
        await _publisher.PublishAsync(message, exchangeName, routingKey);   
    }
}
