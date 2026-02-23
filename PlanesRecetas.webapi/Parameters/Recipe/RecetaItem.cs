using PlanesRecetas.application.Care;

namespace PlanesRecetas.webapi.Parameters.Recipe
{
    public class RecetaItem
    {
        public Guid guid;
        public string Instrucciones { get; set; }
        public string Nombre { get; set; }
       //ublic List<ParamIngrediente> Ingredientes { get; set; }
        public int TiempoId { get; set; }
    }
}
