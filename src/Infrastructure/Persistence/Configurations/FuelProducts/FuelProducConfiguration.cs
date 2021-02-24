using FreightManagement.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FreightManagement.Infrastructure.Persistence.Configurations.FuelProducts
{
    public class FuelProducConfiguration : BaseEntityConfigurations<FuelProduct>
    {
        public override void Configure(EntityTypeBuilder<FuelProduct> builder)
        {

            base.Configure(builder);

            builder.ToTable("fuel_products");

            builder.Property(t => t.Grade)
                .HasColumnName("fuel_grade")
                .HasMaxLength(20)
                .HasConversion(
                    u => u.ToString(),
                    u => Enum.Parse<FuelGrade>(u)
                )
                .IsRequired();

        }
    }
}
