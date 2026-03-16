using PlanesRecetas.application.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe.Evento
{
    public class IngredienteMessage : IntegrationMessage
    {
        public Guid IngredienteId { get; set; }

        public string Nombre { get; set; }

        public decimal Calorias { get; set; }

        public Guid CategoriaId { get; set; }

        public int UnidadId { get; set; }

    }
}
