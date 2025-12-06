using PlanesRecetas.domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.testing.Domain.Persons
{
    public class NutricionistaTests
    {
        // Datos de prueba comunes
       

        // ----------------------------------------------------------------------
        // Test para Constructor sin Guid (Usado comúnmente en Repositorios o Mappers)
        // ----------------------------------------------------------------------

        [Fact]
        public void Constructor_SinId_DebeInicializarCorrectamentePropiedades()
        {
            var NombreEsperado = "Dra. Elisa Pérez";
            DateTime FechaCreacionEsperada = new DateTime(2025, 1, 15);
            bool ActivoEsperado = true;
            Guid IdEsperado = Guid.NewGuid();
            // Arrange & Act
            var nutricionista = new Nutricionista(
                NombreEsperado,
                ActivoEsperado,
                FechaCreacionEsperada
            );

            // Assert
            // Verifica que los valores de las propiedades sean los pasados al constructor
            Assert.Equal(nutricionista.Nombre, NombreEsperado); 
            Assert.Equal(nutricionista.Activo, ActivoEsperado);
            Assert.Equal(nutricionista.FechaCreacion, FechaCreacionEsperada);
      

            // Verifica que el Id del AggregateRoot se genere por defecto (Guid.Empty o un nuevo Guid,
            // dependiendo de la implementación del constructor vacío de AggregateRoot. 
            // Si el constructor de la clase Nutricionista NO llama a :base(id), el Id será Guid.Empty si la base lo inicializa así).
            // Si AggregateRoot no inicializa el Id en el constructor por defecto, será Guid.Empty.
        }

        // ----------------------------------------------------------------------
        // Test para Constructor con Guid (Usado comúnmente al recuperar de BD o crear un nuevo Aggregate)
        // ----------------------------------------------------------------------

        [Fact]
        public void Constructor_ConId_DebeInicializarCorrectamentePropiedadesYBaseId()
        {
            // Arrange & Act
            var NombreEsperado = "Dra. Elisa Pérez";
            DateTime FechaCreacionEsperada = new DateTime(2025, 1, 15);
            bool ActivoEsperado = true;
            Guid IdEsperado = Guid.NewGuid();
            var nutricionista = new Nutricionista(
                IdEsperado,
                NombreEsperado,
                ActivoEsperado,
                FechaCreacionEsperada
            );

            // Assert
            // 1. Verificar el Id de la clase base (AggregateRoot)
           // nutricionista.Id.Should().Be(IdEsperado);
            Assert.Equal(nutricionista.Nombre, NombreEsperado);
          // 2. Verificar las propiedades específicas de Nutricionista
            Assert.Equal(ActivoEsperado, ActivoEsperado);
            Assert.Equal(FechaCreacionEsperada, FechaCreacionEsperada);
        }

        // ----------------------------------------------------------------------
        // Test para Constructor Vacío (Requerido por algunos ORMs como Entity Framework)
        // ----------------------------------------------------------------------

        [Fact]
        public void Constructor_Vacio_DebeCrearInstanciaConValoresPorDefecto()
        {
            // Arrange & Act
            var nutricionista = new Nutricionista();

            // Assert
            // Los strings deben ser null si no se inicializan explícitamente
            Assert.Null(nutricionista.Nombre);

            // Los booleanos deben ser false
            Assert.False(nutricionista.Activo);

            // Los DateTime deben ser DateTime.MinValue
            Assert.Equal(DateTime.MinValue, nutricionista.FechaCreacion);

            // El Id debe ser Guid.Empty
            //Assert.Equal(Guid.Empty, nutricionista.Id);
        }
    }
}
