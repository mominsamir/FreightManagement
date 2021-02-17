using FreightManagement.Domain.Entities.Payables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Payables
{
    public class InvoiceConfiguration : BaseEntityConfigurations<Invoice>
    {
        public override void Configure(EntityTypeBuilder<Invoice> builder)
        {
            base.Configure(builder);

            builder.ToTable("vendor_invoice");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.InvoiceNum)
                .HasColumnName("invoice_num")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.InvoiceDate)
                .HasColumnName("invoice_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(t => t.DueDate)
                .HasColumnName("invoice_due_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(t => t.Notes)
                .HasColumnName("notes")
                .HasMaxLength(500);

            builder.Property(t => t.Taxes)
                .HasColumnName("invoice_taxes")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.Total)
                .HasColumnName("invoice_total")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion<string>();

            builder
                .HasOne(e => e.Vendor)
                .WithMany()
                .IsRequired();

            builder
                .HasMany(s => s.InvoiceItems)
                .WithOne(g => g.Invoice)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
