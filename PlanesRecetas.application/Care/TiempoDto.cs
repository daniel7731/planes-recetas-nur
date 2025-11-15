using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class TiempoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public TiempoDto() { }
        public TiempoDto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
