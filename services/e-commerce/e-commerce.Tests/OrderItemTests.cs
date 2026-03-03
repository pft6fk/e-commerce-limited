namespace e_commerce.Tests;

public class OrderItemTests
{
    private static OrderItem CreateItem(int qty = 2, decimal price = 50m)
        => new(qty, new Money(price, "USD"), ProductId.New());

    [Fact]
    public void Constructor_ShouldSetAllProperties()
    {
        var productId = ProductId.New();
        var unitPrice = new Money(50, "USD");

        var item = new OrderItem(2, unitPrice, productId);

        item.ProductId.Should().Be(productId);
        item.Quantity.Should().Be(2);
        item.UnitPrice.Amount.Should().Be(50);
    }

    [Fact]
    public void Constructor_WhenQuantityIsZeroOrNegative_ShouldThrow()
    {
        var actZero = () => CreateItem(qty: 0);
        actZero.Should().Throw<ArgumentException>();

        var actNegative = () => CreateItem(qty: -1);
        actNegative.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_WhenUnitPriceIsNull_ShouldThrow()
    {
        var act = () => new OrderItem(1, null!, ProductId.New());

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void TotalPrice_ShouldReturnQuantityTimesUnitPrice()
    {
        var item = CreateItem(qty: 3, price: 50m);

        item.TotalPrice.Amount.Should().Be(150m);
    }
}
