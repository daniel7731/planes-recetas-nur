using Joseco.DDD.Core.Results;
using MediatR;


namespace PlanesRecetas.application.Plan.Query
{
    public sealed record GetRecetasByPlanAlimentarioQuery(Guid PlanId) : IRequest<Result<List<DietaRecetaDto>>>;
}
