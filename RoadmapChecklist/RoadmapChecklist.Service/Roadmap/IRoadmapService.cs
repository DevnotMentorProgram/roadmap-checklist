using System;
using System.Collections.Generic;
using System.Text;

namespace RoadmapChecklist.Service.Roadmap
{
    public interface IRoadmapService
    {
        ReturnModel<Entity.Roadmap.Roadmap> Create(Entity.Roadmap.Roadmap roadmap);
        ReturnModel<Entity.Roadmap.Roadmap> Update(Entity.Roadmap.Roadmap roadmap);
        ReturnModel<Entity.Roadmap.Roadmap> GetById(Guid id);
        ReturnModel<List<Entity.Roadmap.Roadmap>> GetByUserId(Guid userId);
    }
}
