using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class TipoAlimentoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public TipoAlimentoDto() { }
        public TipoAlimentoDto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
