using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes
{
    public static class Errors
    {
        public static readonly Error PacientesNotFound = new(
           "Pacientes.NotFound",
           "No Pacientes were found in the system.",
           ErrorType.NotFound
       );
       public static readonly Error PacienteNotCreated = new(
           "Paciente.NotCreated",
           "The specified Paciente was not created.",
           ErrorType.Failure
       );
    }
}
