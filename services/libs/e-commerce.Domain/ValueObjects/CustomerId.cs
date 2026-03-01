namespace e_commerce.Domain.ValueObjects
{
    public sealed record CustomerId(Guid Value)
    {
        public static CustomerId New() => new(Guid.NewGuid());

    }
}
