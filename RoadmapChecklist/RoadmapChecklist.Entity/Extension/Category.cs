using RoadmapChecklist.Entity.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Entity.Extension
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string FriendlyUrl { get; set; }

        // Relations
        public virtual ICollection<RoadmapCategory> RoadmapCategories { get; set; }
    }
}
