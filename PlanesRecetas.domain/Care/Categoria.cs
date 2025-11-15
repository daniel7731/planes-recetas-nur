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
        public TipoAlimento Tipo { get; set; }
        public int TipoAlimentoId {
            get; set;
        }   // scalar FK column
        public Categoria(Guid id, String nombre, TipoAlimento tipo) : base(id)
        {
            Nombre = nombre;
            Tipo = tipo;
        }
        private Categoria() { }
        public Categoria(Guid id, string nombre, int tipoAlimentoId):base(id)
        {
       
            Nombre = nombre;
            Tipo = new TipoAlimento(tipoAlimentoId,"");
            TipoAlimentoId = tipoAlimentoId;
           
        }
    }
}
