namespace PlanesRecetas.webapi.Parameters.Plan
{
    public class CreatePlanAlimentarioParameter
    {
        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public List<DietaParameter> Dietas { get; set; }
        public DateTime FechaInicio { get; set; }
        public int DuracionDias { get; set; } = 15;

        public bool Requerido { get; set; } = false;

    }
}