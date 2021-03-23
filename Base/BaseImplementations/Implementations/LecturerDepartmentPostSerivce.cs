using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using System;
using System.Linq;
using Tools;

namespace BaseImplementations.Implementations
{
	public class LecturerDepartmentPostSerivce : ILecturerDepartmentPostSerivce
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Должности_преподавателей;

        private readonly string _entity = "Должности преподавателей";

        public ResultService<LecturerDepartmentPostPageViewModel> GetLecturerDepartmentPosts(LecturerDepartmentPostGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.LecturerDepartmentPosts.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.Order);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new LecturerDepartmentPostPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ModelFactoryToViewModel.CreateLecturerDepartmentPostViewModel).ToList()
                    };

                    return ResultService<LecturerDepartmentPostPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<LecturerDepartmentPostPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<LecturerDepartmentPostViewModel> GetLecturerDepartmentPost(LecturerDepartmentPostGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerDepartmentPosts
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<LecturerDepartmentPostViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<LecturerDepartmentPostViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    return ResultService<LecturerDepartmentPostViewModel>.Success(ModelFactoryToViewModel.CreateLecturerDepartmentPostViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<LecturerDepartmentPostViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateLecturerDepartmentPost(LecturerDepartmentPostSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ModelFacotryFromBindingModel.CreateLecturerDepartmentPost(model);

                    var exsistEntity = context.LecturerDepartmentPosts.FirstOrDefault(x => x.DepartmentPostTitle == entity.DepartmentPostTitle);
                    if (exsistEntity == null)
                    {
                        context.LecturerDepartmentPosts.Add(entity);
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

        public ResultService UpdateLecturerDepartmentPost(LecturerDepartmentPostSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerDepartmentPosts.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = ModelFacotryFromBindingModel.CreateLecturerDepartmentPost(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteLecturerDepartmentPost(LecturerDepartmentPostGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.LecturerDepartmentPosts.FirstOrDefault(x => x.Id == model.Id);
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