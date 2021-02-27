using FreightManagement.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Vehichles
{
    public class VehicleConfiguration : BaseEntityConfigurations<Vehicle>
    {
        public override void Configure(EntityTypeBuilder<Vehicle> builder)
        {

            base.Configure(builder);

            builder.ToTable("vehicles");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.NumberPlate)
                .HasColumnName("number_plate")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.VIN)
                .HasColumnName("vin" )
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(t => t.Status)
                .HasColumnName("status")
                .HasConversion(
                    u => u.ToString(),
                    u => Enum.Parse<VehicleStatus>(u)
                )
                .IsRequired();

            builder
                .HasMany(t => t.CheckLists)
                .WithOne(t => t.Vehicle)
                .IsRequired();


            builder.HasIndex(c => c.NumberPlate).IsUnique();
            builder.HasIndex(c => c.VIN).IsUnique();

        }
    }
}
