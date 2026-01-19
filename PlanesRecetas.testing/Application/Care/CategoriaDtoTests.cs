using PlanesRecetas.application.Care;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Application.Care
{
    public class CategoriaDtoTests
    {
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
            var id = Guid.NewGuid();
            var nombre = "Frutas Tropicales";
            var tipoId = 2; // Fruta

            // Act
            var dto = new CategoriaDto(id, nombre, tipoId);

            // Assert
            Assert.Equal(id, dto.Id);
            Assert.Equal(nombre, dto.Nombre);
            Assert.Equal(tipoId, dto.TipoAlimentoId);
        }

        [Fact]
        public void CategoriaDto_ValidData_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new CategoriaDto
            {
                Id = Guid.NewGuid(),
                Nombre = "Verduras de Hoja Verde",
                TipoAlimentoId = 1
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Fact]
        public void CategoriaDto_InvalidTipoAlimentoId_ShouldReturnRangeError()
        {
            // Arrange
            var dto = new CategoriaDto
            {
                Nombre = "Test",
                TipoAlimentoId = 0 // ID inválido según nuestra regla [Range(1, ...)]
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("TipoAlimentoId"));
            Assert.NotNull(error);
            Assert.Equal("Debe seleccionar un tipo de alimento válido", error.ErrorMessage);
        }

        [Fact]
        public void CategoriaDto_NombreTooLong_ShouldReturnLengthError()
        {
            // Arrange - Límite SQL: NVARCHAR(100)
            var dto = new CategoriaDto
            {
                Nombre = new string('C', 101),
                TipoAlimentoId = 1
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var error = errors.Find(v => v.MemberNames.Contains("Nombre"));
            Assert.NotNull(error);
            Assert.Contains("100 caracteres", error.ErrorMessage);
        }
    }
}
