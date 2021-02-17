using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FreightManagement.Domain.Entities.Orders;
using System;
using Microsoft.EntityFrameworkCore;
using FreightManagement.Domain.Entities.Customers;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Orders
{
    class OrderConfiguration : BaseEntityConfigurations<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {

            base.Configure(builder);

            builder.ToTable("orders");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.OrderDate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("order_date")
                .IsRequired();

            builder.Property(t => t.ShipDate)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("ship_date")
                .IsRequired();

            builder.Property(t => t.Status)
                .HasColumnName("is_active")
                .HasMaxLength(20)
                .HasConversion<string>()
                .IsRequired();

            builder
                .HasMany(s => s.OrderItems)
                .WithOne(g => g.Order)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Customer)
                .WithMany()
                .IsRequired();


        }
    }
}
