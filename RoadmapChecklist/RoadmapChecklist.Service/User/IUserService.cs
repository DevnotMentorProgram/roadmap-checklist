using Microsoft.AspNetCore.Http;
using System;

namespace RoadmapChecklist.Service.User
{
    public interface IUserService
    {
        ReturnModel<Entity.User.User> Create(Entity.User.User user);
        ReturnModel<Entity.User.User> GetById(Guid userId);
        ReturnModel<Entity.User.User> IsUserValidForRegister(string email, string userName);
        ReturnModel<Entity.User.User> IsUserValidForLogin(Entity.User.User user);
        System.Threading.Tasks.Task<ReturnModel<string>> AddUserCookieAsync(HttpContext httpContext, string id);
        System.Threading.Tasks.Task<ReturnModel<string>> DeleteUserCookieAsync(HttpContext httpContext);
    }
}