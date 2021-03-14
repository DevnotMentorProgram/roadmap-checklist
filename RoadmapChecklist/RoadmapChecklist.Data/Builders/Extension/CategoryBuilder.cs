using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Extension
{
    public class CategoryBuilder : BaseEntityBuilder<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
            builder.Property(c => c.FriendlyUrl).IsRequired().HasMaxLength(300);

            // Relations
            builder.HasMany(c => c.RoadmapCategories)
                .WithOne(rc => rc.Category)
                .HasForeignKey(rc => rc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
