using RoadmapChecklist.Api.Models.Request.Roadmap.RoadmapItems;
using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadmapChecklist.Api.Models.Mappings
{
    public class RoadmapItemMapper
    {
        public static RoadmapItem MapCreateRoadmapItemModel(CreateRoadmapItem createRoadmapItem)
        {
            return new RoadmapItem
            {
                Title = createRoadmapItem.Title,
                Description = createRoadmapItem.Description,
                Order = createRoadmapItem.Order ?? 0,

            };
        }

        public static RoadmapItem MapUpdateRoadmapItemModel(CreateRoadmapItem createRoadmapItem)
        {
            return new RoadmapItem
            {
                Title = createRoadmapItem.Title,
                Description = createRoadmapItem.Description,
                Order = createRoadmapItem.Order ?? 0,

            };
        }
    }
}
