using e_commerce.Application.Interfaces;
using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Infrastructure.Persistence.Repositories;

public class CustomerRepository : Repository<Customer, CustomerId>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await Context.Customers
            .FirstOrDefaultAsync(c => c.Email == email, ct);
    }
}
