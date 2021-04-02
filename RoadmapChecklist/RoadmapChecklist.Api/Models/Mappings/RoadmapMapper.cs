using RoadmapChecklist.Api.Models.Request.Roadmap;
using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Linq;
using System.Security.Claims;

namespace RoadmapChecklist.Api.Models.Mappings
{
    public class RoadmapMapper
    {
        public static Roadmap MapRoadmapCreateModel(Create roadmapCreateModel)
        {
            return new Roadmap
            {
                Name = roadmapCreateModel.Name,
                Visibility = roadmapCreateModel.Visibility,
                StartDate = roadmapCreateModel.StartDate,
                EndDate = roadmapCreateModel.EndDate
            };
        }

        public static Roadmap MapRoadmapEditModel(Edit roadmapEditModel, ClaimsPrincipal user = null)
        {
            return new Roadmap
            {
                Name = roadmapEditModel.Name,
                Visibility = roadmapEditModel.Visibility,
                StartDate = roadmapEditModel.StartDate,
                EndDate = roadmapEditModel.EndDate,
                UserId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier))
            };
        }
    }
}
