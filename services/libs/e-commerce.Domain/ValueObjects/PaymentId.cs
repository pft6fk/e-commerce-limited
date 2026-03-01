namespace e_commerce.Domain.ValueObjects;

public sealed record PaymentId(Guid Value)
{
    public static PaymentId New() => new(Guid.NewGuid());
}