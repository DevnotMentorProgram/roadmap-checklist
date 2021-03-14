using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Relations
{
    public class RoadmapCopyBuilder : BaseEntityBuilder<RoadmapCopy>
    {
        public void Configure(EntityTypeBuilder<RoadmapCopy> builder)
        {
            builder.HasKey(rc => new { rc.SourceId, rc.TargetId });

            builder.HasOne(rc => rc.SourceRoudmap)
                .WithMany(r => r.Sources)
                .HasForeignKey(rc => rc.SourceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(rc => rc.TargetRoadmap)
                .WithMany(r => r.Targets)
                .HasForeignKey(rc => rc.TargetId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
