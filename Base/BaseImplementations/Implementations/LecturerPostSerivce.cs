using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using System;
using System.Linq;
using Tools;

namespace BaseImplementations.Services
{
    public class LecturerPostSerivce : ILecturerPostSerivce
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Должности_преподавателей;

        private readonly string _entity = "Должности преподавателей";

        public ResultService<LecturerPostPageViewModel> GetLecturerPosts(LecturerPostGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.LecturerPosts.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.PostTitle);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new LecturerPostPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateLecturerPostViewModel).ToList()
                    };

                    return ResultService<LecturerPostPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<LecturerPostPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<LecturerPostViewModel> GetLecturerPost(LecturerPostGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerPosts
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<LecturerPostViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<LecturerPostViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    return ResultService<LecturerPostViewModel>.Success(ModelFactoryToViewModel.CreateLecturerPostViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<LecturerPostViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateLecturerPost(LecturerPostSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateLecturerPost(model);

                    var exsistEntity = context.LecturerPosts.FirstOrDefault(x => x.PostTitle == entity.PostTitle);
                    if (exsistEntity == null)
                    {
                        context.LecturerPosts.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if (exsistEntity.IsDeleted)
                        {
                            exsistEntity.IsDeleted = false;
                            context.SaveChanges();
                            return ResultService.Success(exsistEntity.Id);
                        }
                        else
                        {
                            return ResultService.Error("Error:", "Элемент уже существует", ResultServiceStatusCode.ExsistItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateLecturerPost(LecturerPostSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerPosts.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateLecturerPost(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteLecturerPost(LecturerPostGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerPosts.FirstOrDefault(x => x.Id == model.Id);
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