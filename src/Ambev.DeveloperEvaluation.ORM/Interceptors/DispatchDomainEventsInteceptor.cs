using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ambev.DeveloperEvaluation.ORM.Interceptors
{
    public class DispatchDomainEventsInteceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;
        public DispatchDomainEventsInteceptor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? dbContext)
        {
            if (dbContext == null)
                return;

            var domainEntities = dbContext.ChangeTracker.Entries<BaseEntity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .ToArray();
            var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToArray();

            domainEntities.ToList().ForEach(entity => entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
