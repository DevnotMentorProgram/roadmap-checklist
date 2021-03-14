using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Entity.Relations
{
    public class RoadmapCopy : BaseEntity
    {
        public int SourceId { get; set; }
        public int TargetId { get; set; }

        // Relations
        public virtual Roadmap.Roadmap SourceRoudmap { get; set; }
        public virtual Roadmap.Roadmap TargetRoadmap { get; set; }
    }
}
