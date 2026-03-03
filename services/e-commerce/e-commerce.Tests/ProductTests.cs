namespace e_commerce.Tests;

public class ProductTests
{
    private static Money ValidPrice => new(100, "USD");

    private static Product CreateProduct(
        string name = "Laptop",
        decimal price = 100m,
        string description = "A laptop",
        bool isActive = true)
        => new(name, new Money(price, "USD"), description, isActive);

    [Fact]
    public void Constructor_ShouldSetAllProperties()
    {
        var product = CreateProduct();

        product.Name.Should().Be("Laptop");
        product.Description.Should().Be("A laptop");
        product.Price.Amount.Should().Be(100m);
        product.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Constructor_WhenNameIsEmpty_ShouldThrow()
    {
        var act = () => CreateProduct(name: "");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_WhenPriceIsNull_ShouldThrow()
    {
        var act = () => new Product("Laptop", null!, "A laptop", true);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_WhenPriceIsNegative_ShouldThrow()
    {
        var act = () => CreateProduct(price: -10m);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Activate_ShouldSetIsActiveToTrue()
    {
        var product = CreateProduct(isActive: false);

        product.Activate();

        product.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        var product = CreateProduct(isActive: true);

        product.Deactivate();

        product.IsActive.Should().BeFalse();
    }

    [Fact]
    public void UpdatePrice_WithValidPrice_ShouldUpdatePrice()
    {
        var product = CreateProduct(price: 100m);
        var newPrice = new Money(200, "USD");

        product.UpdatePrice(newPrice);

        product.Price.Amount.Should().Be(200m);
    }

    [Fact]
    public void UpdatePrice_WithNullPrice_ShouldThrow()
    {
        var product = CreateProduct();

        var act = () => product.UpdatePrice(null!);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void UpdatePrice_WithNegativePrice_ShouldThrow()
    {
        var product = CreateProduct();

        var act = () => product.UpdatePrice(new Money(-10, "USD"));

        act.Should().Throw<DomainException>();
    }
}
