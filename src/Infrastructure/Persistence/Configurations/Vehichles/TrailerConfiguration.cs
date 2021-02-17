using FreightManagement.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Vehichles
{
   public class TrailerConfiguration : BaseEntityConfigurations<Trailer>
    {
        public override void Configure(EntityTypeBuilder<Trailer> builder)
        {
            base.Configure(builder);

            builder.ToTable("trailers");

            builder.Property(t => t.Capacity)
                .HasColumnName("capacity")
                .HasDefaultValue(0.0)
                .IsRequired();

            builder
                .Property(t => t.Compartment)
                .HasColumnName("compartment")
                .HasDefaultValue(0.0)
                .IsRequired();

        }
    }
}
