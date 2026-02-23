namespace PlanesRecetas.webapi.Parameters.Recipe
{
    public class CreateIngredienteParamater
    {
        public decimal Calorias { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadValor { get; set; }
        public Guid CategoriaId { get; set; }
        public int UnidadId { get; set; }
    }
}
