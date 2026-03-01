using System;
using System.Collections.Generic;
using Joseco.DDD.Core.Abstractions;
using PlanesRecetas.domain.Metrics;
using System.ComponentModel.DataAnnotations.Schema;
using PlanesRecetas.domain.Care;
using PlanesRecetas.application.Recipe.Evento;

namespace PlanesRecetas.domain.Recipe
{
    public class Ingrediente : AggregateRoot
    {
        public decimal Calorias { get; set; }
        public string Nombre { get; set; }

        // mapped navigation and FK
        public Categoria? Categoria { get; set; }
        public Guid CategoriaId { get; set; }

        public int UnidadId { get; set; }
        public decimal CantidadValor { get; set; }
        public UnidadMedida? Unidad { get; set; }

        public Ingrediente()
        {
        }
        public Ingrediente(Guid id) : base(id)
        {
        }
        public void SetDomainEvent()
        {
            AddDomainEvent(new IngredienteCreated(Id, Nombre, Calorias, CategoriaId,UnidadId));      
        }
        public Ingrediente(Guid id, decimal calorias, string nombre, Categoria categoria, decimal cantidadValor, UnidadMedida unidad) : base(id)
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
            Unidad = new UnidadMedida(unidadId, "", "");
            CategoriaId = categoriaId;
            UnidadId = unidadId;

        }
    }
}
