using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;
using MediatR;
using PlanesRecetas.application.Recipe.Evento;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using PlanesRecetas.domain.Recipe;

namespace PlanesRecetas.application.Recipe
{
    public class CreateIngredienteCommandHandler
        : IRequestHandler<CreateIngredienteCommand, Result<Guid>>
    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IUnitOfWork _unitOfWork;
       // private readonly IOu
        //private readonly IExternalPublisher _externalPublisher;
        private readonly IOutboxService<DomainEvent> _outboxService;

        public CreateIngredienteCommandHandler(
            IIngredienteRepository ingredienteRepository,
            ICategoriaRepository categoriaRepository,
            IUnidadRepository unidadRepository,
            IUnitOfWork unitOfWork , 
            IOutboxService<DomainEvent> outboxService)        {
            _ingredienteRepository = ingredienteRepository;
            _categoriaRepository = categoriaRepository;
            _unidadRepository = unidadRepository;
            _unitOfWork = unitOfWork;
            _outboxService = outboxService;

        }

        public async Task<Result<Guid>> Handle(CreateIngredienteCommand request, CancellationToken cancellationToken)
        {
            // Validate Categoria
            var categoria = await _categoriaRepository.GetByIdAsync(request.CategoriaId);
            if (categoria is null)
                return Result.Failure<Guid>(Error.Failure("",$"Category with Id '{request.CategoriaId}' not found.", []));

            // Validate Unidad
            var unidad = await _unidadRepository.GetUnidad(request.UnidadId);
            if (unidad is null)
                return Result.Failure<Guid>(Error.Failure("",$"Unit with Id '{request.UnidadId}' not found.", []));

            // Create Ingrediente entity
            var ingrediente = new Ingrediente(request.Id, request.Calorias, request.Nombre,
            request.CategoriaId, request.CantidadValor, request.UnidadId);
            ingrediente.Unidad = null;
            ingrediente.Categoria = null;
            ingrediente.SetDomainEvent();
            IngredienteMessage ingredienteCreated = new IngredienteMessage
            {
                IngredienteId = ingrediente.Id,
                Nombre = ingrediente.Nombre,
                Calorias = ingrediente.Calorias,
                CategoriaId = ingrediente.CategoriaId,
                UnidadId = ingrediente.UnidadId
            };
            var outboxMessage = new OutboxMessage<DomainEvent>
                (new IngredienteCreated(ingrediente.Id, ingrediente.Nombre,
                ingrediente.Calorias, ingrediente.CategoriaId, ingrediente.UnidadId));
           
            await _outboxService.AddAsync(outboxMessage);

            await _ingredienteRepository.AddAsync(ingrediente);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(ingrediente.Id);
        }
    }
}
