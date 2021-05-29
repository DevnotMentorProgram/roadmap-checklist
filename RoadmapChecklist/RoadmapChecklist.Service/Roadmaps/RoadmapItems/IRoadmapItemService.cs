using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Service.Roadmaps.RoadmapItems
{
    public interface IRoadmapItemService
    {
        ReturnModel<IEnumerable<RoadmapItem>> GetAllByUser(Guid userId);
        ReturnModel<RoadmapItem> Get(Guid roadmapItemId);
        ReturnModel<bool> Delete(Guid roadmapId,Guid itemId);
        ReturnModel<RoadmapItem> Create(RoadmapItem roadmapItem);
        ReturnModel<RoadmapItem> UpdateItem(RoadmapItem roadmapItem);
    }
}
