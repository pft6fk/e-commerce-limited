using e_commerce.Domain.Models;
using e_commerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_commerce.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(
                id => id.Value,
                guid => new OrderId(guid)
            );

        builder.Property(o => o.CustomerId)
            .HasConversion(
                id => id.Value,
                guid => new CustomerId(guid)
            );

        builder.Property(o => o.Status)
            .HasConversion<int>();

        builder.Property(o => o.CreatedAt);

        builder.Ignore(o => o.TotalPrice);

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(o => o.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(o => o.DomainEvents);
    }
}
