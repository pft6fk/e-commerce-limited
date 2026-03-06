namespace e_commerce.Domain.Common;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot() { }
    protected AggregateRoot(TId id) : base(id) { }

    public IReadOnlyCollection<IDomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent @event)
        => _domainEvents.Add(@event);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}