using Joseco.Communication.External.Contracts.Message;

namespace PlanesRecetas.webapi.Controllers
{
    public record TestMessage : IntegrationMessage
    {
        public string Property1 { get; set; }
        public int Property2 { get; set; }
    }
}
