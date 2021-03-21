using System;

namespace RoadmapChecklist.Service.User
{
    public interface IUserService
    {
        ReturnModel<Entity.User.User> Create(Entity.User.User user);
        ReturnModel<Entity.User.User> GetById(Guid userId);
        ReturnModel<Entity.User.User> IsUserExist(string email, string userName);
    }
}