using Moq;
using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Care
{
    public class GetAllIngredientesQueryHandlerTests
    {
        private readonly Mock<IIngredienteRepository> _ingredienteRepositoryMock;
        private readonly GetAllIngredientesQueryHandler _handler;

        public GetAllIngredientesQueryHandlerTests()
        {
            _ingredienteRepositoryMock = new Mock<IIngredienteRepository>();
            _handler = new GetAllIngredientesQueryHandler(_ingredienteRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenIngredientesExist()
        {
            // Arrange
            var ingredientesEntities = new List<domain.Care.Ingrediente>
        {
            new domain.Care.Ingrediente(
                Guid.NewGuid(),
                150.5m,
                "Pechuga de Pollo",
                Guid.NewGuid(),
                100,
                1),
            new domain.Care.Ingrediente(
                Guid.NewGuid(),
                52.0m,
                "Manzana",
                Guid.NewGuid(),
                1,
                2)
        };

            _ingredienteRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(ingredientesEntities);

            var query = new GetAllIngredientesQuery();

            // Act
            // Aunque el handler usa Task.FromResult, consumimos con await por la firma de MediatR
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);

            // Verificamos el mapeo de propiedades clave del primer elemento
            var firstDto = result.Value[0];
            Assert.Equal(ingredientesEntities[0].Id, firstDto.Id);
            Assert.Equal(ingredientesEntities[0].Nombre, firstDto.Nombre);
            Assert.Equal(ingredientesEntities[0].Calorias, firstDto.Calorias);
            Assert.Equal(ingredientesEntities[0].CategoriaId, firstDto.CategoriaId);
            Assert.Equal(ingredientesEntities[0].UnidadId, firstDto.UnidadId);

            _ingredienteRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenListIsEmpty()
        {
            // Arrange
            _ingredienteRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<domain.Care.Ingrediente>()); // Lista instanciada pero vacía

            var query = new GetAllIngredientesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.IngredientesNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _ingredienteRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns((List<domain.Care.Ingrediente>?)null); // Retorno nulo

            var query = new GetAllIngredientesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.IngredientesNotFound, result.Error);
        }
    }
}
