using System.Diagnostics;

namespace e_commerce.Domain.Models;

public class Product: AggregateRoot<ProductId>
{
    public string Name { get; private set; } = null!;
    public string Description  { get; private set; } = null!;
    public Money Price { get; private set; } = null!;
    public bool IsActive { get; private set; }
    private Product()
    {
        
    }
    public Product(string name, Money price, string description, bool isActive)
        : base(ProductId.New())
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or empty.");
        if (price is null || string.IsNullOrEmpty(price.Currency) || price.Amount < 0)
            throw new ArgumentNullException(nameof(price));

        this.Name = name;
        this.Price = price;
        this.Description = description;
        this.IsActive = isActive;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    public void UpdatePrice(Money newPrice)
    {
        if (newPrice is null || newPrice.Amount < 0)
            throw new DomainException("Price must be zero or positive.");

        Price = newPrice;
    }

}
