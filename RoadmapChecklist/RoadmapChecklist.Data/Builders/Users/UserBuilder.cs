using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoadmapChecklist.Entity;
using RoadmapChecklist.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders.Users
{
    public class UserBuilder : BaseEntityBuilder<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Name).IsRequired().HasMaxLength(500);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(500);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
            builder.Property(u => u.ProfilePicture).HasMaxLength(255);

            // Relations
            builder.HasMany(u => u.Roadmaps)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
