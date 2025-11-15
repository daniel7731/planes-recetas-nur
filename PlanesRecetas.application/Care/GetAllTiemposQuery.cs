using Joseco.DDD.Core.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public sealed record GetAllTiemposQuery() : IRequest<Result<List<TiempoDto>>>;
}
