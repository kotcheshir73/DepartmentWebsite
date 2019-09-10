using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class CommentService : ICommentService
    {
        public ResultService<CommentPageViewModel> GetComments(CommentGetBindingModel model)
        {
            try
            {
                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Comments.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.NewsId.HasValue)
                    {
                        query = query.Where(x => x.NewsId == model.NewsId.Value);
                    }

                    if (model.DepartmentUserId.HasValue)
                    {
                        query = query.Where(x => x.DepartmentUserId == model.DepartmentUserId.Value);
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

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.DepartmentUser).Include(x => x.Childs);

                    var result = new CommentPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(WebModelFactoryToViewModel.CreateCommentViewModel).ToList()
                    };

                    return ResultService<CommentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<CommentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<CommentViewModel>> GetAnswers(CommentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Comments
                        .Where(x => !x.IsDeleted && x.ParentId == model.ParentId)
                        .Include(x => x.DepartmentUser)
                        .OrderBy(x => x.DateCreate)
                        .Select(WebModelFactoryToViewModel.CreateCommentViewModel)
                        .ToList();

                    return ResultService<List<CommentViewModel>>.Success(query);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<CommentViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<CommentViewModel> GetComment(CommentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Comments
                        .Include(x => x.DepartmentUser)
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<CommentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<CommentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<CommentViewModel>.Success(WebModelFactoryToViewModel.CreateCommentViewModel(entity));
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateComment(model);

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
                    entity = WebModelFacotryFromBindingModel.CreateComment(model, entity);

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
                using (var context = DepartmentUserManager.GetContext)
                using (var transaction = context.Database.BeginTransaction())
                {
                    var entity = context.Comments.Include(x => x.Childs).FirstOrDefault(x => x.Id == model.Id);

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

                    if (entity.Childs != null)
                    {
                        foreach(var child in entity.Childs)
                        {
                            child.IsDeleted = true;
                            child.DateDelete = DateTime.Now;
                            context.SaveChanges();
                        }
                    }

                    transaction.Commit();

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