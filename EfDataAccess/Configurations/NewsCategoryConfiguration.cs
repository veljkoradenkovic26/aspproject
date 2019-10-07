using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
    public class NewsCategoryConfiguration : IEntityTypeConfiguration<NewsCategories>
    {
        public void Configure(EntityTypeBuilder<NewsCategories> builder)
        {
            builder.HasKey(nc => new { nc.CategoryId, nc.NewsId });
        }
    }
}
