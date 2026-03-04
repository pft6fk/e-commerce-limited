using e_commerce.Domain.Models;
using e_commerce.Domain.Enums;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                guid => new PaymentId(guid)
            );

        builder.Property(p => p.OrderId)
            .HasConversion(
                id => id.Value,
                guid => new OrderId(guid)
            );

        builder.Property(p => p.Status)
            .HasConversion<int>();

        builder.Property(p => p.ProcessedAt);

        builder.OwnsOne(p => p.Amount, money =>
        {
            money.Property(m => m.Amount).HasColumnName("Amount_Value").HasColumnType("decimal(18,2)");
            money.Property(m => m.Currency).HasColumnName("Amount_Currency").HasMaxLength(3);
        });

        builder.Ignore(p => p.DomainEvents);
    }
}
