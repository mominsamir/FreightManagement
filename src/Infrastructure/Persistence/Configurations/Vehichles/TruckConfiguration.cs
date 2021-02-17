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
        }
    }
}
