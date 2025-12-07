using Joseco.DDD.Core.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace PlanesRecetas.application.Medicos
{
    public class CreateNutricionistaComand : IRequest<Result<Guid>>
    {
       
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public Guid Guid { get; set; }
        public DateTime FechaCreacion { get; set; }
        public CreateNutricionistaComand(string nombre, bool activo, DateTime fechaCreacion)
        {
            Nombre = nombre;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }
        [JsonConstructor]
        public CreateNutricionistaComand(Guid guid, string nombre, bool activo, DateTime fechaCreacion)
        {
            Guid = guid;
            Nombre = nombre;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }
    }
}
