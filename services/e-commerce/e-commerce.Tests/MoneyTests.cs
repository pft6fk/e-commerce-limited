namespace e_commerce.Tests;

public class MoneyTests
{
    private Money _100usd => new Money(100, "USD");
    private Money _50usd => new Money(50, "USD");
    private Money _50eur => new Money(50, "EUR");

    [Fact]
    public void Constructor_ShouldSetAmountAndCurrency()
    {
        var money = new Money(100, "USD");

        money.Amount.Should().Be(100);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Constructor_WhenCurrencyIsEmpty_ShouldThrow()
    {
        var act = () => new Money(100, string.Empty);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Addition_SameCurrency_ShouldReturnSum()
    {
        var result = _100usd + _50usd;

        result.Amount.Should().Be(150);
        result.Currency.Should().Be("USD");
    }

    [Fact]
    public void Addition_DifferentCurrency_ShouldThrow()
    {
        var act = () => _100usd + _50eur;

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Multiply_ShouldReturnProduct()
    {
        var result = _100usd * 2;

        result.Amount.Should().Be(200);
        result.Currency.Should().Be("USD");
    }

    [Fact]
    public void Multiply_WhenZeroOrNegative_ShouldThrow()
    {
        var actZero = () => _100usd * 0;
        actZero.Should().Throw<InvalidOperationException>();

        var actNegative = () => _100usd * (-1);
        actNegative.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Equality_SameAmountAndCurrency_ShouldBeEqual()
    {
        var a = new Money(150, "USD");
        var b = new Money(150, "USD");

        (a == b).Should().BeTrue();
    }

    [Fact]
    public void Equality_DifferentAmount_ShouldNotBeEqual()
    {
        var a = new Money(100, "USD");
        var b = new Money(200, "USD");

        (a != b).Should().BeTrue();
    }

    [Fact]
    public void Zero_ShouldReturnZeroAmount()
    {
        Money.Zero.Amount.Should().Be(0);
        Money.Zero.Currency.Should().Be("USD");
    }
}
