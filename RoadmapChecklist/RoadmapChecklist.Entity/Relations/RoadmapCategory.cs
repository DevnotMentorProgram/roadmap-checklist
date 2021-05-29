using RoadmapChecklist.Entity.Extension;
using System;

namespace RoadmapChecklist.Entity.Relations
{
    public class RoadmapCategory : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Guid RoadmapId { get; set; }

        // Relations
        public virtual Category Category { get; set; }
        public virtual Roadmap.Roadmap Roadmap { get; set; }
    }
}
