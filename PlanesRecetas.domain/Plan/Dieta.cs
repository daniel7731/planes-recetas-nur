using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Recipe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Plan
{
    public class Dieta : AggregateRoot
    {
  
        public Guid PlanAlimentacionId { get; set; }
        public DateTime FechaConsumo { get; set; } 
       
        public Dieta(Guid id, DateTime fechaConsumo, Guid planAlimentacionId) : base(id)
        {
            FechaConsumo = fechaConsumo;
            PlanAlimentacionId = planAlimentacionId;
        }
    }
}
