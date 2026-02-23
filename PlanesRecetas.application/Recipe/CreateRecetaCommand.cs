using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe
{
    public class CreateRecetaCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string? Instrucciones { get; set; }
        public List<CreateRecetaIngredienteComand> Ingredientes { get; set; }
        public int TiempoId { get; set; }
    }
}
