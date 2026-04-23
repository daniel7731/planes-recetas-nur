using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Recipe.Evento
{
    public record EventRecetaCreated : DomainEvent
    {
        public string Nombre { get; set; } = string.Empty;

        public string Instrucciones { get; set; } = string.Empty;

        // mapped collection for the join entity
        public List<EventItemIngredient> IngredientesId { get; set; } = new List<EventItemIngredient>(); 
        public int TiempoId { get; set; }
        public EventRecetaCreated(Guid Id, string nombre, string instrucciones, int tiempoId)
        {
            this.Id = Id;
            Nombre = nombre;
            Instrucciones = instrucciones;
            TiempoId = tiempoId;
        }
        
    }
}
