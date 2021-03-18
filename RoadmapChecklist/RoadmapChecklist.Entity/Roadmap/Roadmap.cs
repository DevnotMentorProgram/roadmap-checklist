using RoadmapChecklist.Entity.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Entity.Roadmap
{
    public class Roadmap : BaseEntity
    {
        public string Name { get; set; }
        public int Visibility { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid UserId { get; set; }

        // Relations
        public virtual User.User User { get; set; }
        public virtual ICollection<RoadmapItem> Items { get; set; }
        public virtual ICollection<RoadmapCategory> Categories { get; set; }
        public virtual ICollection<RoadmapTag> Tags { get; set; }
        public virtual ICollection<RoadmapCopy> Sources { get; set; }
        public virtual ICollection<RoadmapCopy> Targets { get; set; }
    }
}
