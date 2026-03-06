using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Persistence.Configurations;

public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItems");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                guid => new ProductId(guid)
            );

        builder.Property(s => s.AvailableQuantity);

        builder.Ignore(s => s.RowVersion);
        builder.UseXminAsConcurrencyToken();

        builder.Ignore(s => s.DomainEvents);
    }
}
