using Joseco.DDD.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.domain.Persons
{
    public class Paciente : AggregateRoot
    {
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String Email { get; set; }
        public String Telefono { get; set; }
        public Decimal Peso { get; set; }
        public Decimal Altura { get; set; }
        public Paciente(Guid id, String nombre, String apellido, DateTime fechaNacimiento, String email, String telefono):base(id)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Telefono = telefono;
        }
        public Paciente(String nombre, String apellido, DateTime fechaNacimiento, Decimal peso, Decimal altura)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Altura= altura;
            Peso= peso;
        }
        public Paciente(Guid id, String nombre, String apellido, DateTime fechaNacimiento, Decimal peso, Decimal altura) : base(id)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Altura = altura;
            Peso = peso;
        }
    }
}
