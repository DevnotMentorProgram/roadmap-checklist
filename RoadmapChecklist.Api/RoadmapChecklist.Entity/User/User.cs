using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Entity.User
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }

        // Relations
        public virtual ICollection<Roadmap.Roadmap> Roadmaps { get; set; }
    }
}
