namespace e_commerce.Domain.Common;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;

    protected Entity() { } // for EF

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id is null || other.Id is null)
            return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
        => Id?.GetHashCode() ?? 0;
}