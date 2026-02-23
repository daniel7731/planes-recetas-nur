using PlanesRecetas.application.Care;
using PlanesRecetas.application.Recipe;

namespace PlanesRecetas.application.Plan
{
    public class DietaRecetaDto
    {
        public RecetaDto Receta { get; set; }
        public int Orden { get; set; }
        public TiempoDto Tiempo { get; set; }
        public DietaRecetaDto() { }
    }
}