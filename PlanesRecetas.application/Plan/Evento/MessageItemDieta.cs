using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan.Evento
{
    public class MessageItemDieta
    {
        public Guid DietaId { get; set; }
        public DateTime FechaConsumo { get; set; }
        public List<MessageItemDietaReceta> Recetas { get; set; } = new List<MessageItemDietaReceta>();
    }
}
