using Joseco.DDD.Core.Abstractions;

namespace PlanesRecetas.domain.Plan.Events
{
    public record PlanAlimentacionCreadoEvent : DomainEvent
    {
        private Guid Id;
        private Guid pacienteId;
        private Guid nutricionistaId;
        private DateTime fechaInicio;
        private int duracion;

        public PlanAlimentacionCreadoEvent(Guid Id, Guid pacienteId, Guid nutricionistaId, DateTime fechaInicio, int duracion)
        {
            this.Id = Id;
            this.pacienteId = pacienteId;
            this.nutricionistaId = nutricionistaId;
            this.fechaInicio = fechaInicio;
            this.duracion = duracion;
        }

       
    }
}