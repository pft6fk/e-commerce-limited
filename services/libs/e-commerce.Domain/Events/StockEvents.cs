namespace e_commerce.Domain.Events;

public class StockReducedDomainEvent : DomainEvent
{
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public StockReducedDomainEvent(int quantity, ProductId productId)
    {
        this.Quantity = quantity;
        this.ProductId = productId;
    }
}
public class StockIncreasedDomainEvent : DomainEvent
{
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public StockIncreasedDomainEvent(int quantity, ProductId productId)
    {
        this.Quantity = quantity;
        this.ProductId = productId;
    }
}
