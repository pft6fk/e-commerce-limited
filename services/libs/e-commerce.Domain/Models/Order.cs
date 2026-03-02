namespace e_commerce.Domain.Models;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items;
    public CustomerId CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Money TotalPrice => _items.Sum(x => x.TotalPrice);
    public DateTime CreatedAt { get; private set; }
}
