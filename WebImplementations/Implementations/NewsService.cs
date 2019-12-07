using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class NewsService : INewsService
    {
        public ResultService<NewsPageViewModel> GetNewses(NewsGetBindingModel model)
        {
            try
            {
                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Newses.Where(x => !x.IsDeleted).AsQueryable();

                    if (!string.IsNullOrEmpty(model.Tag))
                    {
                        var tags = model.Tag.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        query = query.Where(x => tags.Any(y => y.Contains(model.Tag)));
                    }

                    if(model.DepartmentUserId.HasValue)
                    {
                        query = query.Where(x => x.DepartmentUserId == model.DepartmentUserId);
                    }

                    query = query.OrderByDescending(x => x.DateCreate);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.DepartmentUser);

                    var result = new NewsPageViewModel
                    {
                        MaxCount = countPages,
                        CurrentPage = model.PageNumber ?? -1,
                        List = query.Select(WebModelFactoryToViewModel.CreateNewsViewModel).ToList()
                    };

                    return ResultService<NewsPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<NewsPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<NewsViewModel> GetNews(NewsGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Newses
                        .Include(x => x.DepartmentUser)
                        .FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService<NewsViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<NewsViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<NewsViewModel>.Success(WebModelFactoryToViewModel.CreateNewsViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<NewsViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateNews(NewsSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = WebModelFacotryFromBindingModel.CreateNews(model);
                                        
                    context.Newses.Add(entity);
                    context.SaveChanges();
                    return ResultService.Success(entity.Id);
                    
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateNews(NewsSetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Newses.FirstOrDefault(x => x.Id == model.Id);

                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = WebModelFacotryFromBindingModel.CreateNews(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteNews(NewsGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                using (var transaction = context.Database.BeginTransaction())
                {
                    var entity = context.Newses.Include(x => x.Comments).FirstOrDefault(x => x.Id == model.Id);

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

                    if(entity.Comments != null)
                    {
                        foreach(var comment in entity.Comments)
                        {
                            comment.IsDeleted = true;
                            comment.DateDelete = DateTime.Now;
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