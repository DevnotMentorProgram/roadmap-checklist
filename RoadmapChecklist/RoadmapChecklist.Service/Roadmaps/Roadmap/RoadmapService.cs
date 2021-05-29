using Data.Infrastructure.Repository;
using Data.Infratructure.UnitOfWork;
using RoadmapChecklist.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RoadmapChecklist.Service.Roadmap
{
    public class RoadmapService : IRoadmapService
    {
        private readonly IRepository<Entity.Roadmap.Roadmap> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RoadmapService(IRepository<Entity.Roadmap.Roadmap> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public ReturnModel<Entity.Roadmap.Roadmap> Create(Entity.Roadmap.Roadmap roadmap)
        {
            var result = new ReturnModel<Entity.Roadmap.Roadmap>();
            try
            {
                _repository.Add(roadmap);
                Save();

                result.Data = roadmap;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<Entity.Roadmap.Roadmap> GetById(Guid id)
        {
            var result = new ReturnModel<Entity.Roadmap.Roadmap>();

            try
            {
                result.Data = _repository.Get(x => x.Id == id);
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<List<Entity.Roadmap.Roadmap>> GetByUserId(Guid userId)
        {
            var result = new ReturnModel<List<Entity.Roadmap.Roadmap>>();

            try
            {
                result.Data = _repository.GetMany(x => x.UserId == userId);
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<Entity.Roadmap.Roadmap> Update(Entity.Roadmap.Roadmap roadmap)
        {
            var result = new ReturnModel<Entity.Roadmap.Roadmap>();

            try
            {
                _repository.Update(roadmap);
                Save();
                result.Data = roadmap;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public ReturnModel<Entity.Roadmap.Roadmap> IsRoadmapValidForEdit(Entity.Roadmap.Roadmap roadmap)
        {
            var result = new ReturnModel<Entity.Roadmap.Roadmap>();

            try
            {
                result.Data = _repository.Get(x => x.Id == roadmap.Id && x.UserId == roadmap.UserId && x.Status == (int)StatusEnum.ActiveRoadmap);
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }

        public void Delete(Entity.Roadmap.Roadmap roadmap)
        {
            var result = new ReturnModel<Entity.Roadmap.Roadmap>();

            try
            {
                _repository.Delete(roadmap);
                Save();
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }
        }
        private void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
