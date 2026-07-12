using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Common.BaseEntity;
using LibraryManagementSystem.Infrastructure.Persistence.Contexts;
using MediatR;

namespace LibraryManagementSystem.Infrastructure.Persistence.Events;

public class DomainEventDispatcher(
    LibraryDbContext context,
    IMediator mediator
) : IDomainEventDispatcher
{
    public async Task DispatchEventsAsync(CancellationToken cancellationToken = default)
    {
        if (context.ChangeTracker.HasChanges())
            throw new InvalidOperationException(
                "DispatchEventsAsync was called while there are unsaved changes. " +
                "Call IUnitOfWork.SaveChangesAsync first so events are only published for persisted state.");

        var entitiesWithEvents = context.ChangeTracker
            .Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                var wrapperType = typeof(DomainEventNotification<>)
                    .MakeGenericType(domainEvent.GetType());
                var notification = (INotification)Activator.CreateInstance(wrapperType, domainEvent)!;

                await mediator.Publish(notification, cancellationToken);
            }
        }
    }
}