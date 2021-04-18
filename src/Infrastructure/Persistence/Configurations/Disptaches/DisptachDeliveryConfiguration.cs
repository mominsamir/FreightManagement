using FreightManagement.Domain.Entities.Disptaches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Disptaches
{
    public class DisptachDeliveryConfiguration : BaseEntityConfigurations<DisptachDelivery>
    {
        public override void Configure(EntityTypeBuilder<DisptachDelivery> builder)
        {
            base.Configure(builder);
            builder.ToTable("disptache_deliveries");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.DeliveredQnt)
                .HasColumnName("delivered_qnt")
                .IsRequired()
                .HasDefaultValue(0.0);

            builder.Property(t => t.ReceivedByName)
                .HasColumnName("received_by_name")
                .HasMaxLength(50);

            builder
                .HasOne(e => e.Location)
                .WithMany()
                .IsRequired();

            builder
                .HasOne(e => e.DispatchLoading)
                .WithMany(e => e.Deliveries)
                .IsRequired();

        }
    }
}
