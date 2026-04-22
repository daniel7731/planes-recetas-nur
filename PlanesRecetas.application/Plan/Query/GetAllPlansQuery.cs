using Joseco.DDD.Core.Results;
using MediatR;

namespace PlanesRecetas.application.Plan.Query
{
    public sealed record GetAllPlansQuery : IRequest<Result<List<PlanAlimentacionDto>>>
    {
    }
}
