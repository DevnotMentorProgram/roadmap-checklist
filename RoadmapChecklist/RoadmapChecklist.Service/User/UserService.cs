using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Infrastructure.Repository;
using Data.Infratructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

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

        public ReturnModel<Entity.User.User> IsUserValidForRegister(string email, string name)
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

        public ReturnModel<Entity.User.User> IsUserValidForLogin(Entity.User.User user)
        {
            var result = new ReturnModel<Entity.User.User>();

            try
            {
                result.Data = repository.Get(u => u.Email == user.Email && u.Password == user.Password);
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public async Task<ReturnModel<string>> AddUserCookieAsync(HttpContext httpContext, string id)
        {
            var result = new ReturnModel<string>();

            try
            {
                var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, id.ToString()) };

                var cIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(cIdentity));
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public async Task<ReturnModel<string>> DeleteUserCookieAsync(HttpContext httpContext)
        {
            var result = new ReturnModel<string>();

            try
            {
                await httpContext.SignOutAsync();
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