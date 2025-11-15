using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Metrics
{
    public sealed record GetAllUnidadesQuery() : IRequest<Result<List<UnidadDto>>>;
}
