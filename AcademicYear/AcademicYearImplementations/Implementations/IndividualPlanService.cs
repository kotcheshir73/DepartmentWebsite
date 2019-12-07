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
    public class IndividualPlanService : IIndividualPlanService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Индивидуальный_план;

        private readonly string _entity = "Индивидуальный план";

        private readonly IAcademicYearService _serviceAY;

        private readonly ILecturerService _serviceL;

        public IndividualPlanService(IAcademicYearService serviceAY, ILecturerService serviceL)
        {
            _serviceAY = serviceAY;
            _serviceL = serviceL;
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            return _serviceL.GetLecturers(model);
        }

        public ResultService<IndividualPlanPageViewModel> GetIndividualPlans(IndividualPlanGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.IndividualPlans.Where(x => !x.IsDeleted);

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.LecturerId.HasValue)
                    {
                        query = query.Where(x => x.LecturerId == model.LecturerId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.AcademicYearId).ThenBy(x => x.LecturerId);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query
                        .Include(x => x.AcademicYear)
                        .Include(x => x.Lecturer);

                    var result = new IndividualPlanPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateIndividualPlanViewModel).ToList()
                    };

                    return ResultService<IndividualPlanPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanViewModel> GetIndividualPlan(IndividualPlanGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlans
                                .Include(x => x.AcademicYear)
                                .Include(x => x.Lecturer)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<IndividualPlanViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<IndividualPlanViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<IndividualPlanViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateIndividualPlanViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlan(IndividualPlanSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlan(model);

                    var exsistEntity = context.IndividualPlans.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId &&
                            x.LecturerId == entity.LecturerId);
                    if (exsistEntity == null)
                    {
                        context.IndividualPlans.Add(entity);
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

        public ResultService UpdateIndividualPlan(IndividualPlanSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlans.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlan(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteIndividualPlan(IndividualPlanGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlans.FirstOrDefault(x => x.Id == model.Id);
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