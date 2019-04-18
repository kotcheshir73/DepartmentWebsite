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
    public class DisciplineTimeDistributionRecordService : IDisciplineTimeDistributionRecordService
    {
        private readonly IDisciplineTimeDistributionService _serviceDTD;

        private readonly ITimeNormService _serviceTN;

        private readonly AccessOperation _serviceOperation = AccessOperation.Расчасовки;

        private readonly string _entity = "Расчасовки";

        public DisciplineTimeDistributionRecordService(IDisciplineTimeDistributionService serviceDTD, ITimeNormService serviceTN)
        {
            _serviceDTD = serviceDTD;
            _serviceTN = serviceTN;
        }

        public ResultService<DisciplineTimeDistributionPageViewModel> GetDisciplineTimeDistributions(DisciplineTimeDistributionGetBindingModel model)
        {
            return _serviceDTD.GetDisciplineTimeDistributions(model);
        }

        public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
        {
            return _serviceTN.GetTimeNorms(model);
        }

        public ResultService<DisciplineTimeDistributionRecordPageViewModel> GetDisciplineTimeDistributionRecords(DisciplineTimeDistributionRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DisciplineTimeDistributionRecords.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.DisciplineTimeDistributionId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineTimeDistributionId == model.DisciplineTimeDistributionId);
                    }
                    if (model.TimeNormId.HasValue)
                    {
                        query = query.Where(x => x.TimeNormId == model.TimeNormId);
                    }

                   // query = query.OrderBy(x => x.TimeNorm.TimeNormName).ThenBy(x => x.WeekNumber);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query
                                .Include(x => x.DisciplineTimeDistribution.StudentGroup)
                                .Include(x => x.DisciplineTimeDistribution.AcademicPlanRecord)
                                .Include(x => x.DisciplineTimeDistribution.AcademicPlanRecord.Discipline)
                                .Include(x => x.TimeNorm);

                    var result = new DisciplineTimeDistributionRecordPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateDisciplineTimeDistributionRecordViewModel).ToList()
                    };

                    return ResultService<DisciplineTimeDistributionRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineTimeDistributionRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineTimeDistributionRecordViewModel> GetDisciplineTimeDistributionRecord(DisciplineTimeDistributionRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributionRecords
                                .Include(x => x.DisciplineTimeDistribution.StudentGroup)
                                .Include(x => x.DisciplineTimeDistribution.AcademicPlanRecord)
                                .Include(x => x.DisciplineTimeDistribution.AcademicPlanRecord.Discipline)
                                .Include(x => x.TimeNorm)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineTimeDistributionRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineTimeDistributionRecordViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineTimeDistributionRecordViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateDisciplineTimeDistributionRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineTimeDistributionRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineTimeDistributionRecord(DisciplineTimeDistributionRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistributionRecord(model);

                    var exsistEntity = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.DisciplineTimeDistributionId == entity.DisciplineTimeDistributionId &&
                            x.TimeNormId == entity.TimeNormId && x.WeekNumber == entity.WeekNumber);
                    if (exsistEntity == null)
                    {
                        context.DisciplineTimeDistributionRecords.Add(entity);
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

        public ResultService UpdateDisciplineTimeDistributionRecord(DisciplineTimeDistributionRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistributionRecord(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteDisciplineTimeDistributionRecord(DisciplineTimeDistributionRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributionRecords.FirstOrDefault(x => x.Id == model.Id);
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