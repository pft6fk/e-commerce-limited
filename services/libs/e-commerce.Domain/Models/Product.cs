using System.Diagnostics;

namespace e_commerce.Domain.Models;

public class Product: AggregateRoot<ProductId>
{
    public ProductId Id { get; private set; }
    public string Name { get; private set; }
    public string Description  { get; private set; }
    public Money Price { get; private set; }
    public bool IsActive { get; private set; }

    public Product(string name, Money price, string desciption)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or empty.");
        if (price == null || string.IsNullOrEmpty(price.Currency) || price.Amount < 0)
            throw new ArgumentNullException(nameof(price));

        this.Id = ProductId.New();
        this.Name = name;
        this.Price = price;
    }
    public void Activate()
    {
        IsActive = true;
    }
    public void Deactivate()
    {
        IsActive = false;
    }
}
