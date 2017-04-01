using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using System.Data.Entity.Validation;
using DepartmentDAL.Models;

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

		public ResultService<List<AcademicPlanViewModel>> GetAcademicPlans()
		{
			try
			{
				return ResultService<List<AcademicPlanViewModel>>.Success(
					ModelFactory.CreateAcademicPlans(_context.AcademicPlans
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

		public ResultService<List<AcademicYearViewModel>> GetAcademicYears()
		{
			return _serviceAY.GetAcademicYears();
		}

		public ResultService<List<EducationDirectionViewModel>> GetEducationDirections()
		{
			return _serviceED.GetEducationDirections();
		}

		public ResultService<AcademicPlanViewModel> GetAcademicPlan(AcademicPlanGetBindingModel model)
		{
			try
			{
				var entity = _context.AcademicPlans.Include(s => s.EducationDirection)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<AcademicPlanViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<AcademicPlanViewModel>.Success(
					ModelFactory.CreateAcademicPlanViewModel(entity));
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
			var entity = new AcademicPlan
			{
				EducationDirectionId = model.EducationDirectionId,
				DateCreate = DateTime.Now,
				AcademicYearId = model.AcademicYearId,
				AcademicLevel = (AcademicLevel)Enum.Parse(typeof(AcademicLevel), model.AcademicLevel),
				AcademicCourses = (AcademicCourse)Enum.ToObject(typeof(AcademicLevel), model.AcademicCourses),
				IsDeleted = false
			};
			try
			{
				_context.AcademicPlans.Add(entity);
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
				entity.AcademicLevel = (AcademicLevel)Enum.Parse(typeof(AcademicLevel), model.AcademicLevel);
				entity.AcademicCourses = (AcademicCourse)Enum.ToObject(typeof(AcademicLevel), model.AcademicCourses);

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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
