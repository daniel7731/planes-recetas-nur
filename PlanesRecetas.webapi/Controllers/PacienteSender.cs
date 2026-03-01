using Joseco.Communication.External.Contracts.Services;
using PlanesRecetas.application.Pacientes.Evento;

namespace PlanesRecetas.webapi.Controllers
{
    public class PacienteSender
    {
        private readonly IExternalPublisher _publisher;

        public PacienteSender(IExternalPublisher publisher)
        {
            _publisher = publisher;
        }
        public async Task SendMessageAsync(PacienteCreated message)
        {


            // By default, the destination always is an Exchange.
            // The destinationName parameter is optinal. If you don't pass any value or pass a null value, the destination name will be the same as the Message type name using the kebab case format.
            var destinationName = "hello-created"; // This parameter is 

            //var declareDestinationFirst = true; // Set to true if you want to declare the exchange before sending the message. This parameter is optional. The default value is false.

            await _publisher.PublishAsync(message, destinationName);


        }
    }
}
