using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public class Receta : AggregateRoot
    {
       
        public String Nombre { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }
     
        public Tiempo? Tiempo { get; set; }
        public int TiempoId{ get; set; }

        public Receta() { }
        public Receta(Guid id) : base(id) { }
        public List<Guid> IngredienteIds {
            get; set;
        }

        public Receta(Guid id, String nombre, List<Ingrediente> ingredientes, Tiempo tiempo):base(id)
        {
            Nombre = nombre;
            Ingredientes = ingredientes;
            Tiempo = tiempo;
            
        }
        public Decimal CalcularCalorias()
        {
            if (Ingredientes == null || Ingredientes.Count == 0)
                return 0;
            return Ingredientes.Sum(i => i.Calorias);
        }
    }
}
