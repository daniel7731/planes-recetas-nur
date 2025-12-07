using PlanesRecetas.domain.Care;
using PlanesRecetas.domain.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Domain.Care
{
    public class IngredienteTests
    {
        // Datos de prueba
        private readonly Guid TestGuid = Guid.NewGuid();
        private readonly Guid TestCategoriaId = Guid.NewGuid();
        private const decimal TestCalorias = 250.75m;
        private const string TestNombre = "Mantequilla";
        private const decimal TestCantidadValor = 10.0m;
        private const int TestUnidadId = 2;

        // Objetos de prueba
        private readonly Categoria MockCategoria;
        private readonly Unidad MockUnidad;

        public IngredienteTests()
        {
            // Inicializar mocks para los tests
            MockCategoria = new Categoria(TestCategoriaId, "Grasas", 1);
            MockUnidad = new Unidad(TestUnidadId, "Cuchara", "Cda");
        }

        // --- 1. Test de Constructor por Defecto ---

        [Fact]
        public void ConstructorPorDefecto_InicializaValoresPorDefecto()
        {
            // Act
            var ingrediente = new Ingrediente();

            // Assert
            Assert.Equal(Guid.Empty, ingrediente.Id);
            Assert.Equal(0m, ingrediente.Calorias);
            Assert.Null(ingrediente.Nombre);
            Assert.Null(ingrediente.Categoria);
            Assert.Equal(Guid.Empty, ingrediente.CategoriaId);
            Assert.Null(ingrediente.Unidad);
            Assert.Equal(0, ingrediente.UnidadId);
            // La colección debe ser inicializada si se espera que no sea nula.
            // En este caso, el constructor por defecto no la inicializa explícitamente, por lo que se espera null.
            Assert.Null(ingrediente.Recetas);
        }

        // --- 2. Test de Constructor Solo con ID ---

        [Fact]
        public void ConstructorSoloID_AsignaCorrectamenteID()
        {
            // Act
            var ingrediente = new Ingrediente(TestGuid);

            // Assert
            Assert.Equal(TestGuid, ingrediente.Id);
        }

        // --- 3. Test de Constructor con Objetos de Dominio Completos ---

        [Fact]
        public void ConstructorCompleto_ConObjetos_AsignaPropiedadesYReferenciasCorrectamente()
        {
            // Act
            var ingrediente = new Ingrediente(
                TestGuid,
                TestCalorias,
                TestNombre,
                MockCategoria,
                TestCantidadValor,
                MockUnidad
            );

            // Assert
            Assert.Equal(TestGuid, ingrediente.Id);
            Assert.Equal(TestCalorias, ingrediente.Calorias);
            Assert.Equal(TestNombre, ingrediente.Nombre);
            Assert.Equal(TestCantidadValor, ingrediente.CantidadValor);

            // Verifica que se asignen las referencias de objetos
            Assert.Same(MockCategoria, ingrediente.Categoria);
            Assert.Same(MockUnidad, ingrediente.Unidad);

            // Verifica que las FKs no se inicialicen (ya que este constructor no las toca)
            Assert.Equal(Guid.Empty, ingrediente.CategoriaId);
            Assert.Equal(0, ingrediente.UnidadId);
        }

        // --- 4. Test de Constructor con ID de Clave Foránea (FK) ---

        [Fact]
        public void ConstructorConIDs_CreaObjetosAsociadosYAsignaIDs()
        {
            // Arrange
            var nuevaUnidadId = 99;

            // Act
            var ingrediente = new Ingrediente(
                TestGuid,
                TestCalorias,
                TestNombre,
                TestCategoriaId, // Usando la FK de Categoría
                TestCantidadValor,
                nuevaUnidadId      // Usando la FK de Unidad
            );

            // Assert
            Assert.Equal(TestGuid, ingrediente.Id);
            Assert.Equal(TestCalorias, ingrediente.Calorias);
            Assert.Equal(TestNombre, ingrediente.Nombre);
            Assert.Equal(TestCantidadValor, ingrediente.CantidadValor);

            // Verifica que se asignen las claves foráneas (FKs)
            Assert.Equal(TestCategoriaId, ingrediente.CategoriaId);
            Assert.Equal(nuevaUnidadId, ingrediente.UnidadId);

            // Verifica que los objetos de navegación (Categoria y Unidad) hayan sido creados
            Assert.NotNull(ingrediente.Categoria);
            Assert.NotNull(ingrediente.Unidad);

            // Verifica que los objetos creados se hayan inicializado con la FK
            Assert.Equal(TestCategoriaId, ingrediente.Categoria.Id);
            Assert.Equal(nuevaUnidadId, ingrediente.Unidad.Id);
        }

        // --- 5. Test de Asignación de Propiedades Abiertas ---

        [Fact]
        public void PropiedadesAbiertas_PuedenSerModificadas()
        {
            // Arrange
            var ingrediente = new Ingrediente(TestGuid);
            var nuevoNombre = "Aceite de Oliva";

            // Act
            ingrediente.Nombre = nuevoNombre;
            ingrediente.Calorias = 500.5m;

            // Assert
            Assert.Equal(nuevoNombre, ingrediente.Nombre);
            Assert.Equal(500.5m, ingrediente.Calorias);
        }
    }
}
