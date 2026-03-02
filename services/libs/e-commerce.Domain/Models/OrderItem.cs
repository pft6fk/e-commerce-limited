namespace e_commerce.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    public Money TotalPrice => UnitPrice * Quantity;
}
