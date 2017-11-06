using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class AcademicPlanService : IAcademicPlanService
	{
		private readonly DepartmentDbContext _context;

		private readonly IAcademicYearService _serviceAY;

		private readonly IEducationDirectionService _serviceED;

		public AcademicPlanService(DepartmentDbContext context, IEducationDirectionService serviceED,
			IAcademicYearService serviceAY)
		{
			_context = context;
			_serviceAY = serviceAY;
			_serviceED = serviceED;
		}


		public ResultService<List<AcademicYearViewModel>> GetAcademicYears()
		{
			return _serviceAY.GetAcademicYears();
		}

		public ResultService<List<EducationDirectionViewModel>> GetEducationDirections()
		{
			return _serviceED.GetEducationDirections();
		}


		public ResultService<List<AcademicPlanViewModel>> GetAcademicPlans()
		{
			try
			{
				return ResultService<List<AcademicPlanViewModel>>.Success(
					ModelFactoryToViewModel.CreateAcademicPlans(_context.AcademicPlans
						.Include(ap => ap.AcademicYear).Include(s => s.EducationDirection)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<AcademicPlanViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<AcademicPlanViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<AcademicPlanViewModel> GetAcademicPlan(AcademicPlanGetBindingModel model)
		{
			try
			{
				var entity = _context.AcademicPlans.Include(ap => ap.AcademicYear).Include(ap => ap.EducationDirection)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<AcademicPlanViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<AcademicPlanViewModel>.Success(
					ModelFactoryToViewModel.CreateAcademicPlanViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AcademicPlanViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AcademicPlanViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateAcademicPlan(AcademicPlanRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateAcademicPlan(model);
			try
			{
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
				var entity = _context.AcademicPlans
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
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
				var entity = _context.AcademicPlans
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
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
