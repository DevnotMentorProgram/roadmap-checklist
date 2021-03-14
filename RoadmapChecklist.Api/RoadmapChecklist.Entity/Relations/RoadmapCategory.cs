using RoadmapChecklist.Entity.Extension;

namespace RoadmapChecklist.Entity.Relations
{
    public class RoadmapCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public int RoadmapId { get; set; }

        // Relations
        public virtual Category Category { get; set; }
        public virtual Roadmap.Roadmap Roadmap { get; set; }
    }
}
