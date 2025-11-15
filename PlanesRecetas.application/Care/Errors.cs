using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public static class Errors
    {


        public static readonly Error CategoriesNotFound = new(
        "Categories.NotFound",
        "No food categories were found in the system.",
        ErrorType.NotFound
    );
        public static readonly Error CategoryNotFound = new(
            "Category.NotFound",
            "The specified food category was not found in the system.",
            ErrorType.NotFound
        );

        // --- New Errors for Ingrediente (Ingredient) ---

        public static readonly Error IngredientesNotFound = new(
            "Ingredientes.NotFound",
            "No ingredients were found in the system.",
            ErrorType.NotFound
        );
        public static readonly Error IngredienteNotFound = new(
            "Ingrediente.NotFound",
            "The specified ingredient was not found in the system.",
            ErrorType.NotFound
        );

        // --- New Errors for TipoAlimento (Food Type) ---

        public static readonly Error TiposAlimentoNotFound = new(
            "TiposAlimento.NotFound",
            "No food types were found in the system.",
            ErrorType.NotFound
        );
        public static readonly Error TipoAlimentoNotFound = new(
            "TipoAlimento.NotFound",
            "The specified food type was not found in the system.",
            ErrorType.NotFound
        );

        public static Error TiemposNotFound = new(
            "Tiempos.NotFound",
            "No tiempos were found in the system.",
            ErrorType.NotFound
        );
    }
}
