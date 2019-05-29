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
    public class CommentService : ICommentService
    {
        //private readonly AccessOperation _serviceOperation = AccessOperation.Студенты;

        //private readonly string _entity = "Студенты";

        public ResultService<CommentPageViewModel> GetComments(CommentGetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);
                                
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Comments.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.EventId.HasValue)
                    {
                        query = query.Where(x => x.EventId == model.EventId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineId == model.DisciplineId.Value);
                    }
                    if (model.ParentId.HasValue)
                    {
                        query = query.Where(x => x.ParentId == model.ParentId.Value);
                    }

                    query = query.OrderBy(x => x.DateCreate);

                    var result = new CommentPageViewModel
                    {                        
                        List = query.Select(ModelFactoryToViewModel.CreateCommentViewModel).ToList()
                    };

                    return ResultService<CommentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<CommentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<CommentViewModel> GetComment(CommentGetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Comments.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<CommentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<CommentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<CommentViewModel>.Success(ModelFactoryToViewModel.CreateCommentViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<CommentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateComment(CommentSetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateComment(model);
                                        
                    context.Comments.Add(entity);
                    context.SaveChanges();
                    return ResultService.Success(entity.Id);
                    
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateComment(CommentSetBindingModel model)
        {
            try
            {
                //DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Comments.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateComment(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteComment(CommentGetBindingModel model)
        {
            try
            {
                foreach (var com in GetComments(new CommentGetBindingModel
                    {
                        EventId = model.EventId,
                        DisciplineId = model.DisciplineId,
                        ParentId = model.Id
                    }).Result.List)
                {
                    DeleteComment(new CommentGetBindingModel
                    {
                        Id = com.Id
                    });
                }

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Comments.FirstOrDefault(x => x.Id == model.Id);
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
