using PlanesRecetas.application.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Recipe
{
    using Joseco.DDD.Core.Abstractions;
    using Joseco.DDD.Core.Results;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using PlanesRecetas.domain.Care;
    using PlanesRecetas.domain.Metrics;
    using PlanesRecetas.domain.Recipe;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;




    public class CreateRecetaCommandHandler : IRequestHandler<CreateRecetaCommand, Result<Guid>>
    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITiempoRepository _tiempoRepository;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IRecetaRepository _recetaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateRecetaCommandHandler(IRecetaRepository recetaRepositor, 
            IIngredienteRepository ingredienteRepository,
            ICategoriaRepository categoriaRepository,
            ITiempoRepository tiempoRepository,
            IUnidadRepository unidadRepository,
            IUnitOfWork unitOfWork)
        {
            _recetaRepository = recetaRepositor;    
            _ingredienteRepository = ingredienteRepository;
            _tiempoRepository = tiempoRepository;
            _categoriaRepository = categoriaRepository;
            _unidadRepository = unidadRepository;
            _unitOfWork = unitOfWork;
        }

        // The method that handles the command execution
        public async Task<Result<Guid>> Handle(CreateRecetaCommand request, CancellationToken cancellationToken)
        {
            int tiempoId = request.TiempoId;
            var tiempo = await _tiempoRepository.GetById(tiempoId);
            if (tiempo is null)
                return Result.Failure<Guid>(Error.Failure("", $"Tiempo with Id '{request.TiempoId}' not found.", []));
            // 2. **Map Command to Entity:** // Create the Domain Entity (Receta) from the command data.
            

            var newReceta = new Receta(request.Id); // Assuming 'Receta' is your Entity class
            newReceta.Nombre = request.Nombre;
            newReceta.TiempoId = request.TiempoId;
            newReceta.Instrucciones = request.Instrucciones;
           
            // 3. **Persistence:**
            // Add the new entity to the data context and save changes.
            await _recetaRepository.AddAsync(newReceta);
          
             List<RecetaIngrediente> ingredientes = request.Ingredientes.Select(incrediente => new RecetaIngrediente()
             {
                 RecetaId = newReceta.Id,
                 IngredienteId = incrediente.IngredienteId,
                 CantidadValor = incrediente.CantidadValor
             }).ToList();
            // Add related Ingredientes (example relationship setup)
            await _recetaRepository.AddIngredientes(newReceta, ingredientes);
            await _unitOfWork.CommitAsync(cancellationToken);

            // 4. **Return Result:**
            // Return a successful result containing the Id of the newly created entity.
            return Result.Success(newReceta.Id);
        }
    }
}
