using Joseco.DDD.Core.Abstractions;
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
    public class CreateCategoryHandlerTests
    {
        private readonly Mock<ICategoriaRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryHandlerTests()
        {
            _repositoryMock = new Mock<ICategoriaRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            // Initialize the handler with mocked dependencies
            _handler = new CreateCategoryHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_Should_CreateCategory_And_ReturnSuccessId()
        {
            TipoAlimento tipoAlimento = new TipoAlimento(1, "Comida");
            // Arrange
            var command = new CreateCategoryCommand
            {
                Id = Guid.NewGuid(),
                Nombre = "Postres",
                Tipo = tipoAlimento
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(command.Id, result.Value);

            // Verify that the repository's AddAsync was called exactly once
            _repositoryMock.Verify(
                x => x.AddAsync(It.Is<Categoria>(c => c.Id == command.Id && c.Nombre == command.Nombre)),
                Times.Once
            );

            // Verify that the Unit of Work committed the transaction
            _unitOfWorkMock.Verify(
                x => x.CommitAsync(cancellationToken),
                Times.Once
            );
        }
    }
}
