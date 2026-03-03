namespace e_commerce.Tests;

public class StockItemTests
{
    private static StockItem CreateStockItem(int quantity = 100)
        => new(ProductId.New(), quantity);

    [Fact]
    public void Constructor_ShouldSetProductIdAndQuantity()
    {
        var productId = ProductId.New();

        var stock = new StockItem(productId, 50);

        stock.Id.Should().Be(productId);
        stock.AvailableQuantity.Should().Be(50);
    }

    [Fact]
    public void Constructor_WhenNegativeQuantity_ShouldThrow()
    {
        var act = () => new StockItem(ProductId.New(), -1);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_WithDefaultQuantity_ShouldBeZero()
    {
        var stock = new StockItem(ProductId.New());

        stock.AvailableQuantity.Should().Be(0);
    }

    [Fact]
    public void IncreaseStock_ShouldAddToAvailableQuantity()
    {
        var stock = CreateStockItem(100);

        stock.IncreaseStock(5);

        stock.AvailableQuantity.Should().Be(105);
    }

    [Fact]
    public void IncreaseStock_WhenZeroOrNegative_ShouldThrow()
    {
        var stock = CreateStockItem();

        var actZero = () => stock.IncreaseStock(0);
        actZero.Should().Throw<ArgumentException>();

        var actNegative = () => stock.IncreaseStock(-5);
        actNegative.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IncreaseStock_ShouldRaiseStockIncreasedDomainEvent()
    {
        var stock = CreateStockItem();

        stock.IncreaseStock(10);

        stock.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<StockIncreasedDomainEvent>();
    }

    [Fact]
    public void ReduceStock_ShouldSubtractFromAvailableQuantity()
    {
        var stock = CreateStockItem(100);

        stock.ReduceStock(30);

        stock.AvailableQuantity.Should().Be(70);
    }

    [Fact]
    public void ReduceStock_WhenZeroOrNegative_ShouldThrow()
    {
        var stock = CreateStockItem();

        var actZero = () => stock.ReduceStock(0);
        actZero.Should().Throw<ArgumentException>();

        var actNegative = () => stock.ReduceStock(-5);
        actNegative.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ReduceStock_WhenExceedsAvailable_ShouldThrow()
    {
        var stock = CreateStockItem(10);

        var act = () => stock.ReduceStock(20);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void ReduceStock_ShouldRaiseStockReducedDomainEvent()
    {
        var stock = CreateStockItem(100);

        stock.ReduceStock(10);

        stock.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<StockReducedDomainEvent>();
    }
}
