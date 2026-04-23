using PlanesRecetas.domain.Recipe;
using PlanesRecetas.domain.Recipe.Evento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Plan.Events
{
    public class EventItemDietaReceta
    {
        public Guid RecetaId { get; set; }
        public int TiempoId { get; set; } = 1;
        public int Orden { get; set; } = 0;
    }
}
