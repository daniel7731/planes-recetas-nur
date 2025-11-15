using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.Care
{
    public sealed record GetAllCategoriasQuery() : IRequest<Result<List<CategoriaDto>>>;
}
