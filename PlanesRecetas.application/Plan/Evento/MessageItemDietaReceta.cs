using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan.Evento
{
    public class MessageItemDietaReceta
    {
        public Guid RecetaId { get; set; }
        public int Orden { get; set; }
        public int TiempoId { get; set; }
    }
}
