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
    public class GetNutricionistaByIdQueryHandlerTests
    {
        private readonly Mock<INutricionistaRepository> _nutricionistaRepositoryMock;
        private readonly GetNutricionistaByIdQueryHandler _handler;

        public GetNutricionistaByIdQueryHandlerTests()
        {
            _nutricionistaRepositoryMock = new Mock<INutricionistaRepository>();
            _handler = new GetNutricionistaByIdQueryHandler(_nutricionistaRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenNutricionistaExists()
        {
            // Arrange
            var nutricionistaId = Guid.NewGuid();
            var nutricionistaEntity = new domain.Persons.Nutricionista(
                nutricionistaId,
                "Dr. Garcia",
                true,
                DateTime.UtcNow
            );

            _nutricionistaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(nutricionistaId, It.IsAny<bool>()))
                .ReturnsAsync(nutricionistaEntity);

            var query = new GetNutricionistaByIdQuery(nutricionistaId, ReadOnly: true);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(nutricionistaId, result.Value.Id);
            Assert.Equal("Dr. Garcia", result.Value.Nombre);
            Assert.True(result.Value.Activo);

            _nutricionistaRepositoryMock.Verify(x => x.GetByIdAsync(nutricionistaId, true), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenNutricionistaIsNotFound()
        {
            // Arrange
            var nutricionistaId = Guid.NewGuid();

            _nutricionistaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(nutricionistaId, It.IsAny<bool>()))
                .ReturnsAsync((domain.Persons.Nutricionista?)null);

            var query = new GetNutricionistaByIdQuery(nutricionistaId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(Errors.NutricionistaNotFound, result.Error);
            _nutricionistaRepositoryMock.Verify(x => x.GetByIdAsync(nutricionistaId, false), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_MapAllPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var fecha = new DateTime(2023, 10, 1);
            var entity = new domain.Persons.Nutricionista(id, "Test Name",false,fecha);

            _nutricionistaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(id, It.IsAny<bool>()))
                .ReturnsAsync(entity);

            var query = new GetNutricionistaByIdQuery(id);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            // Verificamos propiedad por propiedad para asegurar que el mapeo manual es correcto
            Assert.Equal(id, result.Value.Id);
            Assert.Equal("Test Name", result.Value.Nombre);
            Assert.Equal(fecha, result.Value.FechaCreacion);
            Assert.False(result.Value.Activo);
        }
    }
}
