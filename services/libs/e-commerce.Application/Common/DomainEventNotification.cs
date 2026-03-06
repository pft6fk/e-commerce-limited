using e_commerce.Domain.Common;
using MediatR;

namespace e_commerce.Application.Common;

public class DomainEventNotification<TEvent> : INotification where TEvent : IDomainEvent
{
    public TEvent DomainEvent { get; }

    public DomainEventNotification(TEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}
