namespace PlanesRecetas.webapi.Parameters.Persons
{
    public class CreatePacienteParameter
    {
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
    }
}
