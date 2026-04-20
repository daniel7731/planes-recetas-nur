using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanesRecetas.domain.Recipe
{
    public interface IRecetaIngredienteRepository 
    {
        public List<RecetaIngrediente> GetRecetaIngredientes(Guid idReceta);
    }
}
