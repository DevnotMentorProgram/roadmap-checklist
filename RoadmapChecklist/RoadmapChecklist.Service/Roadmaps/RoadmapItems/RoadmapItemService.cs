using Data.Infrastructure.Repository;
using Data.Infratructure.UnitOfWork;
using RoadmapChecklist.Core;
using RoadmapChecklist.Entity.Roadmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadmapChecklist.Service.Roadmaps.RoadmapItems
{
    public class RoadmapItemService : IRoadmapItemService
    {
        private readonly IRepository<RoadmapItem> repository;
        private readonly IUnitOfWork unitOfWork;

        public RoadmapItemService(IRepository<RoadmapItem> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public void Save()
        {
            unitOfWork.Commit();
        }

        #region Create
        public ReturnModel<RoadmapItem> Create(RoadmapItem roadmapItem)
        {
            var result = new ReturnModel<RoadmapItem>();

            try
            {
                var item = new RoadmapItem()
                {
                    Title = roadmapItem.Title,
                    RoadmapId = roadmapItem.RoadmapId,
                    ParentId = roadmapItem.ParentId
                };

                if (roadmapItem.Order > 0)
                {
                    item.Order = roadmapItem.Order;

                    var afterItems = repository.AsIQueryable(x => x.RoadmapId == roadmapItem.RoadmapId
                  && x.ParentId == roadmapItem.ParentId
                  && x.Status != (int)StatusEnum.DeletedRoadmapItem
                  && (x.Order == item.Order || x.Order > item.Order))
                  .OrderBy(x => x.Order)
                  .ToList();

                    if (afterItems != null && afterItems.Count > 0)
                    {
                        foreach (var after in afterItems)
                        {
                            after.Order += 1;
                            repository.Update(after);
                        }
                    }
                }

                // En son sıraya ekler
                else
                {
                    var parent = repository.AsIQueryable(x => x.RoadmapId == roadmapItem.RoadmapId
                    && x.Status != (int)StatusEnum.DeletedRoadmapItem
                    && x.ParentId == roadmapItem.ParentId)
                    .OrderByDescending(x => x.Order)
                    .FirstOrDefault();

                    if (parent != null) item.Order = parent.Order + 1;
                }

                repository.Add(item);
                Save();

                result.Data = item;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }
        #endregion

        #region Delete
        public ReturnModel<bool> Delete(Guid roadmapId, Guid itemId)
        {
            var result = new ReturnModel<bool>();

            try
            {
                var toDelete = repository.AsIQueryable(x => x.RoadmapId == roadmapId && x.Id == itemId && x.Status != (int)StatusEnum.DeletedRoadmapItem).FirstOrDefault();

                if (toDelete != null)
                {
                    // kaydı silinecek olarak işaretle.
                    toDelete.Status = (int)StatusEnum.DeletedRoadmapItem;
                    repository.Update(toDelete);

                    // silinecek kayda ait alt kayıtlar varsa onları da sil.


                    var childRoadmapItems = repository.AsIQueryable(x => x.RoadmapId == roadmapId && x.Status != (int)StatusEnum.DeletedRoadmapItem && x.ParentId == toDelete.Id).ToList();

                    if (childRoadmapItems != null && childRoadmapItems.Count > 0)
                    {
                        foreach (var child in childRoadmapItems)
                            child.Status = (int)StatusEnum.DeletedRoadmapItem;

                        repository.UpdateRange(childRoadmapItems);
                    }

                    // Kendinden sonraki kayıtların sırasını azalt.
                    var afterItems = repository.AsIQueryable(
                       x => x.RoadmapId == roadmapId
                       && x.ParentId == toDelete.ParentId
                       && x.Status != (int)StatusEnum.DeletedRoadmapItem
                       && x.Id != toDelete.Id
                       && x.Order > toDelete.Order)
                       .OrderBy(x => x.Order)
                       .ToList();

                    if (afterItems != null && afterItems.Count > 0)
                    {
                        foreach (var after in afterItems) { after.Order--; }
                        repository.UpdateRange(afterItems);
                    }

                    // db'yi güncel}-le

                    Save();
                }
            }

            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }
        #endregion

        #region Get
        public ReturnModel<RoadmapItem> Get(Guid roadmapItemId)
        {
            var result = new ReturnModel<Entity.Roadmap.RoadmapItem>();

            try
            {
                result.Data = repository.Get(roadmap => roadmap.Id == roadmapItemId);
                Save();

            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }
        #endregion

        #region GetAllByUser
        public ReturnModel<IEnumerable<RoadmapItem>> GetAllByUser(Guid userId)
        {
            var result = new ReturnModel<IEnumerable<RoadmapItem>>();

            try
            {
                var roadMapItems = repository.GetMany(roadmap => roadmap.Id == userId);
                result.Data = roadMapItems;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.Exception = exception;
                result.Message = exception.Message;
            }

            return result;
        }
        #endregion

        #region Update
        public ReturnModel<RoadmapItem> UpdateItem(RoadmapItem roadmapItem)
        {
            var result = new ReturnModel<RoadmapItem>();

            try
            {
                var updateRoadmapItem = new RoadmapItem()
                {
                    Title = roadmapItem.Title,
                    Description = roadmapItem.Description,
                    TargetDate = roadmapItem.TargetDate,
                    EndDate = roadmapItem.EndDate,
                    RoadmapId = roadmapItem.RoadmapId,
                    Status = 1,
                    ParentId = roadmapItem.ParentId
                };

                repository.Add(updateRoadmapItem);
                Save();

                result.Data = updateRoadmapItem;
            }
            catch (Exception ex)
            {

                result.IsSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }
        #endregion

    }
}
