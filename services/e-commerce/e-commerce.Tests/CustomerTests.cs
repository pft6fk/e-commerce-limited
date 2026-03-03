namespace e_commerce.Tests;

public class CustomerTests
{
    private readonly string _firstName = "John";
    private readonly string _lastName = "Doe";
    private readonly string _validEmail = "email@gmail.com";
    private readonly string _nonvalidEmail = "emailgmail.com";

    [Fact]
    public void Constructor_ShouldSetAllProperties()
    {
        var customer = new Customer(_firstName, _lastName, _validEmail);
        customer.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WhenEmailIsEmpty_ShouldThrow()
    {
        var customer = new Customer(_firstName, _lastName, string.Empty);
        var act = () => customer.Should().BeNull();
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_WhenEmailIsNonValid_ShouldThrow()
    {
        var customer = new Customer(_firstName, _lastName, _nonvalidEmail);
        customer.RegisteredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Constructor_ShouldSetRegisteredAtToNow()
    {
        var customer = new Customer(_firstName, _lastName, _validEmail);
        customer.RegisteredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
