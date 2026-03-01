using Joseco.Communication.External.Contracts.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe.Evento
{
    public record IngredienteMessage : IntegrationMessage
    {
        public Guid IngredienteId { get; set; }
       
        public string Nombre { get; set; }
     
        public decimal Calorias { get; set; }
        
        public Guid CategoriaId { get; set; }
   
        public int UnidadId { get; set; }
        
    }
}
