namespace PlanesRecetas.webapi.Parameters.Plan
{
    public class ParamSearchPlanAlimentario
    {
        public Guid PacienteId { get; set; }
        public DateTime DesdeFecha { get; set; }

        public bool IgnorarFiltroFecha { get; set; } = false;

    }
}
