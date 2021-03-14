using Data.Builders.Extension;
using Data.Builders.Relations;
using Data.Builders.Roadmaps;
using Data.Builders.Users;
using Microsoft.EntityFrameworkCore;
using RoadmapChecklist.Entity;
using RoadmapChecklist.Entity.Extension;
using RoadmapChecklist.Entity.Relations;
using RoadmapChecklist.Entity.Roadmap;
using RoadmapChecklist.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class RoadmapChecklistDbContext : DbContext
    {
        public RoadmapChecklistDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Roadmap> Roadmaps { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RoadmapCategory> RoadmapCategoryRelations { get; set; }
        public DbSet<RoadmapCopy> RoadmapCopies { get; set; }
        public DbSet<RoadmapItem> RoadmapItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RoadmapTag> RoadmapTagRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryBuilder());
            modelBuilder.ApplyConfiguration(new RoadmapCategoryBuilder());
            modelBuilder.ApplyConfiguration(new RoadmapBuilder());
            modelBuilder.ApplyConfiguration(new RoadmapCopyBuilder());
            modelBuilder.ApplyConfiguration(new TagBuilder());
            modelBuilder.ApplyConfiguration(new RoadmapTagBuilder());
            modelBuilder.ApplyConfiguration(new UserBuilder());
        }
    }
}
