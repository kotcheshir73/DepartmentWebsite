using DatabaseContext;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using LearningProgressInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace LearningProgressImplementations.Implementations
{
    public class DisciplineLessonTaskVariantService : IDisciplineLessonTaskVariantService
    {
        private readonly IDisciplineLessonTaskService _serviceDLT;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

        private readonly string _entity = "Дисциплины";

        public DisciplineLessonTaskVariantService(IDisciplineLessonTaskService serviceDLT)
        {
            _serviceDLT = serviceDLT;
        }

        public ResultService<DisciplineLessonTaskPageViewModel> GetDisciplineLessonTasks(DisciplineLessonTaskGetBindingModel model)
        {
            return _serviceDLT.GetDisciplineLessonTasks(model);
        }

        public ResultService<DisciplineLessonTaskVariantPageViewModel> GetDisciplineLessonTaskVariants(DisciplineLessonTaskVariantGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineLessonTaskVariants.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.DisciplineLessonTaskId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineLessonTaskId == model.DisciplineLessonTaskId);
                    }

                    query = query.OrderBy(x => x.Order);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.DisciplineLessonTask);

                    var result = new DisciplineLessonTaskVariantPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonTaskVariantViewModel).ToList()
                    };

                    return ResultService<DisciplineLessonTaskVariantPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonTaskVariantPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineLessonTaskVariantViewModel> GetDisciplineLessonTaskVariant(DisciplineLessonTaskVariantGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonTaskVariants
                                .Include(x => x.DisciplineLessonTask)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineLessonTaskVariantViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineLessonTaskVariantViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineLessonTaskVariantViewModel>.Success(LearningProgressModelFactoryToViewModel.CreateDisciplineLessonTaskVariantViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineLessonTaskVariantViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineLessonTaskVariant(DisciplineLessonTaskVariantRecordBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLessonTaskVariant(model);

                    var exsistEntity = context.DisciplineLessonTaskVariants.FirstOrDefault(x => x.DisciplineLessonTaskId == entity.DisciplineLessonTaskId && 
                                                x.VariantNumber == model.VariantNumber);
                    if (exsistEntity == null)
                    {
                        context.DisciplineLessonTaskVariants.Add(entity);
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

        public ResultService UpdateDisciplineLessonTaskVariant(DisciplineLessonTaskVariantRecordBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonTaskVariants.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LearningProgressModelFacotryFromBindingModel.CreateDisciplineLessonTaskVariant(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteDisciplineLessonTaskVariant(DisciplineLessonTaskVariantGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineLessonTaskVariants.FirstOrDefault(x => x.Id == model.Id);
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