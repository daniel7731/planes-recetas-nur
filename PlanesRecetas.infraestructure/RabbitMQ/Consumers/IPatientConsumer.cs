
using PlanesRecetas.application.Pacientes.Evento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.infraestructure.RabbitMQ.Consumers
{
    public interface IPatientConsumer
    {
        Task ProcessMessageAsync(PacienteCreated message);
    }
}
