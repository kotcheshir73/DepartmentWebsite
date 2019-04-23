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
    public class AcademicPlanRecordElementService : IAcademicPlanRecordElementService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        private readonly string _entity = "Учебные планы";

        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly ITimeNormService _serviceTN;

        public AcademicPlanRecordElementService(IAcademicPlanRecordService serviceAPR, ITimeNormService serviceTN)
        {
            _serviceAPR = serviceAPR;
            _serviceTN = serviceTN;
        }

        public ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
        {
            return _serviceAPR.GetAcademicPlanRecords(model);
        }

        public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
        {
            return _serviceTN.GetTimeNorms(model);
        }

        public ResultService<AcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(AcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordElements.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.AcademicPlanRecordId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanRecordId == model.AcademicPlanRecordId);
                    }
                    if (model.TimeNormId.HasValue)
                    {
                        query = query.Where(x => x.TimeNormId == model.TimeNormId);
                    }

                    query = query.OrderBy(x => x.AcademicPlanRecordId).ThenBy(x => x.TimeNormId);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicPlanRecord).Include(x => x.AcademicPlanRecord.Discipline).Include(x => x.TimeNorm);

                    var result = new AcademicPlanRecordElementPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateAcademicPlanRecordElementViewModel).ToList()
                    };

                    return ResultService<AcademicPlanRecordElementPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordElementPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<AcademicPlanRecordElementViewModel> GetAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordElements
                        .Include(x => x.AcademicPlanRecord).Include(x => x.AcademicPlanRecord.Discipline).Include(x => x.TimeNorm)
                        .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<AcademicPlanRecordElementViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<AcademicPlanRecordElementViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<AcademicPlanRecordElementViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateAcademicPlanRecordElementViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordElementViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlanRecordElement(AcademicPlanRecordElementSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(model);

                    var exsistEntity = context.AcademicPlanRecordElements.FirstOrDefault(x => x.AcademicPlanRecordId == entity.AcademicPlanRecordId && x.TimeNormId == entity.TimeNormId);
                    if (exsistEntity == null)
                    {
                        context.AcademicPlanRecordElements.Add(entity);
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

        public ResultService UpdateAcademicPlanRecordElement(AcademicPlanRecordElementSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordElements.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecordElement(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteAcademicPlanRecordElement(AcademicPlanRecordElementGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordElements.FirstOrDefault(x => x.Id == model.Id);
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