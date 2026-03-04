using e_commerce.Application.Interfaces;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Persistence.Repositories;

public class PaymentRepository : Repository<Payment, PaymentId>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context) { }

    public async Task<Payment?> GetByOrderIdAsync(OrderId orderId, CancellationToken ct = default)
    {
        return await Context.Payments
            .FirstOrDefaultAsync(p => p.OrderId == orderId, ct);
    }
}
