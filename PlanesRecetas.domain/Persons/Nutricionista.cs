using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Persons
{
    public class Nutricionista : AggregateRoot
    {
       
        public String Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }    
        public Nutricionista() { }
        public Nutricionista( string nombre, bool activo, DateTime fechaCreacion)
        {
            Nombre=nombre;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }
        public Nutricionista(Guid id , string nombre, bool activo, DateTime fechaCreacion) : base(id)
        {
            Nombre = nombre;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }
    }
}
