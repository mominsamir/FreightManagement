using FreightManagement.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FreightManagement.Infrastructure.Persistence.Configurations.Vehichles
{
    public class VehicleCheckListConfiguration : BaseEntityConfigurations<VehicleCheckList>
    {
        public override void Configure(EntityTypeBuilder<VehicleCheckList> builder)
        {
            base.Configure(builder);

            builder.ToTable("vehichle_check_lists");

            builder.HasKey(prop => prop.Id);

            builder.Property(t => t.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            builder
                .Property(t => t.Note)
                .HasColumnName("note")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasOne(t => t.Vehicle)
                .WithMany(t=> t.CheckLists)
                .IsRequired();

        }
    }
}
