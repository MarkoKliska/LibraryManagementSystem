using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Contracts.Library;
using LibraryManagementSystem.Domain.Events;
using LibraryManagementSystem.Domain.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Infrastructure.Events;

public sealed class BookRentedDomainEventHandler(
    IRentalRepository rentalRepository,
    IPublishEndpoint publishEndpoint,
    ILogger<BookRentedDomainEventHandler> logger
) : INotificationHandler<DomainEventNotification<BookRentedDomainEvent>>
{
    public async Task Handle(
        DomainEventNotification<BookRentedDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var rental = await rentalRepository.GetByIdAsync(domainEvent.RentalId, cancellationToken);
        if (rental is null)
        {
            logger.LogWarning(
                "Rental {RentalId} not found while publishing BookRentedIntegrationEvent",
                domainEvent.RentalId);
            return;
        }

        try
        {
            await publishEndpoint.Publish(
                new BookRentedIntegrationEvent(
                    rental.UserId,
                    rental.User.Email,
                    rental.BookCopy.Book.Title,
                    domainEvent.DueDate),
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Failed to publish BookRentedIntegrationEvent for rental {RentalId}",
                domainEvent.RentalId);
        }
    }
}