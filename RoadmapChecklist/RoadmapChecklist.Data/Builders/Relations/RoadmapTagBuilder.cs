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

            //fields
            builder.HasKey(roadmapTag => roadmapTag.Id);
            builder.Property(roadmapTag => roadmapTag.Id).ValueGeneratedOnAdd();
            builder.Property(roadmapTag => roadmapTag.Tag.Id).IsRequired();
            builder.Property(roadmapTag => roadmapTag.RoadmapId).IsRequired();

            //relations
            builder.HasOne(roadmapTag => roadmapTag.Tag)
                .WithMany(tag => tag.RoadmapTags)
                .HasForeignKey(roadmapTag => roadmapTag.Tag.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(roadmapTag => roadmapTag.Roadmap)
                .WithMany(roadmap => roadmap.Tags)
                .HasForeignKey(roadmapTag => roadmapTag.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
