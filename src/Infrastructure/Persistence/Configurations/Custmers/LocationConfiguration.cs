using FreightManagement.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FreightManagement.Infrastructure.Persistence.Configurations.Custmers
{
    public class LocationConfiguration : BaseEntityConfigurations<Location>
    {
        public override void Configure(EntityTypeBuilder<Location> builder)
        {

            base.Configure(builder);

            builder.ToTable("customer_locations");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("location_name")
                .HasMaxLength(200)
                .IsRequired();


            builder.OwnsOne(
                o => o.DeliveryAddress,
                a =>
                {
                    a.Property(p => p.Street)
                        .HasColumnName("d_street")
                        .HasMaxLength(200)
                        .IsRequired();
                    a.Property(p => p.City)
                        .HasColumnName("d_city")
                        .HasMaxLength(200)
                        .IsRequired();
                    a.Property(p => p.State)
                        .HasColumnName("d_state")
                        .HasMaxLength(25)
                        .IsRequired();
                    a.Property(p => p.Country)
                        .HasColumnName("d_country")
                        .HasMaxLength(20)
                        .IsRequired();
                    a.Property(p => p.ZipCode)
                        .HasColumnName("d_zip_code")
                        .HasMaxLength(12)
                        .IsRequired();
                }).Navigation(p => p.DeliveryAddress).IsRequired();

                builder
                .HasMany(l => l.Customers)
                .WithMany(l => l.Locations);

        }
    }
}
