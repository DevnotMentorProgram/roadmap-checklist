using FluentValidation;
using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadmapChecklist.Api.Models.Request.Roadmap.RoadmapItems
{
    public class CreateRoadmapItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }
        public int Status { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid RoadmapId { get; set; }
        public Guid ParentId { get; set; }

        public class RoadmapItemValidator : AbstractValidator<RoadmapItem>
        {
            public RoadmapItemValidator()
            {
                RuleFor(x => x.Title).Length(0, max: 500);
                RuleFor(x => x.Description).Length(0, max: 1000);
                RuleFor(x => x.Status).NotNull();
                RuleFor(x => x.TargetDate).Null();
                RuleFor(x => x.EndDate).NotNull();
                RuleFor(x => x.RoadmapId).NotNull();
                RuleFor(x => x.ParentId).NotNull();
            }
        }
    }
}
