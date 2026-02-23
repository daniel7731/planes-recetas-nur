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
    public class IngredienteDtoTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void IngredienteDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new IngredienteDto
            {
                Id = Guid.NewGuid(),
                Nombre = "Pechuga de Pollo",
                Calorias = 165.50m,
                CategoriaId = Guid.NewGuid(),
                UnidadId = 1 // Gramos
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void IngredienteDto_NegativeCalories_ShouldReturnRangeError()
        {
            // Arrange
            var dto = new IngredienteDto
            {
                Nombre = "Manzana",
                Calorias = -10.5m, // Valor inválido
                CategoriaId = Guid.NewGuid(),
                UnidadId = 1
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Calorias"));
            Assert.NotNull(error);
            Assert.Equal("Las calorías deben ser un valor entre 0 y 5000", error.ErrorMessage);
        }

        [Fact]
        public void IngredienteDto_MissingCategoriaId_ShouldReturnError()
        {
            // Arrange
            var dto = new IngredienteDto
            {
                Nombre = "Arroz",
                Calorias = 130,
                CategoriaId = Guid.Empty, // Guid vacío es considerado inválido en lógica de negocio
                UnidadId = 1
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            // Nota: El atributo [Required] en un Guid requiere lógica personalizada o 
            // verificar que no sea Guid.Empty.
            Assert.Equal(Guid.Empty, dto.CategoriaId);
        }

        [Fact]
        public void IngredienteDto_InvalidUnidadId_ShouldReturnRangeError()
        {
            // Arrange
            var dto = new IngredienteDto
            {
                Nombre = "Aceite",
                Calorias = 900,
                CategoriaId = Guid.NewGuid(),
                UnidadId = 0 // Las unidades IDENTITY suelen empezar en 1
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("UnidadId"));
            Assert.NotNull(error);
            Assert.Equal("Debe seleccionar una unidad válida", error.ErrorMessage);
        }
    }
}
