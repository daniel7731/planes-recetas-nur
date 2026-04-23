using PlanesRecetas.application.Messaging;


namespace PlanesRecetas.application.Plan.Evento
{
    public class PlanMessage : IntegrationMessage
    {
        public Guid PlanId { get; set; }
        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Duracion { get; set; }
        public bool Requerido { get; set; } = false;

        public List<MessageItemDieta> Dietas { get; set; } = new List<MessageItemDieta>();
    }
}
