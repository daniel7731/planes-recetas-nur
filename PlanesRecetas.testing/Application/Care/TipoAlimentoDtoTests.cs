using PlanesRecetas.application.Care;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Care
{
    public class TipoAlimentoDtoTests
    {
        // Helper para validación
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Constructor_ShouldAssignPropertiesCorrectly()
        {
            // Arrange
            int expectedId = 10;
            string expectedNombre = "Verdura";

            // Act
            var dto = new TipoAlimentoDto(expectedId, expectedNombre);

            // Assert
            Assert.Equal(expectedId, dto.Id);
            Assert.Equal(expectedNombre, dto.Nombre);
        }

        [Fact]
        public void TipoAlimentoDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new TipoAlimentoDto { Id = 1, Nombre = "Carne Roja" };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void TipoAlimentoDto_NombreEmpty_ShouldReturnRequiredError()
        {
            // Arrange
            var dto = new TipoAlimentoDto { Nombre = "" };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Equal("El nombre es obligatorio", error.ErrorMessage);
        }

        [Fact]
        public void TipoAlimentoDto_NombreTooLong_ShouldReturnLengthError()
        {
            // Arrange - Límite 50 caracteres
            var dto = new TipoAlimentoDto { Nombre = new string('X', 51) };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Equal("El nombre no puede exceder los 50 caracteres", error.ErrorMessage);
        }
    }
}
