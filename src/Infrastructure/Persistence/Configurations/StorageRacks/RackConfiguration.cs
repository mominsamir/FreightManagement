using FreightManagement.Domain.Entities.StorageRack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FreightManagement.Infrastructure.Persistence.Configurations.StorageRacks
{
    public class RackConfiguration : BaseEntityConfigurations<Rack>
    {
        public override void Configure(EntityTypeBuilder<Rack> builder)
        {

            base.Configure(builder);

            builder.ToTable("racks");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("rack_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.IRSCode)
                .HasColumnName("irs_code")
                .HasMaxLength(30)

                .IsRequired();

            builder.Property(t => t.IsActive)
                .HasColumnName("is_active")
                .HasMaxLength(30)
                .IsRequired();


            builder.OwnsOne(
                o => o.Address,
                a =>
                {
                    a.Property(p => p.Street)
                        .HasColumnName("street")
                        .HasMaxLength(200)
                        .IsRequired();
                    a.Property(p => p.City)
                        .HasColumnName("city")
                        .HasMaxLength(200)
                        .IsRequired();
                    a.Property(p => p.State)
                        .HasColumnName("state")
                        .HasMaxLength(25)
                        .IsRequired();
                    a.Property(p => p.Country)
                        .HasColumnName("country")
                        .HasMaxLength(20)
                        .IsRequired();
                    a.Property(p => p.ZipCode)
                        .HasColumnName("zip_code")
                        .HasMaxLength(12)
                        .IsRequired();
                }).Navigation(p=> p.Address).IsRequired();

            builder.HasIndex(c => c.IRSCode).IsUnique();

        }
    }
}
