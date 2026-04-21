using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Template
{
    public interface IPlanTemplateRepository
    {
        public List<PlanTemplate> GetAllTemplates();
        public PlanTemplate? GetTemplateById(int id);

        public List<PlanTemplate> GetTemplatesByName(string name);

        public PlanTemplate CreateTemplate(PlanTemplate template);
    }
}
