namespace e_commerce.Domain.Models;

public class StockItem : AggregateRoot<ProductId>
{
    public int AvailableQuantity { get; private set; }
    public byte[] RowVersion { get; private set; }
    public StockItem(ProductId productId, int quantity = 0)
    {
        if (quantity < 0)
            throw new ArgumentException("Cannot create negative amount of stock");
        this.Id = productId;
    }
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException($"Cannot increase stock amount by {quantity}.");

        AvailableQuantity += quantity;
    }
    
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException($"Cannot decrease stock amount by {quantity}.");
        if (AvailableQuantity < quantity)
            throw new DomainException($"Not enough stock. Available: {AvailableQuantity}, requested: {quantity}}");
        AvailableQuantity -= quantity;
    }
}
