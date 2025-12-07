using Joseco.DDD.Core.Abstractions;
using Joseco.DDD.Core.Results;
using MediatR;
using Moq;
using PlanesRecetas.application.Medicos;
using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Medicos
{
    public class CreateNutricionstaHandlerTests
    {
        
       
        /// </summary>
        [Fact]
        public async Task Handle_ValidCommand_CreatesAndSavesNutricionistaAndReturnsId()
        {
            // Arrange
            var mockRepository = new Mock<INutricionistaRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            Guid gui = Guid.NewGuid();
            // Data for the command
            var command = new CreateNutricionistaComand(
                guid : gui,
                nombre: "Dr. Smith",
                activo: true,
                fechaCreacion: DateTime.UtcNow
            );

            // The handler being tested
            var handler = new CreateNutricionstaHandler(
                mockRepository.Object,
                mockUnitOfWork.Object
            );
           

            Nutricionista capturedNutricionista = null;
                //new Nutricionista(gui, command.Nombre,command.Activo,command.FechaCreacion);

            // Setup the mock repository to capture the entity passed to AddAsync
            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<Nutricionista>()))
                .Callback<Nutricionista>(n => capturedNutricionista = new Nutricionista(gui,n.Nombre,
                    n.Activo,n.FechaCreacion)) // Capture the instance
                .Returns(Task.FromResult(gui));
           
            // Act
            var result = await ((IRequestHandler<CreateNutricionistaComand, Result<Guid>>)handler)
                .Handle(command, CancellationToken.None);

            // Assert

            // 1. Verify the result is successful and contains a non-empty GUID
            Assert.True(result.IsSuccess);
         
            // 2. Verify the Nutricionista was created with the correct data
            Assert.NotNull(capturedNutricionista);
            Assert.NotEqual(Guid.Empty, result.Value);
            Assert.Equal(command.Nombre, capturedNutricionista.Nombre);
            Assert.Equal(command.Activo, capturedNutricionista.Activo);
            Assert.Equal(command.FechaCreacion, capturedNutricionista.FechaCreacion);
            Assert.Equal(capturedNutricionista.Id, result.Value); // Verify returned ID matches entity ID

            // 3. Verify interaction with dependencies (Repository and UoW)
            // Verify AddAsync was called exactly once with ANY Nutricionista object
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Nutricionista>()), Times.Once);

            // Verify CommitAsync was called exactly once
            mockUnitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
