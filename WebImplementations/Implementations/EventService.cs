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
    public class EventService : IWebEventService
    {
        //private readonly AccessOperation _serviceOperation = AccessOperation.Студенты;

        //private readonly string _entity = "Студенты";

        private readonly ICommentService _serviceC;

        public EventService(ICommentService serviceC)
        {
            _serviceC = serviceC;
        }


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

                    query = query.OrderByDescending(x => x.DateCreate);
                    var result = new EventPageViewModel();
                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);

                        result = new EventPageViewModel
                        {
                            MaxCount = countPages,
                            CurrentPage = model.PageNumber.Value,
                            List = query.Select(WebModelFactoryToViewModel.CreateEventViewModel).ToList()
                        };
                    }
                    else
                    {
                        result = new EventPageViewModel
                        {
                            List = query.Select(WebModelFactoryToViewModel.CreateEventViewModel).ToList()
                        };
                    }

                    

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

                    return ResultService<EventViewModel>.Success(WebModelFactoryToViewModel.CreateEventViewModel(entity));
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
                    var entity = WebModelFacotryFromBindingModel.CreateEvent(model);
                                        
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

        public ResultService UpdateEvent(EventUpdateBindingModel model)
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
                    entity = WebModelFacotryFromBindingModel.UpdateEvent(model, entity);

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

                    foreach (var comment in _serviceC.GetComments(new CommentGetBindingModel
                    {
                        EventId = entity.Id
                    }).Result.List)
                    {
                        _serviceC.DeleteComment(new CommentGetBindingModel
                        {
                            Id = comment.Id
                        });
                    }

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
