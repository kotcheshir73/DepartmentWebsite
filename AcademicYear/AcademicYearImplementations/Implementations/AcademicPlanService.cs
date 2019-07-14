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
    public class AcademicPlanService : IAcademicPlanService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

        private readonly string _entity = "Учебные планы";

        private readonly IAcademicYearService _serviceAY;

		private readonly IEducationDirectionService _serviceED;

		public AcademicPlanService(IEducationDirectionService serviceED, IAcademicYearService serviceAY)
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


		public ResultService<AcademicPlanPageViewModel> GetAcademicPlans(AcademicPlanGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.AcademicPlans.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.Id.HasValue)
                    {
                        query = query.Where(x => x.Id == model.Id);
                    }

                    query = query.OrderBy(x => x.AcademicYear.Title).ThenBy(x => x.EducationDirection.Cipher).ThenBy(x => x.AcademicCourses);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicYear).Include(x => x.EducationDirection);

                    var result = new AcademicPlanPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateAcademicPlanViewModel).ToList()
                    };

                    return ResultService<AcademicPlanPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<AcademicPlanViewModel> GetAcademicPlan(AcademicPlanGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlans
                                .Include(x => x.AcademicYear)
                                .Include(x => x.EducationDirection)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<AcademicPlanViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<AcademicPlanViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<AcademicPlanViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateAcademicPlanViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicPlanViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateAcademicPlan(AcademicPlanSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateAcademicPlan(model);

                    var exsistEntity = context.AcademicPlans.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.EducationDirectionId == entity.EducationDirectionId && 
                                x.AcademicCourses == entity.AcademicCourses);
                    if (exsistEntity == null)
                    {
                        context.AcademicPlans.Add(entity);
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

		public ResultService UpdateAcademicPlan(AcademicPlanSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlans.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateAcademicPlan(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteAcademicPlan(AcademicPlanGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.AcademicPlans.FirstOrDefault(x => x.Id == model.Id);
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