
using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe.Evento
{
    public record IngredienteCreated : DomainEvent
    {
      
       
        public string Nombre { get; set; }
     
        public decimal Calorias { get; set; }
        
        public Guid CategoriaId { get; set; }
   
        public int UnidadId { get; set; }
        public IngredienteCreated(Guid Id, string nombre, decimal calorias, Guid categoriaId, int unidadId)
        {
            this.Id = Id;
            this.Nombre = nombre;
            this.Calorias = calorias;
            this.CategoriaId = categoriaId;
            this.UnidadId = unidadId;
        }
    }
}
