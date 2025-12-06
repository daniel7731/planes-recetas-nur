using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Domain.Care
{
    public class TipoAlimentoTests
    {

     

        // --- 1. Test Parameterized Constructor ---

        [Theory]
        [InlineData(1,"Fruta")]
        [InlineData(2, "Carnes")]
        public void Constructor_WithIdAndNombre_SetsNombreCorrectly(int expectedId,string nombre)
        {
            // Arrange & Act
            var tipoAlimento = new TipoAlimento(expectedId, nombre);

            // Assert
            // Check if the 'Nombre' property was set by the constructor
            Assert.Equal(nombre, tipoAlimento.Nombre);

            // Note: The provided constructor public TipoAlimento(int id ,string nombre) => Nombre = nombre; 
            // does *not* set 'Id', so we expect 'Id' to be its default value (0).
            Assert.Equal(0, tipoAlimento.Id);
        }

        // --- 2. Test Parameterless Constructor ---

        [Fact]
        public void ParameterlessConstructor_InitializesPropertiesToDefaults()
        {
            // Arrange & Act
            var tipoAlimento = new TipoAlimento();

            // Assert
            // The default values for int and string are 0 and null respectively
            Assert.Equal(0, tipoAlimento.Id);
            Assert.Null(tipoAlimento.Nombre);
        }

    }
}
