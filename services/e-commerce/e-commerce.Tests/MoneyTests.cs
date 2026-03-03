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
        money.Should().NotBeNull();
    }


    [Fact]
    public void Constructor_WhenCurrencyIsEmpty_ShouldThrow()
    {
        var money = new Money(100, string.Empty);
        var act = () => money;
        act.Should().Throw<ArgumentNullException>();
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
        var result = _100usd + _50eur;
        var act = () => result;

        act.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void Multiply_ShouldReturnProduct()
    {
        var _200usd = _100usd * 2;
        _200usd.Amount.Should().Be(200);
    }


    [Fact]
    public void Multiply_WhenZeroOrNegative_ShouldThrow()
    {
        var multiplyByZero = _100usd * 0;
        var actOfZero = () => multiplyByZero;
        actOfZero.Should().Throw<InvalidOperationException>();

        var multiplyByNegative = _100usd * (-1);
        var actOfNegative = () => multiplyByNegative;
        actOfNegative.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void Equality_SameAmountAndCurrency_ShouldBeEqual()
    {
        var _150usd = new Money(150, "USD");
        var result = _100usd + _50usd;
        var validation = _150usd == result;
        validation.Should().BeTrue();
    }


    [Fact]
    public void Equality_DifferentAmount_ShouldNotBeEqual()
    {
        var _151usd = new Money(151, "USD");
        var result = _100usd + _50usd;
        var validation = _151usd == result;
        validation.Should().BeFalse();
    }


    [Fact]
    public void Zero_ShouldReturnZeroAmount()
    {
        var zeroMoney = new Money(0, "USD");
        zeroMoney.Should().Be(Money.Zero);
    }


}
