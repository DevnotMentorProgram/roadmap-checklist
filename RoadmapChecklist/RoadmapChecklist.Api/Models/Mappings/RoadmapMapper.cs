using RoadmapChecklist.Api.Models.Request.Roadmap;
using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Linq;
using System.Security.Claims;

namespace RoadmapChecklist.Api.Models.Mappings
{
    public class RoadmapMapper
    {
        public static Roadmap MapRoadmapCreateModel(Create roadmapCreateModel, Guid userId)
        {
            return new Roadmap
            {
                Name = roadmapCreateModel.Name,
                Visibility = roadmapCreateModel.Visibility,
                StartDate = roadmapCreateModel.StartDate,
                EndDate = roadmapCreateModel.EndDate,
                UserId = userId
            };
        }

        public static Roadmap MapRoadmapUpdateModel(Update roadmapUpdateModel, Guid userId)
        {
            return new Roadmap
            {
                Name = roadmapUpdateModel.Name,
                Visibility = roadmapUpdateModel.Visibility,
                StartDate = roadmapUpdateModel.StartDate,
                EndDate = roadmapUpdateModel.EndDate,
                UserId = userId
            };
        }
    }
}
