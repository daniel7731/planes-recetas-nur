using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class IngredienteDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del ingrediente es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Range(0, 150000, ErrorMessage = "Las calorías deben ser un valor entre 0 y 5000")]
        public decimal Calorias { get; set; }
        [Required(ErrorMessage = "La categoría es obligatoria")]
        public Guid CategoriaId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una unidad válida")]
        public int UnidadId { get; set; }

    
    }
}
