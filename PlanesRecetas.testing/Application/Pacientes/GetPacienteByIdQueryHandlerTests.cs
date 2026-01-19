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
    public class GetPacienteByIdQueryHandlerTests
    {
        private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
        private readonly GetPacienteByIdQueryHandler _handler;

        public GetPacienteByIdQueryHandlerTests()
        {
            // 1. Arrange: Configuración común (Mocks)
            _pacienteRepositoryMock = new Mock<IPacienteRepository>();
            _handler = new GetPacienteByIdQueryHandler(_pacienteRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenPacienteExists()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            var pacienteEntity = new domain.Persons.Paciente(
                pacienteId,
                "Juan",
                "Pérez",
                new DateTime(1990, 1, 1),
                75.5m,
                1.80m
            );

            _pacienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(pacienteId, It.IsAny<bool>()))
                .ReturnsAsync(pacienteEntity);

            var query = new GetPacienteByIdQuery(pacienteId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(pacienteId, result.Value.Id);
            Assert.Equal("Juan", result.Value.Nombre);
            _pacienteRepositoryMock.Verify(x => x.GetByIdAsync(pacienteId, false), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenPacienteDoesNotExist()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();

            _pacienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(pacienteId, It.IsAny<bool>()))
                .ReturnsAsync((domain.Persons.Paciente?)null); // Retorna nulo para simular que no existe

            var query = new GetPacienteByIdQuery(pacienteId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.PacientesNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_RespectReadOnlyFlag()
        {
            // Arrange
            var pacienteId = Guid.NewGuid();
            var query = new GetPacienteByIdQuery(pacienteId, ReadOnly: true);

            // Use the constructor to create Paciente, since parameterless constructor does not exist
            _pacienteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), true))
                .ReturnsAsync(new domain.Persons.Paciente(
                    pacienteId,
                    "Test",
                    "ApellidoTest",
                    new DateTime(2000, 1, 1),
                    70.0m,
                    1.75m
                ));

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert
            // Verificamos que el repositorio recibió explícitamente el flag true
            _pacienteRepositoryMock.Verify(x => x.GetByIdAsync(pacienteId, true), Times.Once);
        }
    }
}
