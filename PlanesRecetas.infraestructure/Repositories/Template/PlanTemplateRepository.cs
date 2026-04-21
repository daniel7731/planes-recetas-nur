using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Template;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Template
{
    public class PlanTemplateRepository : IPlanTemplateRepository
    {
        private readonly DomainDbContext _dbContext;
        public PlanTemplateRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public PlanTemplate CreateTemplate(PlanTemplate template)
        {
            _dbContext.PlanTemplates.Add(template);
            _dbContext.SaveChanges();
            return template;
        }

        public List<PlanTemplate> GetAllTemplates()
        {
            return _dbContext.PlanTemplates.
                 Include(pt => pt.Items)
                 .ThenInclude(pi => pi.Receta)
                .ToList();
        }

        public PlanTemplate? GetTemplateById(int id)
        {
            return _dbContext.PlanTemplates
                .Include(pt => pt.Items)
                .ThenInclude(pi => pi.Receta)
                .FirstOrDefault(pt => pt.Id == id);
        }

        public List<PlanTemplate> GetTemplatesByName(string name)
        {
            return _dbContext.PlanTemplates
                .Include(pt => pt.Items)
                .ThenInclude(pi => pi.Receta)
                .Where(pt => pt.Nombre.Contains(name))
                .ToList();
        }
    }
}
