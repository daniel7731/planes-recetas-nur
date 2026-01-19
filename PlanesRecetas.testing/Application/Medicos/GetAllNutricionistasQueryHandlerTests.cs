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
    public class GetAllNutricionistasQueryHandlerTests
    {
        private readonly Mock<INutricionistaRepository> _nutricionistaRepositoryMock;
        private readonly GetAllNutricionistasQueryHandler _handler;

        public GetAllNutricionistasQueryHandlerTests()
        {
            _nutricionistaRepositoryMock = new Mock<INutricionistaRepository>();
            _handler = new GetAllNutricionistasQueryHandler(_nutricionistaRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenNutricionistasExist()
        {
            // Arrange
            var nutricionistasEntities = new List<domain.Persons.Nutricionista>
        {
            new domain.Persons.Nutricionista(Guid.NewGuid(), "Dr. Smith", true , DateTime.Now),
            new domain.Persons.Nutricionista(Guid.NewGuid(), "Dra. García", true , DateTime.Now)
        };

            _nutricionistaRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(nutricionistasEntities);

            var query = new GetAllNutricionistasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Dr. Smith", result.Value[0].Nombre);
            Assert.Equal("Dra. García", result.Value[1].Nombre);
            _nutricionistaRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenListIsEmpty()
        {
            // Arrange
            _nutricionistaRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<domain.Persons.Nutricionista>()); // Lista vacía

            var query = new GetAllNutricionistasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.NutricionistasNotFound, result.Error);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            _nutricionistaRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns((List<domain.Persons.Nutricionista>?)null); // Retorno nulo

            var query = new GetAllNutricionistasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.NutricionistasNotFound, result.Error);
        }
    }
}
