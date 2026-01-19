using Moq;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Pacientes
{
    public class ExistsPacienteQueryHandlerTests
    {
        private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
        private readonly ExistsPacienteQueryHandler _handler;

        public ExistsPacienteQueryHandlerTests()
        {
            _pacienteRepositoryMock = new Mock<IPacienteRepository>();
            _handler = new ExistsPacienteQueryHandler(_pacienteRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnTrue_WhenPacienteExists()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            _pacienteRepositoryMock
                .Setup(repo => repo.ExistsAsync(pacienteId))
                .ReturnsAsync(true);

            var query = new ExistsPacienteQuery(pacienteId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            _pacienteRepositoryMock.Verify(repo => repo.ExistsAsync(pacienteId), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFalse_WhenPacienteDoesNotExist()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            _pacienteRepositoryMock
                .Setup(repo => repo.ExistsAsync(pacienteId))
                .ReturnsAsync(false);

            var query = new ExistsPacienteQuery(pacienteId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess); // Sigue siendo un éxito de ejecución
            Assert.False(result.Value);    // El valor contenido es false
            _pacienteRepositoryMock.Verify(repo => repo.ExistsAsync(pacienteId), Times.Once);
        }
    }
}
