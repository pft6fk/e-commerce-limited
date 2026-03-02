namespace e_commerce.Tests;

public class OrderTests
{
    // ──────────────────────────────────────────────
    // HELPERS
    // ──────────────────────────────────────────────

    private static Order CreateOrder() => new(CustomerId.New());

    private static OrderItem CreateItem(int qty = 1, decimal price = 100m)
        => new(qty, new Money(price, "USD"), ProductId.New());

    // Helper: creates an order that already has items (ready to pay)
    private static Order CreateOrderWithItems()
    {
        var order = CreateOrder();
        order.AddItem(CreateItem());
        return order;
    }

    // ──────────────────────────────────────────────
    // CONSTRUCTOR TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void Constructor_ShouldCreateOrderWithPendingStatus()
    {
        // Arrange & Act
        var order = CreateOrder();

        // Assert
        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact]
    public void Constructor_ShouldCreateOrderWithEmptyItems()
    {
        var order = CreateOrder();

        order.Items.Should().BeEmpty();
    }

    [Fact]
    public void Constructor_ShouldSetCreatedAtToNow()
    {
        var order = CreateOrder();

        order.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Constructor_ShouldSetCustomerId()
    {
        var customerId = CustomerId.New();

        var order = new Order(customerId);

        order.CustomerId.Should().Be(customerId);
    }

    // ──────────────────────────────────────────────
    // ADD ITEM TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void AddItem_WhenPending_ShouldAddItemToOrder()
    {
        var order = CreateOrder();
        var item = CreateItem();

        order.AddItem(item);

        order.Items.Should().HaveCount(1);
    }

    [Fact]
    public void AddItem_WhenPaid_ShouldThrowDomainException()
    {
        var order = CreateOrderWithItems();
        order.Pay();

        var act = () => order.AddItem(CreateItem());

        // Assert
        act.Should().Throw<DomainException>();
    }

    // ──────────────────────────────────────────────
    // PAY TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void Pay_WhenPendingWithItems_ShouldSetStatusToPaid()
    {
        var order = CreateOrderWithItems();

        order.Pay();

        order.Status.Should().Be(OrderStatus.Paid);
    }

    [Fact]
    public void Pay_WhenNoItems_ShouldThrowDomainException()
    {
        var order = CreateOrder(); // no items added

        var act = () => order.Pay();

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Pay_WhenAlreadyPaid_ShouldThrowDomainException()
    {
        var order = CreateOrderWithItems();
        order.Pay(); // now Paid

        var act = () => order.Pay(); // try again

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Pay_ShouldRaiseOrderPaidDomainEvent()
    {
        var order = CreateOrderWithItems();

        order.Pay();

        order.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<OrderPaidDomainEvent>();
    }

    // ──────────────────────────────────────────────
    // CANCEL TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void Cancel_WhenPending_ShouldSetStatusToCancelled()
    {
        var order = CreateOrder();

        order.Cancel();

        order.Status.Should().Be(OrderStatus.Cancelled);
    }

    [Fact]
    public void Cancel_WhenPaid_ShouldThrowDomainException()
    {
        var order = CreateOrderWithItems();
        order.Pay();

        var act = () => order.Cancel();

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Cancel_WhenShipped_ShouldThrowDomainException()
    {
        var order = CreateOrderWithItems();
        order.Pay();
        order.Ship();

        var act = () => order.Cancel();

        act.Should().Throw<DomainException>();
    }

    // ──────────────────────────────────────────────
    // SHIP TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void Ship_WhenPaid_ShouldSetStatusToShipped()
    {
        var order = CreateOrderWithItems();
        order.Pay();

        order.Ship();

        order.Status.Should().Be(OrderStatus.Shipped);
    }

    [Fact]
    public void Ship_WhenPending_ShouldThrowDomainException()
    {
        var order = CreateOrderWithItems();

        var act = () => order.Ship();

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Ship_ShouldRaiseOrderShippedDomainEvent()
    {
        var order = CreateOrderWithItems();
        order.Pay();
        order.ClearDomainEvents(); 

        order.Ship();

        order.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<OrderShippedDomainEvent>();
    }

    // ──────────────────────────────────────────────
    // COMPLETE TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void Complete_WhenShipped_ShouldSetStatusToCompleted()
    {
        var order = CreateOrderWithItems();
        order.Pay();
        order.Ship();

        order.Complete();

        order.Status.Should().Be(OrderStatus.Completed);
    }

    [Fact]
    public void Complete_WhenPaid_ShouldThrowDomainException()
    {
        var order = CreateOrderWithItems();
        order.Pay();

        var act = () => order.Complete();

        act.Should().Throw<DomainException>();
    }

    // ──────────────────────────────────────────────
    // REMOVE ITEM TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void RemoveItem_WhenPendingAndItemExists_ShouldRemoveItem()
    {
        var order = CreateOrder();
        var item = CreateItem();
        order.AddItem(item);

        order.RemoveItem(item.Id);

        order.Items.Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_WhenItemDoesNotExist_ShouldThrowDomainException()
    {
        var order = CreateOrder();

        var act = () => order.RemoveItem(OrderItemId.New());

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void RemoveItem_WhenNotPending_ShouldThrowDomainException()
    {
        var order = CreateOrderWithItems();
        order.Pay(); 

        var act = () => order.RemoveItem(order.Items.First().Id);

        act.Should().Throw<DomainException>();
    }

    // ──────────────────────────────────────────────
    // TOTAL PRICE TESTS
    // ──────────────────────────────────────────────

    [Fact]
    public void TotalPrice_WithMultipleItems_ShouldReturnCorrectSum()
    {
        var order = CreateOrder();
        order.AddItem(CreateItem(qty: 2, price: 50m));  // 2 * 50 = 100
        order.AddItem(CreateItem(qty: 1, price: 200m)); // 1 * 200 = 200

        order.TotalPrice.Amount.Should().Be(300m);
    }

    [Fact]
    public void TotalPrice_WhenEmpty_ShouldReturnZero()
    {
        var order = CreateOrder();

        order.TotalPrice.Amount.Should().Be(0m);
    }
}
