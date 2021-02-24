using FreightManagement.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Orders
{
    public class OrderItemConfiguration : BaseEntityConfigurations<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            base.Configure(builder);

            builder.ToTable("order_items");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(t => t.LoadCode)
                .HasColumnName("load_code")
                .HasMaxLength(50);

            builder
                .HasOne(s => s.Order)
                .WithMany(s=> s.OrderItems)
                .IsRequired();

            builder
                .HasOne(e => e.Location)
                .WithMany()
                .IsRequired();

            builder
                .HasOne(e => e.FuelProduct)
                .WithMany()
                .IsRequired();

        }
    }
}
