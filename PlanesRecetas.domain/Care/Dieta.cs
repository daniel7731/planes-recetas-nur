using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Care
{
    public class Dieta : AggregateRoot
    {
        public String Nombre { get; set; }
        public List<Receta> Recetas { get; set; }
        public DateTime FechaConsumo { get; set; } 
        public Dieta(Guid id, String nombre, List<Receta> recetas, DateTime fechaConsumo):base(id)
        {
            Nombre = nombre;
            Recetas = recetas;
            FechaConsumo = fechaConsumo;
            if (Recetas == null || Recetas.Count == 0)
            {
                throw new ArgumentException("La dieta debe tener al menos una receta.");
            }
        }
    }
}
