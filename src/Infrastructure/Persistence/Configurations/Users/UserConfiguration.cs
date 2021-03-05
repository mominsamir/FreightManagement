using FreightManagement.Domain.Entities.Users;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreightManagement.Infrastructure.Persistence.Configurations.Users
{
    public class UserConfiguration : BaseEntityConfigurations<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Email)
                .HasColumnName("email")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasColumnName("password")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.Role)
                .HasColumnName("role")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(c => c.Email).IsUnique();

        }
    }
}
