using RoadmapChecklist.Entity.Extension;

namespace RoadmapChecklist.Entity.Relations
{
    public class RoadmapTag : BaseEntity
    {
        public int CategoryId { get; set; }
        public int RoadmapId { get; set; }

        // Relations
        public virtual Tag Tag { get; set; }
        public virtual Roadmap.Roadmap Roadmap { get; set; }
    }
}
