using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Contracts.User;
using LibraryManagementSystem.Domain.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Infrastructure.Events;

public sealed class UserRegisteredDomainEventHandler(
    IPublishEndpoint publishEndpoint,
    ILogger<UserRegisteredDomainEventHandler> logger
) : INotificationHandler<DomainEventNotification<UserRegisteredDomainEvent>>
{
    public async Task Handle(
        DomainEventNotification<UserRegisteredDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        try
        {
            await publishEndpoint.Publish(
                new UserRegisteredIntegrationEvent(
                    domainEvent.UserId,
                    domainEvent.Email,
                    domainEvent.FirstName),
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Failed to publish UserRegisteredIntegrationEvent for user {UserId}",
                domainEvent.UserId);
        }
    }
}