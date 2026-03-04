using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;

namespace e_commerce.Application.Interfaces;

public interface ICustomerRepository : IRepository<Customer, CustomerId>
{
    Task<Customer?> GetByEmailAsync(string email, CancellationToken ct = default);
}
