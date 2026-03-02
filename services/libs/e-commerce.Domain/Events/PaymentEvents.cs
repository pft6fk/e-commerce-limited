namespace e_commerce.Domain.Events;

public class PaymentCompletedDomainEvent : DomainEvent
{
    public PaymentId PaymentId { get; }
    public OrderId OrderId { get; }
    public Money Amount { get; }

    public PaymentCompletedDomainEvent(PaymentId paymentId, OrderId orderId, Money amount)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Amount = amount;
    }
}
