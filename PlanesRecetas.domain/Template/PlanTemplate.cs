using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Template
{
    public class PlanTemplate
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int  Dias { get; set; }

        public List<PlanItemTemplate> Items { get; set; } = new List<PlanItemTemplate>();
    }
}
