using FreightManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FreightManagement.Infrastructure.Persistence.Configurations
{

    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //CreatedDate 
            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(50);

            builder.Property(x => x.LastModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");

            builder.Property(x => x.Created)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_on");

            builder.Property(x => x.LastModified)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("last_modified_on");
        }
    }

}
