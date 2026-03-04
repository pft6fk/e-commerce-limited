using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;

namespace e_commerce.Application.Interfaces;

public interface IOrderRepository : IRepository<Order, OrderId>
{
    Task<List<Order>> GetByCustomerIdAsync(CustomerId customerId, CancellationToken ct = default);
}
