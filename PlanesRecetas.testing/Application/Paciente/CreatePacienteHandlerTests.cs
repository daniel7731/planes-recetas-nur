using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using Moq;
using PlanesRecetas.application.Pacientes;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Paciente
{
    public class CreatePacienteHandlerTests
    {
        private readonly Mock<IPacienteRepository> _mockPacienteRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CreatePacienteHandler _handler;

        // Setup common to all tests
        public CreatePacienteHandlerTests()
        {
            _mockPacienteRepository = new Mock<IPacienteRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Inject the mocked dependencies into the handler
            _handler = new CreatePacienteHandler(
                _mockPacienteRepository.Object,
                _mockUnitOfWork.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCreatePaciente_CallRepositoryAdd_CallUnitOfWorkCommit_AndReturnSuccessResult()
        {
            // Arrange
            // 1. Create el comando
            var command = new CreatePacienteComand(
                nombre: "Juan",
                apellido: "Perez",
                fechaNacimiento: new DateTime(1990, 5, 15), 
                peso: 75.5m,
                altura: 1.78m
            );


            // 2. Set up a placeholder for the Paciente object that will be passed to AddAsync
            PlanesRecetas.domain.Persons.Paciente capturedPaciente = new PlanesRecetas.domain.Persons.Paciente(
                    command.Nombre, command.Apellido, command.FechaNacimiento, command.Peso, command.Altura);
       
            // 3. Configure the mock repository to capture the Paciente object passed to AddAsync
            _mockPacienteRepository
                .Setup(r => r.AddAsync(It.IsAny<PlanesRecetas.domain.Persons.Paciente>()))
                .Callback<PlanesRecetas.domain.Persons.Paciente>(p => capturedPaciente = p) 
                .Returns( Task.FromResult(capturedPaciente.Id));

            // Act
            // Call the handler's Handle method
            var result = await ((IRequestHandler<CreatePacienteComand, Result<Guid>>)_handler)
                .Handle(command, CancellationToken.None);
       
            // Assert
            // 1. Verify the result is successful and contains a Guid
            Assert.True(result.IsSuccess);
 
            // 2. Verify the Paciente object was created correctly
            Assert.NotNull(capturedPaciente);
            Assert.Equal(command.Nombre, capturedPaciente.Nombre);
            Assert.Equal(command.Apellido, capturedPaciente.Apellido);
            Assert.Equal(command.FechaNacimiento, capturedPaciente.FechaNacimiento);
            Assert.Equal(command.Peso, capturedPaciente.Peso);
            Assert.Equal(command.Altura, capturedPaciente.Altura);


            // 3. Verify that AddAsync was called exactly once with the created Paciente object
            _mockPacienteRepository.Verify(
                r => r.AddAsync(It.IsAny<PlanesRecetas.domain.Persons.Paciente>()),
                Times.Once,
                "AddAsync should have been called once on the repository."
            );

            // 4. Verify that CommitAsync
            _mockUnitOfWork.Verify(
                u => u.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Once,
                "CommitAsync should have been called once on the unit of work."
            );
        }       
    }
}
