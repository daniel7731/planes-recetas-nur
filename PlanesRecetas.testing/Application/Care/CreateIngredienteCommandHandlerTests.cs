using Joseco.DDD.Core.Abstractions;
using Moq;
using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Care
{
    public class CreateIngredienteCommandHandlerTests
    {
        private readonly Mock<IIngredienteRepository> _ingredienteRepoMock;
        private readonly Mock<ICategoriaRepository> _categoriaRepoMock;
        private readonly Mock<IUnidadRepository> _unidadRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateIngredienteCommandHandler _handler;

        public CreateIngredienteCommandHandlerTests()
        {
            _ingredienteRepoMock = new Mock<IIngredienteRepository>();
            _categoriaRepoMock = new Mock<ICategoriaRepository>();
            _unidadRepoMock = new Mock<IUnidadRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new CreateIngredienteCommandHandler(
                _ingredienteRepoMock.Object,
                _categoriaRepoMock.Object,
                _unidadRepoMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesAndSavesIngredienteAndReturnsId()
        {
            // Arrange
            var ingredienteId = Guid.NewGuid();
            var categoriaId = Guid.NewGuid();
            var unidadId = 1;
            var cantidaValor = 100;
            var calorias = 150.5m;
            string nombre = "Pollo";
            var command = new CreateIngredienteCommand
            {
                Id = ingredienteId,
                Calorias = calorias,
                Nombre = nombre,
                CantidadValor = cantidaValor,
                CategoriaId = categoriaId,
                UnidadId = unidadId
            };

            // Setup validations for foreign keys
            _categoriaRepoMock
                .Setup(r => r.GetByIdAsync(categoriaId, It.IsAny<bool>()))
                .ReturnsAsync(new domain.Care.Categoria(categoriaId, "Proteinas", 1));

            _unidadRepoMock
                .Setup(r => r.GetUnidad(unidadId))
                .ReturnsAsync(new domain.Metrics.Unidad(unidadId, "Gramos", "g"));

            domain.Care.Ingrediente capturedIngrediente = null;

            // Setup the mock repository to capture the entity passed to AddAsync
            _ingredienteRepoMock
                .Setup(r => r.AddAsync(It.IsAny<domain.Care.Ingrediente>()))
                .Callback<domain.Care.Ingrediente>(i => capturedIngrediente = i)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            // 1. Verify the result is successful and contains the correct GUID
            Assert.True(result.IsSuccess);
            Assert.Equal(ingredienteId, result.Value);

            // 2. Verify the Ingrediente was created with the correct data
            Assert.NotNull(capturedIngrediente);
            Assert.Equal(command.Nombre, capturedIngrediente.Nombre);
            Assert.Equal(command.Calorias, capturedIngrediente.Calorias);
            Assert.Equal(command.CategoriaId, capturedIngrediente.CategoriaId);
            Assert.Equal(command.UnidadId, capturedIngrediente.UnidadId);

            // 3. Verify interaction with dependencies
            _categoriaRepoMock.Verify(r => r.GetByIdAsync(categoriaId, It.IsAny<bool>()), Times.Once);
            _unidadRepoMock.Verify(r => r.GetUnidad(unidadId), Times.Once);
            _ingredienteRepoMock.Verify(r => r.AddAsync(It.IsAny<domain.Care.Ingrediente>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCategoria_ReturnsFailure()
        {
            // Arrange
            var command = new CreateIngredienteCommand
            {
                Id = Guid.NewGuid(),
                Calorias = 100,
                Nombre = "Test",
                CantidadValor = 1,
                CategoriaId = Guid.NewGuid(),
                UnidadId = 1
            };

            _categoriaRepoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync((domain.Care.Categoria)null); // Category not found

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Contains("Category with Id", result.Error.Description);
            _ingredienteRepoMock.Verify(r => r.AddAsync(It.IsAny<domain.Care.Ingrediente>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
