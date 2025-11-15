using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public class Tiempo : AggregateRoot
    {
        public int Id { get;  set; }     // Identity
        public string Nombre { get;  set; }

        public Tiempo() { }
        public Tiempo(int id, string nombre)
        {
            Id = id; Nombre = nombre;
        } 
    }
}
