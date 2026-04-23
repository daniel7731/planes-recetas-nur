using Joseco.DDD.Core.Abstractions;

namespace PlanesRecetas.domain.Plan.Events
{
    public record EventPlanCreated : DomainEvent
    {

        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }

        public bool Requerido { get; set; } = false;

        public List<EventItemDieta> Dietas { get; set; } = new List<EventItemDieta>();
        public EventPlanCreated(Guid Id, Guid pacienteId, Guid nutricionistaId, DateTime fechaInicio, int duracion , bool requerido)
        {
            this.Id = Id;
            this.PacienteId = pacienteId;
            this.NutricionistaId = nutricionistaId;
            this.FechaInicio = fechaInicio;
            this.Duracion = duracion;
            this.Requerido = requerido;
        }


    }
}