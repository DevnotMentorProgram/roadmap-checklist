using System;
using Data.Infrastructure.Repository;
using Data.Infratructure.UnitOfWork;

namespace RoadmapChecklist.Service.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Entity.User.User> repository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IRepository<Entity.User.User> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        
        public void Save()
        {
            unitOfWork.Commit();
        }
        
        public ReturnModel<Entity.User.User> Create(Entity.User.User user)
        {
            var result = new ReturnModel<Entity.User.User>();
            
            try
            {                
                repository.Add(user);
                Save();

                result.Data = user;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<Entity.User.User> GetById(Guid userId)
        {
            var result = new ReturnModel<Entity.User.User>();

            try
            {
                result.Data = repository.Get(user => user.Id == userId);
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<Entity.User.User> IsUserExist(string email, string name)
        {
            var result = new ReturnModel<Entity.User.User>();
            
            try
            {
                var userEntity = repository.Get(user => user.Name == name || user.Email == email);
                result.Data = userEntity;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }
    }
}