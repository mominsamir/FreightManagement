using FreightManagement.Domain.Entities.Disptaches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FreightManagement.Infrastructure.Persistence.Configurations.Disptaches
{
    public class DispatchLoadingConfiguration : BaseEntityConfigurations<DispatchLoading>
    {
        public override void Configure(EntityTypeBuilder<DispatchLoading> builder)
        {
            base.Configure(builder);
            builder.ToTable("disptache_loadings");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.BillOfLoading)
                .HasColumnName("bill_of_lading")
                .HasMaxLength(50);

            builder.Property(t => t.LoadedQuantity)
                .HasColumnName("loaded_quantity")
                .HasDefaultValue(0);

            builder
                 .HasMany(s => s.Deliveries)
                 .WithOne(g => g.DispatchLoading)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);


            builder
                 .HasOne(s => s.Dispatch)
                 .WithMany(g => g.DispatchLoading)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.OrderItem)
                .WithMany()
                .IsRequired();

        }
    }
}
