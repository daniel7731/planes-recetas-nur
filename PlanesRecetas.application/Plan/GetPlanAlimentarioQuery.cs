using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Plan
{
    public sealed record GetPlanAlimentaryQuery(Guid Id, bool ReadOnly = false) : IRequest<Result<List<PlanAlimentacionDto>>>;
}
