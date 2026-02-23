namespace PlanesRecetas.webapi.Parameters.Plan
{
    public class DietaParameter
    {
      
       public DateOnly Fecha { get; set; }
       public List<DietaRectaParameter> Recetas { get; set; }
    }
}
