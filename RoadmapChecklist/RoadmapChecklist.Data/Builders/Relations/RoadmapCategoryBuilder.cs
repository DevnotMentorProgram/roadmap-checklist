using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Relations
{
    public class RoadmapCategoryBuilder : BaseEntityBuilder<RoadmapCategory>
    {
        public override void Configure(EntityTypeBuilder<RoadmapCategory> builder)
        {
            base.Configure(builder);

            builder.Property(cr => cr.CategoryId).IsRequired();
            builder.Property(cr => cr.RoadmapId).IsRequired();

            //relations
            builder.HasOne(roadmapCategory => roadmapCategory.Category)
                .WithMany(category => category.RoadmapCategories)
                .HasForeignKey(roadmapTag => roadmapTag.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(roadmapCategory => roadmapCategory.Roadmap)
                .WithMany(roadmap => roadmap.Categories)
                .HasForeignKey(roadmapTag => roadmapTag.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
