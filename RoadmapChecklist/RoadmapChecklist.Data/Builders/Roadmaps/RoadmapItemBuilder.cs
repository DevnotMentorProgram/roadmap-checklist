using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Roadmaps
{
    public class RoadmapItemItemBuilder : BaseEntityBuilder<RoadmapItem>
    {
        public override void Configure(EntityTypeBuilder<RoadmapItem> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Title).IsRequired().HasMaxLength(1000);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(5000);
            builder.Property(r => r.TargetDate);
            builder.Property(r => r.EndDate);
            builder.Property(r => r.ParentId);
            builder.Property(r => r.RoadmapId).IsRequired();
            builder.Property(r => r.Order).IsRequired();

            // Relations
            builder.HasOne(r => r.Roadmap)
                .WithMany(i => i.Items)
                .HasForeignKey(i => i.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasMany(r => r.Childiren)
                .WithOne(i => i.Parent)
                .HasForeignKey(i => i.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
