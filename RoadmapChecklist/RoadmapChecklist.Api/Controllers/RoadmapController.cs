using Microsoft.AspNetCore.Mvc;
using RoadmapChecklist.Api.Models.Request.Roadmap;
using RoadmapChecklist.Service.Roadmap;
using RoadmapChecklist.Api.Models.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoadmapChecklist.Core.Infrastructure;

namespace RoadmapChecklist.Api.Controllers
{
    [Route("api/roadmap")]
    [ApiController]
    public class RoadmapController : ControllerBase
    {
        private readonly IRoadmapService service;

        public RoadmapController(IRoadmapService roadmapService)
        {
            this.service = roadmapService;
        }

        [HttpPost("create")]
        public ActionResult Create(Create model)
        {
            var result = service.Create(RoadmapMapper.MapRoadmapCreateModel(model));

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("edit")]
        public ActionResult Edit(Edit model)
        {
            var data = service.IsRoadmapValidForEdit(RoadmapMapper.MapRoadmapEditModel(model, User));

            var roadmap = data.IsSuccess ? data.Data : null;

            if (roadmap == null)
                return BadRequest(data.Message);

            roadmap.Name = model.Name;
            roadmap.Visibility = model.Visibility;
            roadmap.StartDate = model.StartDate;
            roadmap.EndDate = model.EndDate;

            var result = service.Update(roadmap);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public ActionResult Delete(Guid id)
        {
            var roadmapToBeDeleted = service.GetById(id);

            var roadmap = roadmapToBeDeleted.IsSuccess ? roadmapToBeDeleted.Data : null;

            if (roadmap == null)
                return BadRequest(roadmapToBeDeleted.Message);

            roadmap.Status = (int)StatusEnum.DeletedRoadmap;
            service.Delete(roadmap);

            return Ok();
        }
    }
}
