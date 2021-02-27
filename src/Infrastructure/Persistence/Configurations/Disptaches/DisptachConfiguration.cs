using FreightManagement.Domain.Entities.Disptaches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Disptaches
{
    public class DisptachConfiguration : BaseEntityConfigurations<Dispatch>
    {
        public override void Configure(EntityTypeBuilder<Dispatch> builder)
        {
            base.Configure(builder);

            builder.ToTable("disptaches");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Status)
                .HasColumnName("status")
                .HasConversion(
                        s=> s.ToString(),
                        s => Enum.Parse<DispatchStatus>(s)
                    )
                .IsRequired();

            builder.Property(t => t.DispatchDateTime)
                .HasColumnName("disptach_date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(t => t.DispatchStartTime)
                .HasColumnName("disptach_start_time")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.DispatchEndTime)
                .HasColumnName("disptach_end_time")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.RackArrivalTime)
                .HasColumnName("rack_arrived_on")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.RackLeftOnTime)
                .HasColumnName("rack_left_on")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.LoadingStartTime)
                .HasColumnName("loading_start_time")
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.LoadingEndTime)
                .HasColumnName("loadng_end_time")
                .HasColumnType("timestamp with time zone");

            builder
                 .HasMany(s => s.DispatchLoading)
                 .WithOne(g => g.Dispatch)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.DriverSchedule)
                .WithMany()
                .IsRequired();

        }
    }
}
