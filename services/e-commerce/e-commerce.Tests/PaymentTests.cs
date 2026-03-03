namespace e_commerce.Tests;

public class PaymentTests
{
    private static Payment CreatePayment()
        => new(OrderId.New(), new Money(100, "USD"));

    [Fact]
    public void Constructor_ShouldSetStatusToNotPaid()
    {
        var payment = CreatePayment();

        payment.Status.Should().Be(PaymentStatus.NotPaid);
    }

    [Fact]
    public void Constructor_ShouldSetOrderIdAndAmount()
    {
        var orderId = OrderId.New();
        var amount = new Money(250, "USD");

        var payment = new Payment(orderId, amount);

        payment.OrderId.Should().Be(orderId);
        payment.Amount.Amount.Should().Be(250);
    }

    [Fact]
    public void Constructor_ShouldNotSetProcessedAt()
    {
        var payment = CreatePayment();

        payment.ProcessedAt.Should().Be(default);
    }

    [Fact]
    public void Constructor_WhenOrderIdIsNull_ShouldThrow()
    {
        var act = () => new Payment(null!, new Money(100, "USD"));

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Pay_WhenNotPaid_ShouldSetStatusToPaid()
    {
        var payment = CreatePayment();

        payment.Pay();

        payment.Status.Should().Be(PaymentStatus.Paid);
    }

    [Fact]
    public void Pay_ShouldSetProcessedAt()
    {
        var payment = CreatePayment();

        payment.Pay();

        payment.ProcessedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Pay_ShouldRaisePaymentCompletedDomainEvent()
    {
        var payment = CreatePayment();

        payment.Pay();

        payment.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<PaymentCompletedDomainEvent>();
    }

    [Fact]
    public void Pay_WhenAlreadyPaid_ShouldThrow()
    {
        var payment = CreatePayment();
        payment.Pay();

        var act = () => payment.Pay();

        act.Should().Throw<DomainException>();
    }
}
