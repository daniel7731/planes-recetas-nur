using Joseco.DDD.Core.Abstractions;

namespace PlanesRecetas.domain.Plan.Events
{
    public record PlanCreated : DomainEvent
    {
        
        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }

        public PlanCreated(Guid Id, Guid pacienteId, Guid nutricionistaId, DateTime fechaInicio, int duracion)
        {
            this.Id = Id;
            this.PacienteId = pacienteId;
            this.NutricionistaId = nutricionistaId;
            this.FechaInicio = fechaInicio;
            this.Duracion = duracion;
        }

       
    }
}