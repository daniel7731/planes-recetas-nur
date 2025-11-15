using Joseco.DDD.Core.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes
{
    public class CreatePacienteComand : IRequest<Result<Guid>>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Decimal Peso { get; set; }
        public Decimal Altura { get; set; }
        
    }
}
