using PlanesRecetas.application.Care;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Care
{
    public class TiempoDtoTests
    {
        // Helper para ejecutar el motor de validación de .NET
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            int testId = 1;
            string testNombre = "Breakfast";

            // Act
            var dto = new TiempoDto(testId, testNombre);

            // Assert
            Assert.Equal(testId, dto.Id);
            Assert.Equal(testNombre, dto.Nombre);
        }

        [Fact]
        public void TiempoDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new TiempoDto { Id = 3, Nombre = "Lunch" };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TiempoDto_InvalidNombre_ShouldReturnRequiredError(string nombreInvalido)
        {
            // Arrange
            var dto = new TiempoDto { Nombre = nombreInvalido };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Equal("El nombre del tiempo de comida es obligatorio", error.ErrorMessage);
        }

        [Fact]
        public void TiempoDto_NombreExceedsLimit_ShouldReturnLengthError()
        {
            // Arrange
            var dto = new TiempoDto { Nombre = new string('A', 51) };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Equal("El nombre no puede superar los 50 caracteres", error.ErrorMessage);
        }
    }
}
