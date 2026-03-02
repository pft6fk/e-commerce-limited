namespace e_commerce.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money TotalPrice => UnitPrice * Quantity;
    public OrderItem(int quantity, Money unitPrice, ProductId productId)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0.");
        if(unitPrice.Amount <= 0)
            throw new ArgumentException("Unit price must be greater than 0.");
        if (string.IsNullOrWhiteSpace(unitPrice.Currency))
            throw new ArgumentException("Unit price currency cannot be null or empty.");

        this.Quantity = quantity;
        this.UnitPrice = unitPrice;
        this.ProductId = productId;
    }
}
