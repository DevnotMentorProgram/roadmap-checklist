using Data.Infrastructure.Repository;
using Data.Infratructure.UnitOfWork;
using RoadmapChecklist.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RoadmapChecklist.Service.Roadmap
{
    public class RoadmapService : IRoadmapService
    {
        private readonly IRepository<Entity.Roadmap.Roadmap> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Entity.Relations.RoadmapCopy> _copyRepository;
        private readonly IRepository<Entity.Roadmap.RoadmapItem> _itemRepository;

        public RoadmapService(IRepository<Entity.Roadmap.Roadmap> repository, IRepository<Entity.Relations.RoadmapCopy> copyRepository, IUnitOfWork unitOfWork, IRepository<Entity.Roadmap.RoadmapItem> itemRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _itemRepository = itemRepository;
            _copyRepository = copyRepository;
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

        public ReturnModel<Entity.Roadmap.Roadmap> Copy(Guid id, Guid userid)
        {
            var result = new ReturnModel<Entity.Roadmap.Roadmap>();

            try
            {
                var sourceRoadmap = _repository.Get(x => x.Id == id, navigations: new string[] { "Targets", "Categories", "Tags", "Items" });
                if (sourceRoadmap != null)
                {
                    Entity.Roadmap.Roadmap targetRoadmap = new Entity.Roadmap.Roadmap()
                    {
                        Name = sourceRoadmap.Name,
                        UserId = userid,
                        Visibility = sourceRoadmap.Visibility,
                    };
                    _repository.Attach(targetRoadmap);

                    #region Categories
                    // TODO: Add Category
                    #endregion

                    #region Tags
                    // TODO: Add Tags
                    #endregion

                    #region RoadmapItems
                    targetRoadmap.Items = new List<Entity.Roadmap.RoadmapItem>();
                    var children = sourceRoadmap.Items.OrderBy(x => x.ParentId).ThenBy(x => x.Order).ToList();
                    foreach (var child in children.Where(x => x.ParentId == null))
                    {
                        var newChild = new Entity.Roadmap.RoadmapItem
                        {
                            Description = child.Description,
                            Order = child.Order,
                            Title = child.Title,
                            Childiren = new List<Entity.Roadmap.RoadmapItem>()
                        };
                        _itemRepository.Attach(newChild);
                        GetChilderen(ref targetRoadmap, child, children, ref newChild);
                        targetRoadmap.Items.Add(newChild);
                    }
                    #endregion

                    var rc = new Entity.Relations.RoadmapCopy { SourceId = sourceRoadmap.Id };
                    _copyRepository.Attach(rc);
                    rc.TargetRoadmap = targetRoadmap;
                    Save();

                    result.Data = targetRoadmap;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
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
        private void GetChilderen(ref Entity.Roadmap.Roadmap targetRoadmap, Entity.Roadmap.RoadmapItem item, List<Entity.Roadmap.RoadmapItem> items, ref Entity.Roadmap.RoadmapItem parent)
        {
            foreach (var child in items.Where(x => x.ParentId == item.Id).OrderBy(x => x.ParentId).ThenBy(x => x.Order))
            {
                var newChild = new Entity.Roadmap.RoadmapItem
                {
                    Title = child.Title,
                    Description = child.Description,
                    Order = child.Order,
                    Childiren = new List<Entity.Roadmap.RoadmapItem>()
                };
                _itemRepository.Attach(newChild);
                GetChilderen(ref targetRoadmap, child, items, ref newChild);
                parent.Childiren.Add(newChild);
                targetRoadmap.Items.Add(newChild);
            }
        }

    }
}
