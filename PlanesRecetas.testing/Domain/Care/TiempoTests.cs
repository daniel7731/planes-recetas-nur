using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Domain.Care
{
    using PlanesRecetas.domain.Care;
    using Xunit; // or NUnit/MSTest

    public class TiempoTests
    {
        
        [Theory]
        [InlineData(1, "Desayuno")]
        [InlineData(2, "Almuerzo")]
        [InlineData(3, "Cena")]
        public void Constructor_WithValidInputs_SetsPropertiesCorrectly(int expectedId, string expectedNombre)
        {
            

            // Act
            var tiempo = new Tiempo(expectedId, expectedNombre);

            // Assert
            Assert.Equal(expectedId, tiempo.Id);
            Assert.Equal(expectedNombre, tiempo.Nombre);
        }

        // --- Test Case: Default Constructor ---
        [Fact]
        public void DefaultConstructor_InitializesToDefaultValues()
        {
            // Act
            var tiempo = new Tiempo();

            // Assert
            Assert.Equal(0, tiempo.Id);
            Assert.Null(tiempo.Nombre);
        }

        // --- Test Case: Property Mutation (Update) ---

 
        [Fact]
        public void PublicSetters_AllowPropertyMutation()
        {
            // Arrange
            var tiempo = new Tiempo(1, "Almuerzo"); // Initial state: Lunch

            int newId = 2;
            string newNombre = "Cena"; // New state: Dinner

            // Act
            tiempo.Id = newId;
            tiempo.Nombre = newNombre;

            // Assert
            Assert.Equal(newId, tiempo.Id);
            Assert.Equal(newNombre, tiempo.Nombre);
        }
    }
}
