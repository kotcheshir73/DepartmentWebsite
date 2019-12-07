using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class AcademicPlanRecordMissionService : IAcademicPlanRecordMissionService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        private readonly string _entity = "Учебные планы";

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        private readonly ILecturerService _serviceL;

        public AcademicPlanRecordMissionService(IAcademicPlanRecordElementService serviceAPRE, ILecturerService serviceL)
        {
            _serviceAPRE = serviceAPRE;
            _serviceL = serviceL;
        }

        public ResultService<AcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(AcademicPlanRecordElementGetBindingModel model)
        {
            return _serviceAPRE.GetAcademicPlanRecordElements(model);
        }

        public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            return _serviceL.GetLecturers(model);
        }

        public ResultService<AcademicPlanRecordMissionPageViewModel> GetAcademicPlanRecordMissions(AcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlanRecordMissions.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.AcademicPlanRecordElementId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanRecordElementId == model.AcademicPlanRecordElementId);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        query = query.Where(x => x.LecturerId == model.LecturerId);
                    }
                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId);
                    }

                    query = query.OrderBy(x => x.AcademicPlanRecordElementId).ThenBy(x => x.LecturerId);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicPlanRecordElement).Include(x => x.AcademicPlanRecordElement.TimeNorm).Include(x => x.Lecturer);

                    var result = new AcademicPlanRecordMissionPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateAcademicPlanRecordMissionViewModel).ToList()
                    };

                    return ResultService<AcademicPlanRecordMissionPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordMissionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<AcademicPlanRecordMissionViewModel> GetAcademicPlanRecordMission(AcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordMissions
                                .Include(x => x.AcademicPlanRecordElement)
                                .Include(x => x.AcademicPlanRecordElement.TimeNorm)
                                .Include(x => x.Lecturer)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<AcademicPlanRecordMissionViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<AcademicPlanRecordMissionViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<AcademicPlanRecordMissionViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateAcademicPlanRecordMissionViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanRecordMissionViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateAcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecordMission(model);

                    var exsistEntity = context.AcademicPlanRecordMissions.FirstOrDefault(x => x.AcademicPlanRecordElementId == entity.AcademicPlanRecordElementId && 
                            x.LecturerId == entity.LecturerId);
                    if (exsistEntity == null)
                    {
                        context.AcademicPlanRecordMissions.Add(entity);
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

        public ResultService UpdateAcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordMissions.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateAcademicPlanRecordMission(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteAcademicPlanRecordMission(AcademicPlanRecordMissionGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlanRecordMissions.FirstOrDefault(x => x.Id == model.Id);
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