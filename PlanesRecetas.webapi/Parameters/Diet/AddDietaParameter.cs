namespace PlanesRecetas.webapi.Parameters.Diet
{
    public class AddDietaParameter
    {
        public Guid PlanId { get; set; }
        public List<Guid> Platillos { get; set; }
        public int NDiaPlan { get; set; }
        public DateTime Fecha { get; set; }
    }
}
