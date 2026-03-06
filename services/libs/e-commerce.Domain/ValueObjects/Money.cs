namespace e_commerce.Domain.ValueObjects;

public class Money
{
    public string Currency { get; private set; }
    public decimal Amount { get; private set; }
    public static Money Zero => new(0, "USD");
    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be null or empty.");
        }

        Amount = amount;
        Currency = currency;
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency.ToLower() != right.Currency.ToLower())
            throw new InvalidOperationException("Cannot add different currencies.");

        return new Money(left.Amount + right.Amount, left.Currency);

    }
    public static Money operator *(Money money, int multiplier)
    {
        if (multiplier <= 0)
            throw new InvalidOperationException("Cannot multiply by less than 0.");

        return new Money(money.Amount * multiplier, money.Currency);

    }
    public static bool operator ==(Money left, Money right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;

        if (left.Currency.ToLower() != right.Currency.ToLower())
            throw new InvalidOperationException("Cannot compare different currencies.");

        return left.Amount == right.Amount;

    }
    public static bool operator !=(Money left, Money right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Money other) return false;
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency.ToLower());
    }
}
