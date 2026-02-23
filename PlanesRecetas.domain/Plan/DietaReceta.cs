using PlanesRecetas.domain.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Plan
{
    public class DietaReceta
    {
        public int Id { get; set; }
        public Guid DietaId { get; set; }
        public Guid RecetaId { get; set; }
        public int  TiempoId { get; set; } // 1: Desayuno, 2: Almuerzo, 3: Cena, etc.   
        public int? Orden { get; set; }

        public Dieta? Dieta { get; set; }
        public Receta? Receta { get; set; }


    }
}
