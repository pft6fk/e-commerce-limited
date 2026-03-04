using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;

namespace e_commerce.Application.Interfaces;

public interface IPaymentRepository : IRepository<Payment, PaymentId>
{
    Task<Payment?> GetByOrderIdAsync(OrderId orderId, CancellationToken ct = default);
}
