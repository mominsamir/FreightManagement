using FreightManagement.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Custmers
{
    public class LocationTankConfiguration : BaseEntityConfigurations<LocationTank>
    {
        public override void Configure(EntityTypeBuilder<LocationTank> builder)
        {

            base.Configure(builder);

            builder.ToTable("customer_location_tanks");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("rack_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnName("tank_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Capactity)
                .HasColumnName("tank_capacity")
                .IsRequired();

            builder
                .HasOne(s => s.Location)
                .WithMany(g => g.Tanks)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
