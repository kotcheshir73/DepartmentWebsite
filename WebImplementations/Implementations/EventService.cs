using WebImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using Enums;

namespace WebImplementations.Implementations
{
    public class EventService : IEventService
    {
        //private readonly AccessOperation _serviceOperation = AccessOperation.Студенты;

        //private readonly string _entity = "Студенты";

        public ResultService<EventPageViewModel> GetEvents(EventGetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Events.Where(x => !x.IsDeleted).AsQueryable();

                    //TODO: разобраться с тегами
                    //if (!string.IsNullOrEmpty(model.Tag))
                    //{
                        //query = query.Where(x => x.Tag.Contains(model.Tag));
                    //}

                    query = query.OrderBy(x => x.DateCreate);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.DepartmentUser);

                    var result = new EventPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateEventViewModel).ToList()
                    };

                    return ResultService<EventPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<EventPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<EventViewModel> GetEvent(EventGetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Events.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<EventViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<EventViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<EventViewModel>.Success(ModelFactoryToViewModel.CreateEventViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<EventViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateEvent(EventSetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateEvent(model);
                                        
                    context.Events.Add(entity);
                    context.SaveChanges();
                    return ResultService.Success(entity.Id);
                    
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateEvent(EventSetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Events.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateEvent(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteEvent(EventGetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Events.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}
