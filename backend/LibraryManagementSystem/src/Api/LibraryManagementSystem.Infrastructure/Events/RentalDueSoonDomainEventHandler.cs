using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Contracts.Library;
using LibraryManagementSystem.Domain.Events;
using LibraryManagementSystem.Domain.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Infrastructure.Events;

public sealed class RentalDueSoonDomainEventHandler(
    IRentalRepository rentalRepository,
    IPublishEndpoint publishEndpoint,
    ILogger<RentalDueSoonDomainEventHandler> logger
) : INotificationHandler<DomainEventNotification<RentalDueSoonDomainEvent>>
{
    public async Task Handle(
        DomainEventNotification<RentalDueSoonDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var rental = await rentalRepository.GetByIdAsync(domainEvent.RentalId, cancellationToken);
        if (rental is null)
        {
            logger.LogWarning(
                "Rental {RentalId} not found while publishing RentalDueSoonIntegrationEvent",
                domainEvent.RentalId);
            return;
        }

        await publishEndpoint.Publish(
            new RentalDueSoonIntegrationEvent(
                rental.UserId,
                rental.User.Email,
                rental.BookCopy.Book.Title,
                domainEvent.DueDate),
            cancellationToken);
    }
}