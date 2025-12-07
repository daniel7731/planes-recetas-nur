using Joseco.DDD.Core.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace PlanesRecetas.application.Pacientes
{
    public class CreatePacienteComand : IRequest<Result<Guid>>
    {
        public Guid Guid { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Decimal Peso { get; set; }
        public Decimal Altura { get; set; }
        [JsonConstructor]
        public CreatePacienteComand(Guid guid, string nombre, string apellido, DateTime fechaNacimiento, Decimal peso, Decimal altura)
        {
            Guid = guid;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Peso = peso;
            Altura = altura;
        }
    }
}
