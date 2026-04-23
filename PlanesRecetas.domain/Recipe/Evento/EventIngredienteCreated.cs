
using Joseco.DDD.Core.Abstractions;


namespace PlanesRecetas.domain.Recipe.Evento
{
    public record EventIngredienteCreated : DomainEvent
    {


        public string Nombre { get; set; }

        public decimal Calorias { get; set; }

        public Guid CategoriaId { get; set; }

        public int UnidadId { get; set; }
        public EventIngredienteCreated(Guid Id, string nombre, decimal calorias, Guid categoriaId, int unidadId)
        {
            this.Id = Id;
            Nombre = nombre;
            Calorias = calorias;
            CategoriaId = categoriaId;
            UnidadId = unidadId;
        }
    }
}
