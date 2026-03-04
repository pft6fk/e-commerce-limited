using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id)
            .HasConversion(
                id => id.Value,
                guid => new OrderItemId(guid)
            );

        builder.Property(oi => oi.ProductId)
            .HasConversion(
                id => id.Value,
                guid => new ProductId(guid)
            );

        builder.Property(oi => oi.Quantity);

        builder.OwnsOne(oi => oi.UnitPrice, money =>
        {
            money.Property(m => m.Amount).HasColumnName("UnitPrice_Amount").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("UnitPrice_Currency").HasMaxLength(3);
        });

        builder.Ignore(oi => oi.TotalPrice);
    }
}
