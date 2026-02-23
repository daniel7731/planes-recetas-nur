using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Metrics
{
    public class UnidadMedidaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la unidad es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El símbolo es obligatorio")]
        [StringLength(10, ErrorMessage = "El símbolo no puede exceder los 10 caracteres")]
        public string Simbolo { get; set; }
    }
}
