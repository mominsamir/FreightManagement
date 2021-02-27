using FreightManagement.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Vehichles
{
    public class TruckConfiguration: BaseEntityConfigurations<Truck>
    {
        public override void Configure(EntityTypeBuilder<Truck> builder)
        {
            base.Configure(builder);
            builder.ToTable("trucks");

            builder.Property(t => t.NextMaintanceDate)
                .HasColumnName("next_maintance_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

         }
    }
}
