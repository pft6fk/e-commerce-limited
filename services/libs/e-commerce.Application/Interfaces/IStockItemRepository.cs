using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;

namespace e_commerce.Application.Interfaces;

public interface IStockItemRepository : IRepository<StockItem, ProductId>
{
}
