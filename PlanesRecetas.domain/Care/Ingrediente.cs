using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Metrics;
namespace PlanesRecetas.domain.Care
{
    public class Ingrediente : AggregateRoot
    {
        public Decimal Calorias { get; set; }
        public String Nombre { get; set; }  
        public Categoria? Categoria { get; set; }    
        public Guid CategoriaId {
            get; set;
        }
        public int UnidadId { 
            get; set;
        }
        public Decimal CantidadValor { get; set; }
        public Unidad? Unidad { get; set; }

        public ICollection<Receta> Recetas { get; set; }
        public Ingrediente()
        {

        }
        public Ingrediente(Guid id):base(id)
        {
        }
        public Ingrediente(Guid id, decimal calorias, string nombre, Categoria categoria, decimal cantidadValor, Unidad unidad) : base(id)
        {
            Calorias = calorias;
            Nombre = nombre;
            Categoria = categoria;
            CantidadValor = cantidadValor;
            Unidad = unidad;
        }

        
        public Ingrediente(Guid id, decimal calorias, string nombre, Guid categoriaId, decimal cantidadValor, int unidadId) : base(id)
        {
            Calorias = calorias;
            Nombre = nombre;
            Categoria = new Categoria(categoriaId, "", 0);
            CantidadValor = cantidadValor;
            Unidad = new Unidad(unidadId, "","");  
            CategoriaId = categoriaId;
            UnidadId = unidadId;
        }
    }
}
