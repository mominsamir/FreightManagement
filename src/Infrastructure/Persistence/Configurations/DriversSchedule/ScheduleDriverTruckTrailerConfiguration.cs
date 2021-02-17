using FreightManagement.Domain.Entities.DriversSchedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.DriversSchedule
{
    public class ScheduleDriverTruckTrailerConfiguration : BaseEntityConfigurations<ScheduleDriverTruckTrailer>
    {
        public override void Configure(EntityTypeBuilder<ScheduleDriverTruckTrailer> builder)
        {
            base.Configure(builder);

            builder.ToTable("driver_truck_trailer_schedules");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.StartTime)
                .HasColumnName("start_time")
                .IsRequired();

            builder.Property(t => t.EndTime)
                .HasColumnName("end_time")
                .IsRequired();

            builder
                .HasOne(e => e.Driver)
                .WithMany()
                .IsRequired();

            builder
                .HasOne(e => e.Trailer)
                .WithMany()
                .IsRequired();

            builder
                .HasOne(e => e.Truck)
                .WithMany()
                .IsRequired();

            builder
                .HasMany(s => s.CheckList)
                .WithOne(g => g.ScheduleDriverTruckTrailer)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
