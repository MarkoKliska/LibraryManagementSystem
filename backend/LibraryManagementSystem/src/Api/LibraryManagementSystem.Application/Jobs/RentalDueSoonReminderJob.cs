using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Repositories;

namespace LibraryManagementSystem.Application.Jobs;

public sealed class RentalDueSoonReminderJob(
    IRentalRepository rentalRepository,
    IUnitOfWork unitOfWork,
    IDomainEventDispatcher domainEventDispatcher
)
{
    private const int ReminderWindowDays = 3;

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var windowEnd = DateTime.UtcNow.AddDays(ReminderWindowDays);

        var dueSoonRentals = await rentalRepository.GetRentalsDueSoonAsync(windowEnd, cancellationToken);

        foreach (var rental in dueSoonRentals)
        {
            rental.MarkReminderSent();
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await domainEventDispatcher.DispatchEventsAsync(cancellationToken);
    }
}