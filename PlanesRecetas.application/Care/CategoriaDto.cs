using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class CategoriaDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

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
