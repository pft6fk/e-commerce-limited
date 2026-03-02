namespace e_commerce.Domain.Models;

public class StockItem : AggregateRoot<ProductId>
{
    public int AvailableQuantity { get; private set; }
    
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException($"Cannot increase stock amount by {quantity}");

        AvailableQuantity = quantity;
    }
    
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException($"Cannot decrease stock amount by {quantity}");

        AvailableQuantity = quantity;
    }
}
