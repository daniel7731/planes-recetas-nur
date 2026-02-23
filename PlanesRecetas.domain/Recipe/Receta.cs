using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PlanesRecetas.domain.Recipe
{
    public class Receta : AggregateRoot
    {
        public string Nombre { get; set; }

        public string? Instrucciones { get; set; }

        // mapped collection for the join entity
        public Tiempo? Tiempo { get; set; }
        public int TiempoId { get; set; }

        public Receta()
        {
            Nombre = "";
        }
        public Receta(Guid id) : base(id)
        {
            Nombre = "";
        }

        public Receta(Guid id, string nombre, Tiempo tiempo) : base(id)
        {
            Nombre = nombre;
            Tiempo = tiempo;
        }
    }
}
