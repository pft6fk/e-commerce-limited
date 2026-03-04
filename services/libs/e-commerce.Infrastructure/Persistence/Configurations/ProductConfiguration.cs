using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                guid => new ProductId(guid)
            );

        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.IsActive);

        builder.OwnsOne(p => p.Price, money =>
        {
            money.Property(m => m.Amount).HasColumnName("Price_Amount").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("Price_Currency").HasMaxLength(3);
        });

        builder.Ignore(p => p.DomainEvents);
    }
}
