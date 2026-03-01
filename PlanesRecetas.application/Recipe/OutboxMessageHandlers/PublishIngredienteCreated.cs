using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using MediatR;
using PlanesRecetas.application.Recipe.Evento;


public class PublishIngredienteCreated : INotificationHandler<OutboxMessage<IngredienteCreated>>
{
    private readonly IExternalPublisher _publisher;

    private readonly string exchangeName = "ingredient-created";
    public PublishIngredienteCreated(IExternalPublisher publisher) {
        _publisher = publisher;
    }

    public async Task Handle(OutboxMessage<IngredienteCreated> outboxMessage, CancellationToken cancellationToken) {
        
        //IngredienteCreated is and event
        IngredienteCreated content = outboxMessage.Content;
        // ingredientMessage is a message that will be sent to the external system extend IntegrationMessage
        IngredienteMessage message = new ()
        {
           IngredienteId = content.Id,
           Nombre = content.Nombre,
           Calorias = content.Calorias,
           UnidadId  = content.UnidadId,
           CategoriaId = content.CategoriaId   
        };
        

        await _publisher.PublishAsync(message,exchangeName);
    }
}
