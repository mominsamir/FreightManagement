using FreightManagement.Domain.Entities.Receivable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Receivable
{
    public class InvoiceItemConfiguration : BaseEntityConfigurations<InvoiceItem>
    {
        public override void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            base.Configure(builder);

            builder.ToTable("customer_invoice_items");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Description)
                .HasColumnName("line_description")
                .HasMaxLength(100)
                .IsRequired();


            builder.Property(t => t.Quantity)
                .HasColumnName("quantity")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.Rate)
                .HasColumnName("rate")
                .IsRequired()
                .HasDefaultValue(0);


            builder.Property(t => t.Taxes)
                .HasColumnName("invoice_taxes")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.LineTotal)
                .HasColumnName("line_total")
                .IsRequired()
                .HasDefaultValue(0);

            builder
                .HasOne(s => s.Invoice)
                .WithMany(s => s.InvoiceItems)
                .IsRequired();

        }
    }
}
