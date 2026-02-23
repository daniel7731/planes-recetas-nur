using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe
{
    public class CreateRecetaIngredienteComand
    {
        public Guid RecetaId { get; set; }
        public Guid IngredienteId { get; set; }
        public decimal? CantidadValor { get; set; }
    }
}
