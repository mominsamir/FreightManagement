using FreightManagement.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : BaseEntityConfigurations<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {

            base.Configure(builder);

            builder.ToTable("customers");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("customer_name")
                .HasMaxLength(200)
                .IsRequired();


            builder.Property(t => t.IsActive)
                .HasColumnName("is_active")
                .IsRequired();


            builder.OwnsOne(
                o => o.BillingAddress,
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
                }).Navigation(p => p.BillingAddress)
                .IsRequired();

                builder
                    .HasMany(l => l.Locations)
                    .WithMany(l => l.Customers);

        }
    }
}
