using PlanesRecetas.application.Metrics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Metrics
{
    public class UnidadDtoTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void UnidadDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new UnidadDto
            {
                Id = 1,
                Nombre = "Kilogramos",
                Simbolo = "kg"
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("Kilogramos", "", "Simbolo")] // Símbolo vacío
        [InlineData("", "kg", "Nombre")]         // Nombre vacío
        public void UnidadDto_RequiredFields_ShouldReturnErrors(string nombre, string simbolo, string memberName)
        {
            // Arrange
            var dto = new UnidadDto { Nombre = nombre, Simbolo = simbolo };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Contains(errors, v => v.MemberNames.Contains(memberName));
        }

        [Fact]
        public void UnidadDto_SimboloTooLong_ShouldReturnError()
        {
            // Arrange - El límite en SQL es NVARCHAR(10)
            var dto = new UnidadDto
            {
                Nombre = "Mililitros",
                Simbolo = "ML-EXTRA-LONG"
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Simbolo"));
            Assert.NotNull(error);
            Assert.Equal("El símbolo no puede exceder los 10 caracteres", error.ErrorMessage);
        }
    }
}
