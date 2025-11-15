using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Metrics
{
    public static class Errors
    {
        // Existing Errors... (Nutricionista, Paciente, Category, Ingrediente, TipoAlimento)

        // --- New Errors for Unidad (Unit of Measurement) ---

        public static readonly Error UnidadesNotFound = new(
            "Unidades.NotFound",
            "No units of measurement were found in the system.",
            ErrorType.NotFound
        );
        public static readonly Error UnidadNotFound = new(
            "Unidad.NotFound",
            "The specified unit of measurement was not found in the system.",
            ErrorType.NotFound
        );
    }
}
