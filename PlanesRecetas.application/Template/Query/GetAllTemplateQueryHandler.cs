using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Template;



namespace PlanesRecetas.application.Template.Query
{
    public class GetAllTemplateQueryHandler : IRequestHandler<GetAllTemplateQuery, Result<List<PlanTemplate>>>
    {
        private readonly IPlanTemplateRepository _repository;

        public GetAllTemplateQueryHandler(IPlanTemplateRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<PlanTemplate>>> Handle(GetAllTemplateQuery request, CancellationToken cancellationToken)
        {
            var templates = _repository.GetAllTemplates();
            return Result.Success(templates);
        }
    }
}
