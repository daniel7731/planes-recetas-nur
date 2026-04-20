namespace PlanesRecetas.webapi.Parameters.Recipe
{
    public class ParamGetReceta
    {
        public Guid Id { get; set; }
        public bool IncludeIngredientes { get; set; } = false;  
    }
}
