using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe
{
    public class Errors
    {
        public static Error RecetasNotFound= new (
           "Recetas.NotFound",
           "No Recetas were found in the system.",
           ErrorType.NotFound
       );
        public static Error IngredientesNotFound = new (
           "Ingredientes.NotFound",
           "No Ingredientes were found in the system.",
           ErrorType.NotFound
       );

        public static Error RecetaNotFound = new(
           "Recetas.NotFound",
           "No Recetas were found in the system.",
           ErrorType.NotFound
       );
    }
}
