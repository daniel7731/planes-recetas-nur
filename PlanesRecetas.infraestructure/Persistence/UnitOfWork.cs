
using Joseco.DDD.Core.Abstractions;
using MediatR;
using PlanesRecetas.infraestructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PlanesRecetas.infraestructure.Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DomainDbContext _dbContext;
        private readonly IMediator _mediator;

        public UnitOfWork(DomainDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {

           // Get domain events
            var domainEvents = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x =>
                {
                    var domainEvents = x.Entity
                                    .DomainEvents
                                    .ToImmutableArray();
                    x.Entity.ClearDomainEvents();

                    return domainEvents;
                })
                .SelectMany(domainEvents => domainEvents)
                .ToList();

            //Publish Domain Events
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }


            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

