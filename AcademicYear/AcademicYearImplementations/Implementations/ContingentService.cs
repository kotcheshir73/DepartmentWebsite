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
    public class ContingentService : IContingentService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Контингент;

        private readonly string _entity = "Контингент";

        private readonly IAcademicYearService _serviceAY;

        private readonly IEducationDirectionService _serviceED;

        public ContingentService(IEducationDirectionService serviceED, IAcademicYearService serviceAY)
        {
            _serviceAY = serviceAY;
            _serviceED = serviceED;
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
        {
            return _serviceED.GetEducationDirections(model);
        }

        public ResultService<ContingentPageViewModel> GetContingents(ContingentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Contingents.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.AcademicPlanId.HasValue)
                    {
                        var ap = context.AcademicPlans.FirstOrDefault(x => x.Id == model.AcademicPlanId);
                        query = query.Where(x => x.AcademicYearId == ap.AcademicYearId);
                    }

                    query = query.OrderBy(x => x.AcademicYearId).ThenBy(x => x.EducationDirection.Cipher).ThenBy(x => x.Course);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicYear).Include(x => x.EducationDirection);

                    var result = new ContingentPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateContingentViewModel).ToList()
                    };

                    return ResultService<ContingentPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ContingentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<ContingentViewModel> GetContingent(ContingentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Contingents
                                .Include(x => x.AcademicYear)
                                .Include(x => x.EducationDirection)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ContingentViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ContingentViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ContingentViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateContingentViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ContingentViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateContingent(ContingentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateContingent(model);

                    var exsistEntity = context.Contingents.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.EducationDirectionId == entity.EducationDirectionId &&
                            x.ContingentName == entity.ContingentName);
                    if (exsistEntity == null)
                    {
                        context.Contingents.Add(entity);
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

        public ResultService UpdateContingent(ContingentSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Contingents.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateContingent(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteContingent(ContingentGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Contingents.FirstOrDefault(x => x.Id == model.Id);
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