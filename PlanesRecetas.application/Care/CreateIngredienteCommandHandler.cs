using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class CreateIngredienteCommandHandler
        : IRequestHandler<CreateIngredienteCommand, Result<Guid>>
    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateIngredienteCommandHandler(
            IIngredienteRepository ingredienteRepository,
            ICategoriaRepository categoriaRepository,
            IUnidadRepository unidadRepository,
            IUnitOfWork unitOfWork)
        {
            _ingredienteRepository = ingredienteRepository;
            _categoriaRepository = categoriaRepository;
            _unidadRepository = unidadRepository;
            _unitOfWork = unitOfWork;
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
 
            await _ingredienteRepository.AddAsync(ingrediente);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(ingrediente.Id);
        }
    }
}
