namespace e_commerce.Domain.Events;

public class OrderPaidDomainEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public Money TotalAmount { get; }

    public OrderPaidDomainEvent(OrderId orderId, CustomerId customerId, Money totalAmount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        TotalAmount = totalAmount;
    }
    
}

public class OrderShippedDomainEvent : DomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public Money TotalAmount { get; }

    public OrderShippedDomainEvent(OrderId orderId, CustomerId customerId, Money totalAmount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        TotalAmount = totalAmount;
    }
    
}
