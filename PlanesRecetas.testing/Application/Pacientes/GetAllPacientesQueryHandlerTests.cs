using Moq;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.application.Pacientes.PlanesRecetas.application.Persons;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errors = PlanesRecetas.application.Pacientes.Errors;

namespace PlanesRecetas.testing.Application.Pacientes
{
    public class GetAllPacientesQueryHandlerTests
    {
        private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
        private readonly GetAllPacientesQueryHandler _handler;

        public GetAllPacientesQueryHandlerTests()
        {
            _pacienteRepositoryMock = new Mock<IPacienteRepository>();
            _handler = new GetAllPacientesQueryHandler(_pacienteRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenPacientesExist()
        {
            // Arrange
            var pacientesEntities = new List<domain.Persons.Paciente>
        {
            new domain.Persons.Paciente(Guid.NewGuid(), "Juan", "Pérez", new DateTime(1990,1,1), 75, 1.75m),
            new domain.Persons.Paciente(Guid.NewGuid(), "Maria", "Lopez", new DateTime(1985,5,10), 60, 1.65m)
        };

            _pacienteRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(pacientesEntities);

            var query = new GetAllPacientesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Juan", result.Value[0].Nombre);
            Assert.Equal("Maria", result.Value[1].Nombre);
            _pacienteRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenListIsEmpty()
        {
            // Arrange
            _pacienteRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<domain.Persons.Paciente>()); // Lista vacía

            var query = new GetAllPacientesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.PacientesNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _pacienteRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns((List<domain.Persons.Paciente>?)null); // Retorno nulo

            var query = new GetAllPacientesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.PacientesNotFound, result.Error);
        }
    }
}
