using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Recipe.Evento
{
    public class EventItemIngredient
    {
        public Guid Id { get; set; }
        public decimal CantidadValor { get; set; } = 0;
    }
}
