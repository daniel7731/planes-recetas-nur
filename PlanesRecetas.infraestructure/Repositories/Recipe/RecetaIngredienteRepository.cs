using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Recipe
{
    public class RecetaIngredienteRepository : IRecetaIngredienteRepository
    {
        private readonly DomainDbContext _dbContext;
        public RecetaIngredienteRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<RecetaIngrediente> GetRecetaIngredientes(Guid idReceta)
        {
            return _dbContext.RecetaIngrediente.Where(ri => ri.RecetaId == idReceta)
                .Include(ri => ri.Ingrediente)
                .Include(ri => ri.Receta)
                .ToList();
        }
    }
}
