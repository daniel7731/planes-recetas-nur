using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Plan.Events
{
    public class EventItemDieta
    {
        public Guid DietaId { get; set; }
        public DateTime FechaConsumo { get; set; }
        public List<EventItemDietaReceta> Recetas { get; set; } = new List<EventItemDietaReceta >();

    }
}
