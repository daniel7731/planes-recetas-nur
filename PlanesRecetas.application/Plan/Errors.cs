using Joseco.DDD.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public static class Errors
    {
        public static readonly Error PlanNotFound = new(
           "PlanNotFound.NotFound",
           "No Plan were found in the system.",
           ErrorType.NotFound
       );
    }
}
