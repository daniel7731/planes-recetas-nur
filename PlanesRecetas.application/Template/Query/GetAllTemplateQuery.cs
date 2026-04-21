

using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Plan;
using PlanesRecetas.domain.Template;

namespace PlanesRecetas.application.Template.Query
{
    public sealed record GetAllTemplateQuery : IRequest<Result<List<PlanTemplate>>>
    {
    }
}
