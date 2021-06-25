using Microsoft.AspNetCore.Mvc;
using RoadmapChecklist.Api.Models.Request.Roadmap;
using RoadmapChecklist.Service.Roadmap;
using RoadmapChecklist.Api.Models.Mappings;
using System;
using RoadmapChecklist.Core.Infrastructure;
using System.Security.Claims;

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
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
                return StatusCode(401);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = service.Create(RoadmapMapper.MapRoadmapCreateModel(model, userId));
            return result.IsSuccess ? StatusCode(201) : StatusCode(403);
        }

        [HttpPost("update")]
        public ActionResult Update(Update model)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
                return StatusCode(401);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = service.IsRoadmapValidForEdit(RoadmapMapper.MapRoadmapUpdateModel(model, userId));

            var roadmap = data.IsSuccess ? data.Data : null;

            if (roadmap == null)
                return BadRequest(data.Message);

            roadmap.Name = model.Name;
            roadmap.Visibility = model.Visibility;
            roadmap.StartDate = model.StartDate;
            roadmap.EndDate = model.EndDate;
            roadmap.UpdatedAt = DateTime.Now;

            var result = service.Update(roadmap);
            return result.IsSuccess ? StatusCode(200) : StatusCode(400);

        }

        [HttpPost("delete")]
        public ActionResult Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
                return StatusCode(401);

            var roadmapToBeDeleted = service.GetById(id);

            var roadmap = roadmapToBeDeleted.IsSuccess ? roadmapToBeDeleted.Data : null;

            if (roadmap == null)
                return BadRequest(roadmapToBeDeleted.Message);

            roadmap.Status = (int)StatusEnum.DeletedRoadmap;
            service.Delete(roadmap);

            //ToDo: update database IsDeleted field
            return Ok();
        }

        [HttpPost("copy")]
        public ActionResult Copy(Guid roadmapId, Guid userId)
        {
            if (userId == null)
                return StatusCode(401);

            var result = service.Copy(roadmapId, userId);
            return result.IsSuccess ? StatusCode(200) : StatusCode(404);
        }
    }
}
