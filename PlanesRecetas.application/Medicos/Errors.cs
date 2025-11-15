using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Medicos
{
    public static class Errors
    {
      

        // Define the specific error for this case
        public static readonly Error NutricionistasNotFound = new(
            "Nutricionistas.NotFound",
            "No nutricionistas were found in the system.",
            ErrorType.NotFound
        );
        public static readonly Error NutricionistaNotFound = new(
           "Nutricionista.NotFound",
           "No nutricionista was found in the system.",
           ErrorType.NotFound
       );
       public static readonly Error PacientesNotFound = new(
            "Pacientes.NotFound",
            "No Pacientes were found in the system.",
            ErrorType.NotFound
        );
        public static readonly Error PacienteNotFound = new(
           "Paciente.NotFound",
           "No Paciente was found in the system.",
           ErrorType.NotFound
       );

    }
}
