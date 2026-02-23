using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Domain.Metrics
{
    

    public class UnidadTests
    {

        
        [Theory]
        [InlineData(2,"Kilogramo","kg")]
        [InlineData(1, "Gramo", "g")]
        [InlineData(3, "Litro", "L")]
        public void Constructor_WithValidInputs_SetsPropertiesCorrectly(int expectedId, string expectedNombre,
            string expectedSimbolo)
        {
            // Arrange
           /* int expectedId = 10;
            string expectedNombre = "Kilogramo";
            string expectedSimbolo = "kg";*/

            // Act
            var unidad = new UnidadMedida(expectedId, expectedNombre, expectedSimbolo);

            // Assert
            Assert.Equal(expectedId, unidad.Id);
            Assert.Equal(expectedNombre, unidad.Nombre);
            Assert.Equal(expectedSimbolo, unidad.Simbolo);
        }
    }
}
