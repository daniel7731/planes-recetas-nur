using Joseco.DDD.Core.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Medicos
{
    public class CreateNutricionistaComand : IRequest<Result<Guid>>
    {
       
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }
        public CreateNutricionistaComand(string nombre, bool activo, DateTime fechaCreacion)
        {
            Nombre = nombre;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }
    }
}
