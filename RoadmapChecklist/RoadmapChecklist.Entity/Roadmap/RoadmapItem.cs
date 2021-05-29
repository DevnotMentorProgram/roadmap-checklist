using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Entity.Roadmap
{
    public class RoadmapItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ParentId { get; set; }
        public Guid RoadmapId { get; set; }
        public int Order { get; set; } = 0;

        // Relations
        public virtual Roadmap Roadmap { get; set; }
        public virtual RoadmapItem Parent { get; set; }
        public virtual ICollection<RoadmapItem> Childiren { get; set; }
    }
}
