using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public class TipoAlimento : AggregateRoot
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();

        public TipoAlimento() { }
        public TipoAlimento(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}
