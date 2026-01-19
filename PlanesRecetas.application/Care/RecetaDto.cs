using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class RecetaDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "el nombre de la receta es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder los 150 caracteres")]
        public string Nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tiempo de comida válido")]
        public int TiempoId { get; set; }

        // Optional: related display info
        public string? TiempoNombre { get; set; }

        // Collection of ingredients that belong to this recipe
        public List<RecetaIngredienteDto> ?Ingredientes { get; set; }
    }
}
