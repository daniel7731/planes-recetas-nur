using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class TiempoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del tiempo de comida es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; }
        public TiempoDto() { }
        public TiempoDto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
