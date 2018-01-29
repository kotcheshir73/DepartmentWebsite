using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class AcademicPlanService : IAcademicPlanService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

		private readonly IAcademicYearService _serviceAY;

		private readonly IEducationDirectionService _serviceED;

		public AcademicPlanService(DepartmentDbContext context, IEducationDirectionService serviceED, IAcademicYearService serviceAY)
		{
			_context = context;
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по учебным планам");
				}

				int countPages = 0;
				var query = _context.AcademicPlans.Where(ap => !ap.IsDeleted).AsQueryable();

                if (model.AcademicYearId.HasValue)
                {
                    query = query.Where(ap => ap.AcademicYearId == model.AcademicYearId);
                }

                query = query.OrderBy(ap => ap.AcademicYear.Title).ThenBy(ap => ap.EducationDirection.Cipher).ThenBy(ap => ap.AcademicCourses);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(ap => ap.AcademicYear).Include(ap => ap.EducationDirection);

				var result = new AcademicPlanPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateAcademicPlanViewModel).ToList()
                };

				return ResultService<AcademicPlanPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AcademicPlanPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по учебным планам");
				}

				var entity = _context.AcademicPlans
                                .Include(ap => ap.AcademicYear)
                                .Include(ap => ap.EducationDirection)
								.FirstOrDefault(ap => ap.Id == model.Id && !ap.IsDeleted);
				if (entity == null)
				{
					return ResultService<AcademicPlanViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<AcademicPlanViewModel>.Success(ModelFactoryToViewModel.CreateAcademicPlanViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AcademicPlanViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AcademicPlanViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateAcademicPlan(AcademicPlanRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по учебным планам");
				}

				var entity = ModelFacotryFromBindingModel.CreateAcademicPlan(model);

				_context.AcademicPlans.Add(entity);
				_context.SaveChanges();

				return ResultService.Success(entity.Id);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService UpdateAcademicPlan(AcademicPlanRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по учебным планам");
				}

				var entity = _context.AcademicPlans.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				entity = ModelFacotryFromBindingModel.CreateAcademicPlan(model, entity);
				
				_context.SaveChanges();

				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по учебным планам");
				}

				var entity = _context.AcademicPlans.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity.IsDeleted = true;
				entity.DateDelete = DateTime.Now;
				
				_context.SaveChanges();

				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}
	}
}
