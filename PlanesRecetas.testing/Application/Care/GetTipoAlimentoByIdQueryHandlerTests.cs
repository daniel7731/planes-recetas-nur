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
    public class GetTipoAlimentoByIdQueryHandlerTests
    {
        private readonly Mock<ITipoAlimentoRepository> _repoMock;
        private readonly GetTipoAlimentoByIdQueryHandler _handler;

        public GetTipoAlimentoByIdQueryHandlerTests()
        {
            _repoMock = new Mock<ITipoAlimentoRepository>();
            _handler = new GetTipoAlimentoByIdQueryHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenTipoAlimentoExists()
        {
            // Arrange
            int tipoAlimentoId = 1;
            var entity = new domain.Care.TipoAlimento(tipoAlimentoId, "Proteína");

            _repoMock
                .Setup(r => r.GetByIdAsync(tipoAlimentoId))
                .ReturnsAsync(entity);

            var query = new GetTipoAlimentoByIdQuery(tipoAlimentoId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(tipoAlimentoId, result.Value.Id);
            Assert.Equal("Proteína", result.Value.Nombre);

            _repoMock.Verify(r => r.GetByIdAsync(tipoAlimentoId), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenTipoAlimentoDoesNotExist()
        {
            // Arrange
            int tipoAlimentoId = 99;

            _repoMock
                .Setup(r => r.GetByIdAsync(tipoAlimentoId))
                .ReturnsAsync((domain.Care.TipoAlimento?)null);

            var query = new GetTipoAlimentoByIdQuery(tipoAlimentoId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.TipoAlimentoNotFound, result.Error);
            _repoMock.Verify(r => r.GetByIdAsync(tipoAlimentoId), Times.Once);
        }
    }
}
