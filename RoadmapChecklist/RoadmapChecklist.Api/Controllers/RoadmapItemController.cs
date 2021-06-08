using Microsoft.AspNetCore.Mvc;
using RoadmapChecklist.Api.Models.Mappings;
using RoadmapChecklist.Api.Models.Request.Roadmap.RoadmapItems;
using RoadmapChecklist.Service.Roadmaps.RoadmapItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadmapChecklist.Api.Controllers
{
    public class RoadmapItemController : ControllerBase
    {
        private readonly IRoadmapItemService roadmapItemService;

        public RoadmapItemController(IRoadmapItemService roadmapItemService)
        {
            this.roadmapItemService = roadmapItemService;
        }

        [HttpPost("createRoadmapItem")]
        public IActionResult Create([FromBody]CreateRoadmapItem createItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = roadmapItemService.Create(RoadmapItemMapper.MapCreateRoadmapItemModel(createItem));
            return result.IsSuccess ? StatusCode(201) : StatusCode(403);
        }

        [HttpPost("updateRoadmapItem")]
        public IActionResult Update([FromBody] CreateRoadmapItem updateItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = roadmapItemService.Create(RoadmapItemMapper.MapUpdateRoadmapItemModel(updateItem));
            return result.IsSuccess ? StatusCode(201) : StatusCode(403);
        }
    }
}
