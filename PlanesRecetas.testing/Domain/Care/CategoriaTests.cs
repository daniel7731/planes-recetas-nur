using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Domain.Care
{
    public class CategoriaTests
    {
        private readonly Guid TestGuid = Guid.NewGuid();
        private const string TestNombre = "Lácteos";
        private const int TestTipoAlimentoId = 5;

        // --- 1. Test Constructor with TipoAlimento object ---

        [Fact]
        public void Constructor_WithTipoAlimentoObject_SetsPropertiesCorrectly()
        {
            // Arrange
            var mockTipoAlimento = new TipoAlimento(TestTipoAlimentoId, "Comida");

            // Act
            var categoria = new Categoria(TestGuid, TestNombre, mockTipoAlimento);

            // Assert
            // Check base class property (Id)
            //Assert.Equal(TestGuid, categoria.Id);

            // Check assigned scalar property
            Assert.Equal(TestNombre, categoria.Nombre);

            // Check assigned complex object property
            Assert.Same(mockTipoAlimento, categoria.Tipo);

            // Check if the scalar FK property is set (it is *not* set by this constructor)
            Assert.Equal(0, categoria.TipoAlimentoId);
        }

        // --- 2. Test Constructor with scalar TipoAlimentoId ---

        [Fact]
        public void Constructor_WithTipoAlimentoId_SetsPropertiesAndCreatesTipoObject()
        {
            // Arrange & Act
            var categoria = new Categoria(TestGuid, TestNombre, TestTipoAlimentoId);

            // Assert
            // Check base class property (Id)
           // Assert.Equal(TestGuid, categoria.Id);

            // Check assigned scalar properties
            Assert.Equal(TestNombre, categoria.Nombre);
            Assert.Equal(TestTipoAlimentoId, categoria.TipoAlimentoId);

            // Check complex object creation and initialization
            Assert.NotNull(categoria.Tipo);
            Assert.Equal(TestTipoAlimentoId, categoria.Tipo.Id);

            // Check the dummy name assignment in the constructor: new TipoAlimento(tipoAlimentoId,"")
            Assert.Equal("", categoria.Tipo.Nombre);
        }

        // --- 3. Test Parameterless Constructor (for ORM/Deserialization) ---

       
    }
}
