using e_commerce.Application.Common;
using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;
using MediatR;

namespace e_commerce.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IPublisher _publisher;

    public UnitOfWork(AppDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        // Collect domain events from all tracked aggregates before saving
        var domainEvents = _context.ChangeTracker
            .Entries<IAggregateRoot>()
            .SelectMany(entry =>
            {
                var events = entry.Entity.DomainEvents.ToList();
                entry.Entity.ClearDomainEvents();
                return events;
            })
            .ToList();

        // Save first, then publish events
        await _context.SaveChangesAsync(ct);

        // Dispatch each event wrapped as a MediatR notification
        foreach (var domainEvent in domainEvents)
        {
            var notification = CreateNotification(domainEvent);
            await _publisher.Publish(notification, ct);
        }
    }

    private static INotification CreateNotification(IDomainEvent domainEvent)
    {
        var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
        return (INotification)Activator.CreateInstance(notificationType, domainEvent)!;
    }
}
