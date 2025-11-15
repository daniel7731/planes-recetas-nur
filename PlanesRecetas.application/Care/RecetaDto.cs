using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class RecetaDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public int TiempoId { get; set; }

        // Optional: related display info
        public string? TiempoNombre { get; set; }

        // Collection of ingredients that belong to this recipe
        public List<RecetaIngredienteDto> ?Ingredientes { get; set; }
    }
}
