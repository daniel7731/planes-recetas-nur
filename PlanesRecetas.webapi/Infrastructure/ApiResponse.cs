using System.Text.Json.Serialization;

namespace PlanesRecetas.webapi.Infrastructure
{
    public class ApiResponse
    {
        public Object Value { get; set; }

        public bool IsFailure { get; set; }

        public bool IsSuccess { get; set; }
        
        public ErrorDetails Error { get; set; }
    }
}
