using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Metrics
{
    public class UnidadMedida : AggregateRoot
    {
        public int Id { get; private set; } // Identity
        public string Nombre { get; private set; }
        public string Simbolo { get; private set; }

        private UnidadMedida() { }
        public UnidadMedida(int id,string nombre, string simbolo)
        {
            Id = id;
            Nombre = nombre;
            Simbolo = simbolo;
        }

        public void Update(string nombre, string simbolo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(simbolo))
                throw new ArgumentException("El símbolo no puede estar vacío.");

            Nombre = nombre;
            Simbolo = simbolo;
        }


    }
}
