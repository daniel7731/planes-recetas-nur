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
    public class GetAllTiemposQueryHandlerTests
    {
        private readonly Mock<ITiempoRepository> _tiempoRepositoryMock;
        private readonly GetAllTiemposQueryHandler _handler;

        public GetAllTiemposQueryHandlerTests()
        {
            _tiempoRepositoryMock = new Mock<ITiempoRepository>();
            _handler = new GetAllTiemposQueryHandler(_tiempoRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenTiemposExist()
        {
            // Arrange
            var tiemposEntities = new List<domain.Care.Tiempo>
        {
            new domain.Care.Tiempo(1, "Breakfast"),
            new domain.Care.Tiempo(3, "Lunch")
        };

            _tiempoRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(tiemposEntities);

            var query = new GetAllTiemposQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);

            // Verificamos que los datos se mapearon correctamente usando el constructor
            Assert.Equal(1, result.Value[0].Id);
            Assert.Equal("Breakfast", result.Value[0].Nombre);
            Assert.Equal(3, result.Value[1].Id);
            Assert.Equal("Lunch", result.Value[1].Nombre);

            _tiempoRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenListIsEmpty()
        {
            // Arrange
            _tiempoRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<domain.Care.Tiempo>()); // Lista vacía

            var query = new GetAllTiemposQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.TiemposNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _tiempoRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns((List<domain.Care.Tiempo>?)null); // Retorno nulo

            var query = new GetAllTiemposQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.TiemposNotFound, result.Error);
        }
    }
}
