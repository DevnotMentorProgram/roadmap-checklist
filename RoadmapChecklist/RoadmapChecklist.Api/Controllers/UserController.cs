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
            var isUserValidForRegister = service.IsUserValidForRegister(userRegisterModel.Email.ToLower(), userRegisterModel.UserName);

            var user = isUserValidForRegister.IsSuccess ? isUserValidForRegister.Data : null;
            
            if (isUserValidForRegister != null)
            {
                if (user.Email == userRegisterModel.Email.ToLower())
                {
                    ModelState.AddModelError("Email" , "Email already exists!");
                } 
                else if (user.Name == userRegisterModel.UserName)
                {
                    ModelState.AddModelError("UserName" , "UserName already exists!");
                }
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = service.Create(UserMapper.MapUserRegisterModel(userRegisterModel));
            return result.IsSuccess ? StatusCode(201) : StatusCode(403);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login userLoginModel)
        {
            var isUserValidForLogin = service.IsUserValidForLogin(UserMapper.MapUserLoginModel(userLoginModel));

            var user = isUserValidForLogin.IsSuccess ? isUserValidForLogin.Data : null;

            if (user == null)
            {
                return StatusCode(401);
            }

            var result = service.AddUserCookieAsync(HttpContext, user.Id.ToString()).Result;
            return result.IsSuccess ? StatusCode(200) : StatusCode(401);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var result = service.DeleteUserCookieAsync(HttpContext).Result;
            return result.IsSuccess ? StatusCode(200) : StatusCode(403);
        }
    }
}