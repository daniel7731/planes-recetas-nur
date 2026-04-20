using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.application.Recipe;


namespace PlanesRecetas.domain.Recipe.Query
{
    public sealed record GetRecetaByIdQuery(Guid Id, bool ReadOnly = false , bool IncludeIngredientes = false) : IRequest<Result<RecetaDto>>;
}
