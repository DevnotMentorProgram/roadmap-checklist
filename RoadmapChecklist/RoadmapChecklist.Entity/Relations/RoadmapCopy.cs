using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Entity.Relations
{
    public class RoadmapCopy : BaseEntity
    {
        public Guid SourceId { get; set; }
        public Guid TargetId { get; set; }

        // Relations
        public virtual Roadmap.Roadmap SourceRoadmap { get; set; }
        public virtual Roadmap.Roadmap TargetRoadmap { get; set; }
    }
}
