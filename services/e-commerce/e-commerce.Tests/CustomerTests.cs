namespace e_commerce.Tests;

public class CustomerTests
{
    private readonly string _firstName = "John";
    private readonly string _lastName = "Doe";
    private readonly string _validEmail = "email@gmail.com";

    [Fact]
    public void Constructor_ShouldSetAllProperties()
    {
        var customer = new Customer(_firstName, _lastName, _validEmail);

        customer.FirstName.Should().Be(_firstName);
        customer.LastName.Should().Be(_lastName);
        customer.Email.Should().Be(_validEmail);
    }

    [Fact]
    public void Constructor_WhenEmailIsEmpty_ShouldThrow()
    {
        var act = () => new Customer(_firstName, _lastName, string.Empty);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_WhenEmailIsNonValid_ShouldThrow()
    {
        var act = () => new Customer(_firstName, _lastName, "emailgmail.com");

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_ShouldSetRegisteredAtToNow()
    {
        var customer = new Customer(_firstName, _lastName, _validEmail);

        customer.RegisteredAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
