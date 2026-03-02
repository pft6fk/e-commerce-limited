namespace e_commerce.Domain.ValueObjects;

public sealed record OrderItemId(Guid Value)
{
    public static OrderItemId New() => new(Guid.NewGuid());
}