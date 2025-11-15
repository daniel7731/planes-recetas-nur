using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class RecetaIngredienteDto
    {
        public Guid RecetaId { get; set; }

        public Guid IngredienteId { get; set; }

        public decimal? CantidadValor { get; set; }

        // Optional: related display info for convenience
        public string? IngredienteNombre { get; set; }
        public string? UnidadNombre { get; set; }
        public string? CategoriaNombre { get; set; }
    }

}
