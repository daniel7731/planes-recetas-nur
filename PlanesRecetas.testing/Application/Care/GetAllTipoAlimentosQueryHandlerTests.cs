using Joseco.DDD.Core.Results;
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
    public class GetAllTipoAlimentosQueryHandlerTests
    {
        private readonly Mock<ITipoAlimentoRepository> _repoMock;
        private readonly GetAllTipoAlimentosQueryHandler _handler;

        public GetAllTipoAlimentosQueryHandlerTests()
        {
            _repoMock = new Mock<ITipoAlimentoRepository>();
            _handler = new GetAllTipoAlimentosQueryHandler(_repoMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenItemsExist()
        {
            // Arrange
            var entidades = new List<domain.Care.TipoAlimento>
        {
            new domain.Care.TipoAlimento(1, "Verdura"),
            new domain.Care.TipoAlimento(2, "Fruta")
        };

            _repoMock
                .Setup(r => r.GetAll())
                .Returns(entidades);

            var query = new GetAllTipoAlimentosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Contains(result.Value, x => x.Nombre == "Verdura");
            Assert.Contains(result.Value, x => x.Nombre == "Fruta");
            _repoMock.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetAll())
                .Returns(new List<domain.Care.TipoAlimento>());

            var query = new GetAllTipoAlimentosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.TiposAlimentoNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetAll())
                .Returns((List<domain.Care.TipoAlimento>?)null);

            var query = new GetAllTipoAlimentosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.TiposAlimentoNotFound, result.Error);
        }
    }
}
