using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class DisciplineTimeDistributionClassroomService : IDisciplineTimeDistributionClassroomService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Расчасовки;

        private readonly string _entity = "Расчасовки";

        public ResultService<DisciplineTimeDistributionClassroomPageViewModel> GetDisciplineTimeDistributionClassrooms(DisciplineTimeDistributionClassroomGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineTimeDistributionClassrooms.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.DisciplineTimeDistributionId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineTimeDistributionId == model.DisciplineTimeDistributionId);
                    }
                    if (model.TimeNormId.HasValue)
                    {
                        query = query.Where(x => x.TimeNormId == model.TimeNormId);
                    }
                    query = query.Include(x => x.DisciplineTimeDistribution).Include(x => x.TimeNorm);
                    query = query.OrderBy(x => x.DisciplineTimeDistributionId).ThenBy(x => x.TimeNormId);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new DisciplineTimeDistributionClassroomPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateDisciplineTimeDistributionClassroomViewModel).ToList()
                    };

                    return ResultService<DisciplineTimeDistributionClassroomPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineTimeDistributionClassroomPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineTimeDistributionClassroomViewModel> GetDisciplineTimeDistributionClassroom(DisciplineTimeDistributionClassroomGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributionClassrooms
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineTimeDistributionClassroomViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineTimeDistributionClassroomViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineTimeDistributionClassroomViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateDisciplineTimeDistributionClassroomViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineTimeDistributionClassroomViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineTimeDistributionClassroom(DisciplineTimeDistributionClassroomSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistributionClassroom(model);

                    var exsistEntity = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.DisciplineTimeDistributionId == entity.DisciplineTimeDistributionId && 
                            x.TimeNormId == entity.TimeNormId && x.ClassroomDescription == entity.ClassroomDescription);
                    if (exsistEntity == null)
                    {
                        context.DisciplineTimeDistributionClassrooms.Add(entity);
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

        public ResultService UpdateDisciplineTimeDistributionClassroom(DisciplineTimeDistributionClassroomSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistributionClassroom(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteDisciplineTimeDistributionClassroom(DisciplineTimeDistributionClassroomGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.Id == model.Id);
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