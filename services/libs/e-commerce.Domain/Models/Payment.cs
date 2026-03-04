namespace e_commerce.Domain.Models;

public class Payment : AggregateRoot<PaymentId>
{
    public OrderId OrderId { get; private set; } 
    public Money Amount { get; private set; } 
    public PaymentStatus Status { get; private set; } 
    public DateTime ProcessedAt { get; private set; }
    private Payment()
    {
        
    }
    public Payment(OrderId orderId, Money amount)
    {
        if(orderId == null)
            throw new ArgumentNullException(nameof(orderId));

        this.Status = PaymentStatus.NotPaid;
        this.OrderId = orderId;
        this.Amount = amount;
        this.ProcessedAt = default;
    }

    public void Pay()
    {
        if (this.Status == PaymentStatus.Paid)
            throw new DomainException("Cannot pay for paid item");
        
        this.Status = PaymentStatus.Paid;
        this.ProcessedAt = DateTime.UtcNow;
        RaiseDomainEvent(new PaymentCompletedDomainEvent(Id, OrderId, Amount));
    }
}
