using PlanesRecetas.application.Medicos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Medicos
{
    public class NutricionistaDtoTests
    {
        // Método helper para ejecutar las validaciones
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void NutricionistaDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new NutricionistaDto
            {
                Id = Guid.NewGuid(),
                Nombre = "Dr. Alberto Cormillot",
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void NutricionistaDto_MissingNombre_ShouldReturnRequiredError()
        {
            // Arrange
            var dto = new NutricionistaDto
            {
                Nombre = "" // Nombre vacío
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Equal("El nombre es requerido", error.ErrorMessage);
        }

        [Fact]
        public void NutricionistaDto_NombreTooLong_ShouldReturnLengthError()
        {
            // Arrange
            var dto = new NutricionistaDto
            {
                // Creamos un string de 101 caracteres
                Nombre = new string('A', 101)
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Equal("El nombre no puede exceder los 100 caracteres", error.ErrorMessage);
        }
    }
}
