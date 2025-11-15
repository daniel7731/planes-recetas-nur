using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class IngredienteDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public decimal Calorias { get; set; }

        public Guid CategoriaId { get; set; }

        public int UnidadId { get; set; }

    
    }
}
