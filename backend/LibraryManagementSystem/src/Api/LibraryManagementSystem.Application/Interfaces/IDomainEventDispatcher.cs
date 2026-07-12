namespace LibraryManagementSystem.Application.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(CancellationToken cancellationToken = default);
}