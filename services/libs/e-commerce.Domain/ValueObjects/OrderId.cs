namespace e_commerce.Domain.ValueObjects;

public sealed record OrderId(Guid Value)
{
    public static OrderId New() => new(Guid.NewGuid());
}