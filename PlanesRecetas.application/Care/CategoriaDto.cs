using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class CategoriaDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de alimento válido")]
        public int TipoAlimentoId { get; set; }

        public CategoriaDto(Guid id, string nombre, int tipoAlimentoId)
        {
            Id = id;
            Nombre = nombre;
            TipoAlimentoId = tipoAlimentoId;
        }
        public CategoriaDto()
        {

        }
    }
}
