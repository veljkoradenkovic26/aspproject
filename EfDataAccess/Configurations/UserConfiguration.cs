using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.Username)
                .HasMaxLength(15)
                .IsRequired();

            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.HasOne(r => r.Role).WithMany(u => u.Users).HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
