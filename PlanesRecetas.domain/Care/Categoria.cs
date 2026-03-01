using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joseco.DDD.Core.Abstractions;

namespace PlanesRecetas.domain.Care
{
    public class Categoria : AggregateRoot
    {
        public String Nombre { get; set; }
        public TipoAlimento TipoAlimento { get; set; }
        public int TipoAlimentoId {
            get; set;
        }   
        public Categoria(Guid id, String nombre, TipoAlimento tipo) : base(id)
        {
            Nombre = nombre;
            TipoAlimento = tipo;
        }
        private Categoria() { }
        public Categoria(Guid id, string nombre, int tipoAlimentoId):base(id)
        {
       
            Nombre = nombre;
            TipoAlimento = new TipoAlimento(tipoAlimentoId,"");
            TipoAlimentoId = tipoAlimentoId;
           
        }
    }
}
