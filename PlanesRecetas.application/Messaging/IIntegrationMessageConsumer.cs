using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Messaging
{
    public interface IIntegrationMessageConsumer<T> where T : IntegrationMessage
    {
        Task HandleAsync(T message, CancellationToken cancellationToken);

    }
}
