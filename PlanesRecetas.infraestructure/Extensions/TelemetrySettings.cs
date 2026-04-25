using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Extensions
{
    public class TelemetrySettings
    {
        public const string SectionName = "Telemetry";

        public string ServiceName { get; set; } = string.Empty;

        public string OtlpEndpoint { get; set; } = string.Empty;
    }
}
