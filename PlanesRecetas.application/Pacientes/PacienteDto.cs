using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes
{
    public class PacienteDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100)]
        public string Apellido { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Range(1, 500, ErrorMessage = "El peso debe estar entre 1 y 500 kg")]
        public Decimal Peso { get; set; }
        [Range(1, 300, ErrorMessage = "La altura debe estar entre 1 y 300.0 centimetros")]
        public Decimal Altura { get; set; }

    }
}
