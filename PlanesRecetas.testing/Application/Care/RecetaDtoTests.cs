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
    public class RecetaDtoTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            // El parámetro true es vital para validar también las colecciones si se desea
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void RecetaDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new RecetaDto
            {
                Id = Guid.NewGuid(),
                Nombre = "Ensalada César",
                TiempoId = 3, // Lunch
                Ingredientes = new List<RecetaIngredienteDto>
            {
                new RecetaIngredienteDto { IngredienteId = Guid.NewGuid(), CantidadValor = 100 }
            }
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void RecetaDto_NombreTooLong_ShouldReturnLengthError()
        {
            // Arrange - Límite SQL: 150
            var dto = new RecetaDto
            {
                Nombre = new string('R', 151),
                TiempoId = 1
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Contains("150 caracteres", error.ErrorMessage);
        }

        [Fact]
        public void RecetaDto_InvalidTiempoId_ShouldReturnRangeError()
        {
            // Arrange
            var dto = new RecetaDto
            {
                Nombre = "Batido de Proteína",
                TiempoId = 0 // Inválido
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("TiempoId"));
            Assert.NotNull(error);
            Assert.Equal("Debe seleccionar un tiempo de comida válido", error.ErrorMessage);
        }

        [Fact]
        public void RecetaDto_EmptyIngredients_ShouldStillBeValid()
        {
            // Arrange - Una receta puede crearse inicialmente sin ingredientes
            var dto = new RecetaDto
            {
                Nombre = "Agua con Limón",
                TiempoId = 1,
                Ingredientes = new List<RecetaIngredienteDto>()
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }
    }
}
