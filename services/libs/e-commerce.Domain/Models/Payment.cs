namespace e_commerce.Domain.Models;

public class Payment : AggregateRoot<PaymentId>
{
    public PaymentId Id { get; private set; } 
    public OrderId OrderId { get; private set; } 
    public Money Amount { get; private set; } 
    public OrderStatus Status { get; private set; } 
    public DateTime ProcessedAt { get; private set; }

    public Payment(OrderId orderId, Money amount)
    {
        if(orderId == null)
            throw new ArgumentNullException(nameof(orderId));

    }
}
