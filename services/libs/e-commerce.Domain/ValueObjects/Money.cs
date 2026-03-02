namespace e_commerce.Domain.ValueObjects;

public class Money
{
    public string Currency { get; private set; }
    public decimal Amount { get; private set; } 

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }
        Amount = amount;
        Currency = currency;
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
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
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot compare different currencies.");

        return left.Amount == right.Amount;

    }
    public static bool operator !=(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot compare different currencies.");

        return left.Amount != right.Amount;

    }
}
