namespace PlanesRecetas.testing.Domain.Persons
{
 
    using PlanesRecetas.domain.Persons;

    public class PacienteTests
    {
        [Fact]
        public void ConstructorTest()
        {

            var idEsperado = Guid.NewGuid();
            var nombreEsperado = "Carlos";
            var apellidoEsperado = "Mesa";
            var fechaNacimientoEsperada = new DateTime(1990, 5, 15);
            var emailEsperado = "carlos.mesa@salud.com";
            var telefonoEsperado = "555-1234";
            var peso = 0m;
            var altura = 0m;
            // ACT
            var paciente = new Paciente(
                idEsperado,
                nombreEsperado,
                apellidoEsperado,
                fechaNacimientoEsperada,
                emailEsperado,
                telefonoEsperado
            );
            
      
           // Assert.
            // ASSERT
            // 1. Verificar propiedades del AggregateRoot
           // Assert.Equal(paciente.,idEsperado);

            // 2. Verificar propiedades personales
            Assert.Equal(paciente.Nombre, nombreEsperado);
            Assert.Equal(paciente.Apellido, apellidoEsperado);
            Assert.Equal(paciente.FechaNacimiento, fechaNacimientoEsperada);
            Assert.Equal(paciente.Email, emailEsperado);
            Assert.Equal(paciente.Telefono, telefonoEsperado);

            // 3. Verificar que Peso y Altura sean sus valores por defecto (Decimal = 0)
            Assert.Equal(paciente.Peso,peso);
            Assert.Equal(paciente.Altura,altura);
        }
        [Fact]
        public void ConstructorPesoAltura_Correctamente()
        {
            // ARRANGE
            var nombreEsperado = "Ana";
            var apellidoEsperado = "Ríos";
            var fechaNacimientoEsperada = new DateTime(1985, 10, 20);
            var pesoEsperado = 75.5m;
            var alturaEsperada = 1.65m;

            // ACT
            var paciente = new Paciente(
                nombreEsperado,
                apellidoEsperado,
                fechaNacimientoEsperada,
                pesoEsperado,
                alturaEsperada
            );

            // ASSERT
            // Verificar propiedades de salud y personales
            Assert.Equal(paciente.Peso, pesoEsperado);
            Assert.Equal(paciente.Altura, alturaEsperada);
            Assert.Equal(paciente.Nombre, nombreEsperado);
            Assert.Equal(paciente.Apellido, apellidoEsperado);
        }
    }
}