using FreightManagement.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Vendors
{
    public class VendorConfiguration : BaseEntityConfigurations<Vendor>
    {
        public override void Configure(EntityTypeBuilder<Vendor> builder)
        {

            base.Configure(builder);

            builder.ToTable("vendors");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("vendor_name")
                .HasMaxLength(200)
                .IsRequired();


            builder.Property(t => t.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .IsRequired();

            builder.OwnsOne(
                t => t.Email,
                o => {
                    o.Property(p => p.Value)
                    .HasColumnName("email").HasMaxLength(200).IsRequired();
                }).Navigation(p => p.Email).IsRequired();

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
                }).Navigation(p => p.Address).IsRequired();

        }
    }
}
