using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Recipe;


namespace PlanesRecetas.domain.Recipe.Query
{
    public sealed record GetAllRecetasQuery() :IRequest<Result<List<RecetaDto>>>
    {
    }
}
