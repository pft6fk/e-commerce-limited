using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;

namespace e_commerce.Application.Interfaces;

public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<List<Product>> GetAllAsync(CancellationToken ct = default);
    Task<List<Product>> GetActiveAsync(CancellationToken ct = default);
}
