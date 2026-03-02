namespace e_commerce.Domain.Models;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
    public OrderId Id { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items;
    public CustomerId CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Money TotalPrice => _items.Select(x => x.TotalPrice).Aggregate(Money.Zero, (acc, next) => acc + next);
    public DateTime CreatedAt { get; private set; }

    public Order(CustomerId customerId)
    {
        this.CustomerId = customerId;
        this.Status = OrderStatus.Pending;
        this.CreatedAt = DateTime.UtcNow;
        this.Id = OrderId.New();
    }

    public void AddItem(OrderItem item)
    {
        if (this.Status > OrderStatus.Pending )
            throw new DomainException($"Cannot add item due to status being {this.Status}");
        
        _items.Add(item);
    }

    public bool Pay(OrderId id)
    {
        if (!_items.Any())
        {
            throw new DomainException("Cannot process payment because no items are within order");
        }    
        if (this.Status == OrderStatus.Pending)
        {  
            this.Status = OrderStatus.Paid;
            return true;
        }

        return false;
    }

    public bool Cancel(OrderId id)
    {
        this.Status = OrderStatus.Cancelled;

        return true;
    }

    public bool Ship(OrderId id)
    {
        if(this.Status == OrderStatus.Paid )
        {
            this.Status = OrderStatus.Shipped;
            return true;
        }
        throw new DomainException("Cannot ship if not paid");
    }

    public bool Complete(OrderId id)
    {
        if(this.Status == OrderStatus.Shipped)
        {
            this.Status = OrderStatus.Completed;
            return true;
        }
        throw new DomainException("Cannot complete since order is not Shipped");
    }

    public bool RemoveItem(ProductId itemId)
    {
        return true;
    }
}
