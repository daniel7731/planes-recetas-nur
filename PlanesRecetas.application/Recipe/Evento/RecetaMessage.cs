using PlanesRecetas.application.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe.Evento
{
    public class RecetaMessage : IntegrationMessage
    {
        public Guid RecetaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Instrucciones { get; set; } = string.Empty;
        public int TiempoId { get; set; }
        public List<MessageItemIngredient> IngredientesId { get; set; } = new List<MessageItemIngredient>();
    }
}
