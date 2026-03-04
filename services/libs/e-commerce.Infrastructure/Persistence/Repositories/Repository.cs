using e_commerce.Application.Interfaces;
using e_commerce.Domain.Common;

namespace e_commerce.Infrastructure.Persistence.Repositories;

public abstract class Repository<T, TId> : IRepository<T, TId> where T : AggregateRoot<TId>
{
    protected readonly AppDbContext Context;

    protected Repository(AppDbContext context)
    {
        Context = context;
    }

    public virtual async Task<T?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        return await Context.Set<T>().FindAsync(new object[] { id! }, ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await Context.Set<T>().AddAsync(entity, ct);
    }
}
