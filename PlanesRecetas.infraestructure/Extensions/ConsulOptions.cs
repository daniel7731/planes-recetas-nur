using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Extensions
{
    public class ConsulOptions
    {
        public const string SectionName = "Consul";

        public string Host { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceAddress { get; set; } = "localhost";
        public int ServicePort { get; set; } = 80;
        public string[] Tags { get; set; } = ["dotnet", "api", "ms-plans", "metrics"];
        public string HealthCheckEndpoint { get; set; } = "/health/live";
    }

}
