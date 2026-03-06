
using MediatR;
using PlanesRecetas.application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Pacientes.Evento
{
    public class PacienteCreated : IntegrationMessage , INotification
    {
      

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int BloodType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Ocupation { get; set; }
        public string Religion { get; set; }
        public string Alergies { get; set; }
    }
}
