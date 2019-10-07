using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.Property(n => n.Heading)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(n => n.Content)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.HasMany(nc => nc.NewsCategories)
                   .WithOne(n => n.News)
                   .HasForeignKey(n => n.NewsId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.User)
                   .WithMany(n => n.News)
                   .HasForeignKey(u => u.UserId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(n => n.Comments)
                   .WithOne(c => c.News)
                   .HasForeignKey(n => n.NewsId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Pictures)
                   .WithOne(n => n.News)
                   .HasForeignKey(n => n.NewsId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
