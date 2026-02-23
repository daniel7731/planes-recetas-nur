using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public class DietaRecetaComand
    {
        public Guid DietaId { get; set; }
        public int Orden { get; set; }
        public int TiempoId { get; set; } // 1: Desayuno, 2: Almuerzo, 3: Cena, etc.
        public Guid RecetaId { get; set; }
    }
}
