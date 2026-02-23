using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Recipe;

namespace PlanesRecetas.webapi.Parameters.Recipe
{
    public class CreateRecetaParameter
    {
        public string Nombre { get; set; }
        public List<RecetaIngredienteParameter> IngredienteList { get; set; }
        public int TiempoId { get; set; }
        public string Instrucciones { get; set; }   
    }
}
