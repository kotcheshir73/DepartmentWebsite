using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class DisciplineTimeDistributionService : IDisciplineTimeDistributionService
    {
        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly IStudentGroupService _serviceSG;

        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        private readonly string _entity = "Расчасовки";

        public DisciplineTimeDistributionService(IAcademicPlanRecordService serviceAPR, IStudentGroupService serviceSG)
        {
            _serviceAPR = serviceAPR;
            _serviceSG = serviceSG;
        }

        public ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
        {
            return _serviceAPR.GetAcademicPlanRecords(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceSG.GetStudentGroups(model);
        }

        public ResultService<DisciplineTimeDistributionPageViewModel> GetDisciplineTimeDistributions(DisciplineTimeDistributionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordMissions.Where(x => !x.IsDeleted).AsQueryable();
                    if (model.LecturerId.HasValue)
                    {
                        query = query.Where(x => x.LecturerId == model.LecturerId);
                    }

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId);
                    }

                    query = query.Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan).Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions);

                    var grahp = query.SelectMany(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions).Distinct();

                    grahp = grahp.OrderBy(x => x.StudentGroup.GroupName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)grahp.Count() / model.PageSize.Value);
                        grahp = grahp
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }
                    
                    grahp = grahp.Include(x => x.AcademicPlanRecord.Discipline).Include(x => x.StudentGroup);

                    var result = new DisciplineTimeDistributionPageViewModel
                    {
                        MaxCount = countPages,
                        List = grahp.Select(AcademicYearModelFactoryToViewModel.CreateDisciplineTimeDistributionViewModel).ToList()
                    };

                    return ResultService<DisciplineTimeDistributionPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineTimeDistributionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<DisciplineTimeDistributionViewModel> GetDisciplineTimeDistribution(DisciplineTimeDistributionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributions
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<DisciplineTimeDistributionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<DisciplineTimeDistributionViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<DisciplineTimeDistributionViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateDisciplineTimeDistributionViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<DisciplineTimeDistributionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateDisciplineTimeDistribution(DisciplineTimeDistributionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistribution(model);

                    var exsistEntity = context.DisciplineTimeDistributions.FirstOrDefault(x => x.AcademicPlanRecordId == entity.AcademicPlanRecordId &&
                            x.StudentGroupId == entity.StudentGroupId);
                    if (exsistEntity == null)
                    {
                        context.DisciplineTimeDistributions.Add(entity);
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

        public ResultService UpdateDisciplineTimeDistribution(DisciplineTimeDistributionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributions.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateDisciplineTimeDistribution(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteDisciplineTimeDistribution(DisciplineTimeDistributionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DisciplineTimeDistributions.FirstOrDefault(x => x.Id == model.Id);
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