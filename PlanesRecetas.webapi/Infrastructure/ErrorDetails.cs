using System.Text.Json.Serialization;

namespace PlanesRecetas.webapi.Infrastructure
{
    public class ErrorDetails
    {
        public string Code { get; set; }
 
        public string Description { get; set; }

        public string StructuredMessage { get; set; }

  
        public int Type { get; set; }
    }
}
