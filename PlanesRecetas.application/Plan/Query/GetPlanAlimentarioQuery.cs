using Joseco.DDD.Core.Results;
using MediatR;


namespace PlanesRecetas.application.Plan.Query
{
    public sealed record GetPlanAlimentaryQuery(Guid Id, bool ReadOnly = false) : IRequest<Result<List<PlanAlimentacionDto>>>;
}
