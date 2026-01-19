using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public class DietaReceta
    {
        public int Id { get; set; }
        public Guid DietaId { get; set; }
        public Guid RecetaId { get; set; }
        public int? Orden { get; set; }

        // Propiedades de navegación
        public virtual Dieta Dieta { get; set; }
        public virtual Receta Receta { get; set; }
    }
}
