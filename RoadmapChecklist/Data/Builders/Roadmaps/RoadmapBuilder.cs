using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Roadmaps
{
    public class RoadmapBuilder : BaseEntityBuilder<Roadmap>
    {
        public override void Configure(EntityTypeBuilder<Roadmap> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Name).IsRequired().HasMaxLength(500);
            builder.Property(r => r.Visibility).IsRequired().HasDefaultValue(1);

            // Relations
            builder.HasMany(r => r.Items)
                .WithOne(i => i.Roadmap)
                .HasForeignKey(i => i.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Categories)
                .WithOne(c => c.Roadmap)
                .HasForeignKey(c => c.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Tags)
                .WithOne(t => t.Roadmap)
                .HasForeignKey(t => t.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
