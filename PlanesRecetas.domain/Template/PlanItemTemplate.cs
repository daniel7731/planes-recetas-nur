using PlanesRecetas.domain.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Template
{
    public class PlanItemTemplate
    {
        public int Id { get; set; }
        public int PlanTemplateId { get; set; }
        public int NumeroDia { get; set; } = 0;
        public Guid RecetaId { get; set; } = Guid.NewGuid();

        public Receta Receta { get; set; }
    }
}
