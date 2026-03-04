using e_commerce.Application.Interfaces;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product, ProductId>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<List<Product>> GetAllAsync(CancellationToken ct = default)
    {
        return await Context.Products.ToListAsync(ct);
    }

    public async Task<List<Product>> GetActiveAsync(CancellationToken ct = default)
    {
        return await Context.Products
            .Where(p => p.IsActive)
            .ToListAsync(ct);
    }
}
