using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result<Guid>>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoria = new Categoria(request.Id, request.Nombre, request.Tipo);

            await _categoriaRepository.AddAsync(categoria);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(categoria.Id);
        }
    }
}
