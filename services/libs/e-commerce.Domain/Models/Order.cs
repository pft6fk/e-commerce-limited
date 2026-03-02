namespace e_commerce.Domain.Models;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items;
    public CustomerId CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Money TotalPrice => _items.Select(x => x.TotalPrice).Aggregate(Money.Zero, (acc, next) => acc + next);
    public DateTime CreatedAt { get; private set; }
    
    public void AddItem(OrderItem item)
    {
        if (this.Status > OrderStatus.Pending )
            throw new DomainException($"Cannot add item due to status being {this.Status}");
        
        _items.Add(item);
    }
}
