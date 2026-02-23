using Moq;
using PlanesRecetas.application.Metrics;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Metrics
{
    public class GetAllUnidadesQueryHandlerTests
    {
        private readonly Mock<IUnidadRepository> _unidadRepositoryMock;
        private readonly GetAllUnidadesMedidaQueryHandler _handler;

        public GetAllUnidadesQueryHandlerTests()
        {
            _unidadRepositoryMock = new Mock<IUnidadRepository>();
            _handler = new GetAllUnidadesMedidaQueryHandler(_unidadRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenUnidadesExist()
        {
            // Arrange
            var unidadesEntities = new List<domain.Metrics.UnidadMedida>
        {
            new domain.Metrics.UnidadMedida(1, "Gramos", "g"),
            new domain.Metrics.UnidadMedida(2, "Mililitros", "ml")
        };

            _unidadRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(unidadesEntities);

            var query = new GetAllUnidadesMedidaQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);

            // Verificamos el mapeo de propiedades incluyendo el Símbolo
            Assert.Equal("Gramos", result.Value[0].Nombre);
            Assert.Equal("g", result.Value[0].Simbolo);
            Assert.Equal(2, result.Value[1].Id);
            Assert.Equal("ml", result.Value[1].Simbolo);

            _unidadRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenListIsEmpty()
        {
            // Arrange
            _unidadRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<domain.Metrics.UnidadMedida>());

            var query = new GetAllUnidadesMedidaQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.UnidadesNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _unidadRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns((List<domain.Metrics.UnidadMedida>?)null);

            var query = new GetAllUnidadesMedidaQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.UnidadesNotFound, result.Error);
        }
    }
}
