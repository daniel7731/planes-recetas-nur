using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class RecetaIngredienteDto
    {
        [Required(ErrorMessage = "El ID de la receta es obligatorio")]
        public Guid RecetaId { get; set; }

        [Required(ErrorMessage = "El ID del ingrediente es obligatorio")]
        public Guid IngredienteId { get; set; }
        [Range(0.01, 10000, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public decimal? CantidadValor { get; set; }

        // Optional: related display info for convenience
        public string? IngredienteNombre { get; set; }
        public string? UnidadNombre { get; set; }
        public string? CategoriaNombre { get; set; }
    }

}
