using FreightManagement.Domain.Entities.DriversSchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.DriversSchedule
{
    public class DriverScheduleConfiguration : BaseEntityConfigurations<DriverSchedule>
    {
        public override void Configure(EntityTypeBuilder<DriverSchedule> builder)
        {
            base.Configure(builder);

            builder.ToTable("driver_truck_trailer_schedules");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.StartTime)
                .HasColumnName("start_time")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(t => t.EndTime)
                .HasColumnName("end_time")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.Status)
                .HasColumnName("status")
                .HasConversion<string>();

            builder
                .HasOne(e => e.Trailer)
                .WithMany()
                .IsRequired();

            builder
                .HasOne(e => e.Truck)
                .WithMany()
                .IsRequired();

            builder
                .HasOne(e => e.Driver)
                .WithMany()
                .IsRequired();

            builder
                .HasMany(s => s.CheckList)
                .WithOne(g => g.DriverSchedule)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
