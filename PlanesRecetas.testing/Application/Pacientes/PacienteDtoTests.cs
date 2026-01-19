using PlanesRecetas.application.Pacientes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace PlanesRecetas.testing.Application.Pacientes
{
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class PacienteDtoTests
    {
        // Helper para ejecutar la validación manual (Standard .NET)
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            // validateAllProperties: true es clave para revisar todo el objeto
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void PacienteDto_ValidData_ShouldHaveNoValidationErrors()
        {
            // Arrange
            var dto = new PacienteDto
            {
                Id = Guid.NewGuid(),
                Nombre = "Ana",
                Apellido = "García",
                FechaNacimiento = new DateTime(1995, 5, 20),
                Peso = 65.0m,
                Altura = 1.68m
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("", "García", "Nombre")]    // Nombre vacío
        [InlineData("Ana", "", "Apellido")]    // Apellido vacío
        public void PacienteDto_MissingRequiredFields_ShouldReturnErrors(string nombre, string apellido, string memberName)
        {
            // Arrange
            var dto = new PacienteDto
            {
                Nombre = nombre,
                Apellido = apellido
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            // Verificamos que exista al menos un error para el campo específico
            Assert.Contains(errors, v => v.MemberNames.Contains(memberName));
        }

        [Fact]
        public void PacienteDto_WeightOutOfRange_ShouldReturnError()
        {
            // Arrange
            var dto = new PacienteDto
            {
                Nombre = "Ana",
                Apellido = "García",
                Peso = 600m // El máximo en nuestro DTO era 500
            };

            // Act
            var errors = ValidateModel(dto);

            // Assert
            var weightError = errors.FirstOrDefault(v => v.MemberNames.Contains("Peso"));
            Assert.NotNull(weightError);
            Assert.Equal("El peso debe estar entre 1 y 500 kg", weightError.ErrorMessage);
        }
    }
}
