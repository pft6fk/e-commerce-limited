using e_commerce.Domain.Events;

namespace e_commerce.Domain.Models;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
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
    }

    public void AddItem(OrderItem item)
    {
        if (this.Status > OrderStatus.Pending )
            throw new DomainException($"Cannot add item due to status being {this.Status.ToString()}");
        
        _items.Add(item);
    }

    public void Pay()
    {
        if (!_items.Any())
        {
            throw new DomainException("Cannot process payment because no items are within order");
        }    
        if (this.Status != OrderStatus.Pending)
        {  
            throw new DomainException($"Cannot process payment because order status is {this.Status.ToString()}");
        }
        this.Status = OrderStatus.Paid;

        RaiseDomainEvent(new OrderPaidDomainEvent(Id, CustomerId, TotalPrice));

    }

    public void Cancel()
    {
        if (this.Status != OrderStatus.Pending)
            throw new DomainException($"Cannot cancel orders with status of {this.Status.ToString()}");
        this.Status = OrderStatus.Cancelled;
    }

    public void Ship()
    {
        if(this.Status != OrderStatus.Paid)
        {
            throw new DomainException("Cannot ship if not paid");
        }
        this.Status = OrderStatus.Shipped;

        RaiseDomainEvent(new OrderShippedDomainEvent(Id, CustomerId, TotalPrice));
    }

    public void Complete()
    {
        if(this.Status != OrderStatus.Shipped)
        {
            throw new DomainException("Cannot complete since order is not Shipped");
        }
        this.Status = OrderStatus.Completed;
    }

    public void RemoveItem(OrderItemId itemId)
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException($"Cannot remove items from order with status {Status.ToString()}.");

        var item = _items.FirstOrDefault(x => x.Id == itemId);
        if (item is null)
            throw new DomainException($"Item {itemId.Value} not found in order.");

        _items.Remove(item);
    }
}
