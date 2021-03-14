using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Extension
{
    public class TagBuilder : BaseEntityBuilder<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(255);
            builder.Property(t => t.FriendlyUrl).IsRequired().HasMaxLength(300);

            // Relations
            builder.HasMany(t => t.RoadmapTags)
                .WithOne(rt => rt.Tag)
                .HasForeignKey(rt => rt.Tag.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
