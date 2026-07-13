using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Contracts.Library;
using LibraryManagementSystem.Domain.Events;
using LibraryManagementSystem.Domain.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Infrastructure.Events;

public sealed class BookReturnedDomainEventHandler(
    IRentalRepository rentalRepository,
    IPublishEndpoint publishEndpoint,
    ILogger<BookReturnedDomainEventHandler> logger
) : INotificationHandler<DomainEventNotification<BookReturnedDomainEvent>>
{
    public async Task Handle(
        DomainEventNotification<BookReturnedDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var rental = await rentalRepository.GetByIdAsync(domainEvent.RentalId, cancellationToken);
        if (rental is null)
        {
            logger.LogWarning(
                "Rental {RentalId} not found while publishing BookReturnedIntegrationEvent",
                domainEvent.RentalId);
            return;
        }

        try
        {
            await publishEndpoint.Publish(
                new BookReturnedIntegrationEvent(
                    rental.UserId,
                    rental.User.Email,
                    rental.BookCopy.Book.Title),
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Failed to publish BookReturnedIntegrationEvent for rental {RentalId}",
                domainEvent.RentalId);
        }
    }
}