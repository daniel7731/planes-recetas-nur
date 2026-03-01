using System;
using System.Linq;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using PlanesRecetas.infraestructure.Persistence.DomainModel;

namespace PlanesRecetas.infraestructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IOutboxDatabase<DomainEvent>
    {
        private readonly DomainDbContext _dbContext;
        private readonly IMediator _mediator;
        private int _transactionCount = 0;

        public UnitOfWork(DomainDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("-------------------------> UnitOfWork DB Context Id: " + _dbContext.ContextId);
            _transactionCount++;

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

            // Publish Domain Events
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            if (_transactionCount == 1)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                _transactionCount--;
            }
        }

        // Implementation required by IOutboxDatabase<DomainEvent>
        public DbSet<OutboxMessage<DomainEvent>> GetOutboxMessages()
        {
            return _dbContext.OutboxMessages;
        }
    }
}

