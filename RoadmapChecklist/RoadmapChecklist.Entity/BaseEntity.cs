using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Entity
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            var dateTime = DateTime.UtcNow;

            this.CreatedAt = dateTime;
            this.UpdatedAt = dateTime;
            this.Status = 1;
            this.IsDeleted = false;
        }

        public Guid Id { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
