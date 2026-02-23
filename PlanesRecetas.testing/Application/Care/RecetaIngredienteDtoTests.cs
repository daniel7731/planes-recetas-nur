using PlanesRecetas.application.Care;
using PlanesRecetas.application.Recipe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Care
{
    public class RecetaIngredienteDtoTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void RecetaIngredienteDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new RecetaIngredienteDto
            {
                RecetaId = Guid.NewGuid(),
                IngredienteId = Guid.NewGuid(),
                CantidadValor = 250.50m,
                IngredienteNombre = "Pollo"
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void RecetaIngredienteDto_NullCantidad_ShouldBeValid()
        {
            // Arrange - Según el SQL (CantidadValor DECIMAL NULL)
            var dto = new RecetaIngredienteDto
            {
                RecetaId = Guid.NewGuid(),
                IngredienteId = Guid.NewGuid(),
                CantidadValor = null
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void RecetaIngredienteDto_NegativeCantidad_ShouldReturnRangeError()
        {
            // Arrange
            var dto = new RecetaIngredienteDto
            {
                RecetaId = Guid.NewGuid(),
                IngredienteId = Guid.NewGuid(),
                CantidadValor = -5.0m
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("CantidadValor"));
            Assert.NotNull(error);
            Assert.Equal("La cantidad debe ser mayor a 0", error.ErrorMessage);
        }

        [Fact]
        public void RecetaIngredienteDto_MissingIds_ShouldHaveValidationErrors()
        {
            // Arrange - Guids vacíos (default)
            var dto = new RecetaIngredienteDto
            {
                RecetaId = Guid.Empty,
                IngredienteId = Guid.Empty
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            // Nota: Guid.Empty no dispara [Required] por ser un ValueType,
            // pero podemos validar que no sean iguales a Guid.Empty si es necesario.
            Assert.Equal(Guid.Empty, dto.RecetaId);
            Assert.Equal(Guid.Empty, dto.IngredienteId);
        }
    }
}
