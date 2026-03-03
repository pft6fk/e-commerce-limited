namespace e_commerce.Tests;

public class PaymentTests
{
    // ──────────────────────────────────────────────
    // HELPERS
    // ──────────────────────────────────────────────



    [Fact]
    public void Constructor_ShouldSetStatusToNotPaid()
    {
    }


    [Fact]
    public void Constructor_ShouldSetOrderIdAndAmount()
    {
    }


    [Fact]
    public void Constructor_ShouldNotSetProcessedAt()
    {
    }


    [Fact]
    public void Constructor_WhenOrderIdIsNull_ShouldThrow()
    {
    }


    [Fact]
    public void Pay_WhenNotPaid_ShouldSetStatusToPaid()
    {
    }


    [Fact]
    public void Pay_ShouldSetProcessedAt()
    {
    }


    [Fact]
    public void Pay_ShouldRaisePaymentCompletedDomainEvent()
    {
    }


    [Fact]
    public void Pay_WhenAlreadyPaid_ShouldThrow()
    {
    }


}
