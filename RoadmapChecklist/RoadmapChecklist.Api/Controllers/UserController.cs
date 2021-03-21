using Microsoft.AspNetCore.Mvc;
using RoadmapChecklist.Api.Models.Mappings;
using RoadmapChecklist.Api.Models.Request.User;
using RoadmapChecklist.Service.User;

namespace RoadmapChecklist.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService userService)
        {
            this.service = userService;
        }
        
        [HttpPost("register")]
        public IActionResult Register([FromBody]Register userRegisterModel)
        {
            var isUserExist = service.IsUserExist(userRegisterModel.Email.ToLower(), userRegisterModel.UserName).Data;
            if (isUserExist != null)
            {
                if (isUserExist.Email == userRegisterModel.Email.ToLower())
                {
                    ModelState.AddModelError("Email" , "Email already exists!");
                } 
                else if (isUserExist.Name == userRegisterModel.UserName)
                {
                    ModelState.AddModelError("UserName" , "UserName already exists!");
                }
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = service.Create(UserMapper.MapUserRegisterModel(userRegisterModel));
            return result.IsSuccess ? StatusCode(201) : BadRequest();
        }
        
    }
}