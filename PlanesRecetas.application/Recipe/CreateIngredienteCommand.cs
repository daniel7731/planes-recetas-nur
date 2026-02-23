using Joseco.DDD.Core.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe
{
    public class CreateIngredienteCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public decimal Calorias { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadValor { get; set; }
        public Guid CategoriaId { get; set; }
        public int UnidadId { get; set; }
    }
}
