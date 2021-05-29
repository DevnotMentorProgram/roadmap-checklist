using RoadmapChecklist.Entity.Extension;
using System;

namespace RoadmapChecklist.Entity.Relations
{
    public class RoadmapTag : BaseEntity
    {
        public Guid TagId { get; set; }
        public Guid RoadmapId { get; set; }

        // Relations
        public virtual Tag Tag { get; set; }
        public virtual Roadmap.Roadmap Roadmap { get; set; }
    }
}
