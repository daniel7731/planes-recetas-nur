using Moq;
using PlanesRecetas.application.Care;
using PlanesRecetas.domain.Care;
using PlanesRecetas.infraestructure.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Care
{
    public class GetAllCategoriasQueryHandlerTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly GetAllCategoriasQueryHandler _handler;

        public GetAllCategoriasQueryHandlerTests()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _handler = new GetAllCategoriasQueryHandler(_categoriaRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenCategoriasExist()
        {
            // Arrange
            var categoriasEntities = new List<domain.Care.Categoria>
        {
            new domain.Care.Categoria(Guid.NewGuid(), "Legumbres", 1),
            new domain.Care.Categoria(Guid.NewGuid(), "Cítricos", 2)
        };

            _categoriaRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(categoriasEntities);

            var query = new GetAllCategoriasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);

            // Verificamos que los datos se mapearon correctamente
            Assert.Equal(categoriasEntities[0].Nombre, result.Value[0].Nombre);
            Assert.Equal(categoriasEntities[0].TipoAlimentoId, result.Value[0].TipoAlimentoId);
            Assert.Equal(categoriasEntities[1].Nombre, result.Value[1].Nombre);

            _categoriaRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenListIsEmpty()
        {
            // Arrange
            _categoriaRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<domain.Care.Categoria>()); // Lista vacía

            var query = new GetAllCategoriasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.CategoriesNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _categoriaRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns((List<domain.Care.Categoria>?)null); // Retorno nulo

            var query = new GetAllCategoriasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.CategoriesNotFound, result.Error);
        }
    }
}
