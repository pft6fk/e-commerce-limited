using e_commerce.Application.Interfaces;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Persistence.Repositories;

public class OrderRepository : Repository<Order, OrderId>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public override async Task<Order?> GetByIdAsync(OrderId id, CancellationToken ct = default)
    {
        return await Context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, ct);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(CustomerId customerId, CancellationToken ct = default)
    {
        return await Context.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(ct);
    }
}
