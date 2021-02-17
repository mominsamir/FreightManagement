using FreightManagement.Domain.Entities.DriversSchedule;
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

            builder.Property(t => t.CheckListItem)
                .HasColumnName("checklist_item")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasOne(e => e.ScheduleDriverTruckTrailer)
                .WithMany(e=> e.CheckList)
                .IsRequired();

        }
    }
}
