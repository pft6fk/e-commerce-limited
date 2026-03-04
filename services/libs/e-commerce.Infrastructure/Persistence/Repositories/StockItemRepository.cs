using e_commerce.Application.Interfaces;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;

namespace e_commerce.Infrastructure.Persistence.Repositories;

public class StockItemRepository : Repository<StockItem, ProductId>, IStockItemRepository
{
    public StockItemRepository(AppDbContext context) : base(context) { }
}
