using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Relations
{
    public class RoadmapTagBuilder : BaseEntityBuilder<RoadmapTag>
    {
        public override void Configure(EntityTypeBuilder<RoadmapTag> builder)
        {
            base.Configure(builder);

            builder.Property(cr => cr.TagId).IsRequired();
            builder.Property(cr => cr.RoadmapId).IsRequired();

            //relations
            builder.HasOne(roadmapTag => roadmapTag.Tag)
                .WithMany(tag => tag.RoadmapTags)
                .HasForeignKey(roadmapTag => roadmapTag.TagId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(roadmapCategory => roadmapCategory.Roadmap)
                .WithMany(roadmap => roadmap.Tags)
                .HasForeignKey(roadmapTag => roadmapTag.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
