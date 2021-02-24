using FreightManagement.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightManagement.Infrastructure.Persistence.Configurations.FuelProducts
{
    public class ProductConfiguration : BaseEntityConfigurations<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {

            base.Configure(builder);

            builder.ToTable("products");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .HasColumnName("product_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.UOM)
                .HasColumnName("uom")
                .HasConversion(
                    u => u.ToString(),
                    u => Enum.Parse<UnitOfMeasure>(u)
                )
                .IsRequired();

            builder.Property(t => t.IsActive)
                .HasColumnName("is_active")
                .IsRequired();


            builder.HasIndex(c => c.Name).IsUnique();

        }
    }
}
