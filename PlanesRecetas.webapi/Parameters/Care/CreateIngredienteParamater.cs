namespace PlanesRecetas.webapi.Parameters.Care
{
    public class CreateIngredienteParamater
    {
        public decimal Calorias { get; set; }
        public string Nombre { get; set; }
        public Decimal CantidadValor { get; set; }
        public Guid CategoriaId { get; set; }
        public int UnidadId { get; set; }
    }
}
