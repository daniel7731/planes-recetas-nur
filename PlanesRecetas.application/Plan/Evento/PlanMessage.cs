using PlanesRecetas.application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan.Evento
{
    public class PlanMessage : IntegrationMessage
    {
        public Guid PlanId { get; set; }
        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }
    }
}
