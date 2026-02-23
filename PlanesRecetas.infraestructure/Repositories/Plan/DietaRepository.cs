using Microsoft.EntityFrameworkCore;
using PlanesRecetas.domain.Plan;
using PlanesRecetas.domain.Recipe;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Repositories.Plan
{
    public class DietaRepository : IDietaRepository
    {
        private readonly DomainDbContext _dbContext;
        public DietaRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Dieta entity)
        {
            await _dbContext.Dieta.AddAsync(entity);
        }
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Dieta> GetDietasPlan(Guid planId)
        {
            //  return _dbContext.DietaReceta.Join( ).Where(d => d.PlanAlimentacionId == planId).ToList();
          

            var query = from dr in _dbContext.DietaReceta
                        join d in _dbContext.Dieta on dr.DietaId equals d.Id into dietGroup
                        from d in dietGroup.DefaultIfEmpty() // This makes it a LEFT JOIN

                        join pl in _dbContext.PlanAlimentacion on d.PlanAlimentacionId equals pl.Id into planGroup
                        from pl in planGroup.DefaultIfEmpty() // Another LEFT JOIN

                        join r in _dbContext.Receta on dr.RecetaId equals r.Id into recetaGroup
                        from r in recetaGroup.DefaultIfEmpty()

                        join t in _dbContext.Tiempo on dr.TiempoId equals t.Id into tiempoGroup
                        from t in tiempoGroup.DefaultIfEmpty()

                        where d.PlanAlimentacionId == planId
                        select new Dieta(d.Id,d.FechaConsumo,planId){
           
                        
                        };

            var results = query.ToList();
            return results;
        }

        public Task<Dieta?> GetByIdAsync(Guid id, bool readOnly = false)
        {
            return Task.FromResult(_dbContext.Dieta.FirstOrDefault(i => i.Id == id));
        }



        public Task UpdateAsync(Dieta dieta)
        {
            _dbContext.Dieta.Update(dieta);
            return Task.CompletedTask;
        }

        public async Task AddDietaReceta(Dieta dieta, DietaReceta dietaReceta)
        {
            await _dbContext.DietaReceta.AddAsync(dietaReceta);
        }

        public List<DietaReceta> GetDietaRecetas(Guid dietaId)
        {
            // return _dbContext.DietaReceta.Where(dr => dr.DietaId == dietaId).ToList();
            var query = from dr in _dbContext.DietaReceta

                            // Left Join Receta
                        join r in _dbContext.Receta on dr.RecetaId equals r.Id into recetaGroup
                        from r in recetaGroup.DefaultIfEmpty()

                        join d in _dbContext.Dieta on dr.DietaId equals d.Id into dietaGroup
                        from d in dietaGroup.DefaultIfEmpty()

                            // Left Join Tiempo
                        join t in _dbContext.Tiempo on dr.TiempoId equals t.Id into tiempoGroup
                        from t in tiempoGroup.DefaultIfEmpty()

                        where dr.DietaId == dietaId
                        select new DietaReceta
                        {
                            Id = dr.Id,
                            Orden = dr.Orden,
                            TiempoId = dr.TiempoId,
                            DietaId = dr.DietaId,
                            Dieta = new Dieta(dr.DietaId, d.FechaConsumo, d.PlanAlimentacionId)
                            {

                            },
                            Receta = new Receta(r.Id, r.Nombre, r.Tiempo)
                            {
                                Instrucciones = r.Instrucciones,
                                TiempoId = r.TiempoId,
                                
                            }
                        };

            List<DietaReceta> dietaRecetas = query.ToList();
            return dietaRecetas;
        }
    }
}
