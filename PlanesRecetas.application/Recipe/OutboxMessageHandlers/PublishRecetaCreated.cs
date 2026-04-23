
using Joseco.Outbox.Contracts.Model;
using MediatR;
using PlanesRecetas.application.Messaging;
using PlanesRecetas.application.Recipe.Evento;
using PlanesRecetas.domain.Recipe.Evento;


public class PublishRecetaCreated : INotificationHandler<OutboxMessage<EventRecetaCreated>>
{
    private readonly IExternalPublisher _publisher;

    private readonly string exchangeName = "meal-plans";
    private readonly string routingKey = "meal-plan.receta";
    public PublishRecetaCreated(IExternalPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(OutboxMessage<EventRecetaCreated> notification, CancellationToken cancellationToken)
    {
        EventRecetaCreated content = notification.Content;

        // RecetaMessage is a message that will be sent to the external system extend IntegrationMessage
        RecetaMessage message = new()
        {
            RecetaId = content.Id,
            Nombre = content.Nombre,
            Instrucciones = content.Instrucciones,
            TiempoId = content.TiempoId,
            IngredientesId = content.IngredientesId.Select(i => new MessageItemIngredient
            {
                Id = i.Id,
                CantidadValor = i.CantidadValor
            }).ToList()
        };
        await _publisher.PublishAsync(message, exchangeName, routingKey);
    }
}
