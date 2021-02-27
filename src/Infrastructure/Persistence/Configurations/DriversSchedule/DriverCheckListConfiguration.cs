using FreightManagement.Domain.Entities.DriversSchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.DriversSchedule
{
    public class DriverCheckListConfiguration : BaseEntityConfigurations<DriverCheckList>
    {
        public override void Configure(EntityTypeBuilder<DriverCheckList> builder)
        {
            base.Configure(builder);

            builder.ToTable("driver_check_lists");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.IsChecked)
                .HasColumnName("is_checked")
                .IsRequired();

            builder.HasOne(s => s.CheckList)
                .WithMany()
                .IsRequired();

            builder
                .HasOne(e => e.DriverSchedule)
                .WithMany(e=> e.CheckList)
                .IsRequired();

        }
    }
}
