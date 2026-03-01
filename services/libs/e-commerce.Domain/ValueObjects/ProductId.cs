namespace e_commerce.Domain.ValueObjects;

public sealed record ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
}