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
	public class ContingentService : IContingentService
	{
		private readonly DepartmentDbContext _context;

		private readonly IAcademicYearService _serviceAY;

		private readonly IEducationDirectionService _serviceED;

		public ContingentService(DepartmentDbContext context, IEducationDirectionService serviceED, IAcademicYearService serviceAY)
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


		public ResultService<List<ContingentViewModel>> GetContingents()
		{
			try
			{
				return ResultService<List<ContingentViewModel>>.Success(
					ModelFactoryToViewModel.CreateContingents(_context.Contingents
						.Include(ap => ap.AcademicYear).Include(s => s.EducationDirection)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ContingentViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ContingentViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ContingentViewModel> GetContingent(ContingentGetBindingModel model)
		{
			try
			{
				var entity = _context.Contingents.Include(ap => ap.AcademicYear).Include(s => s.EducationDirection)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<ContingentViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<ContingentViewModel>.Success(
					ModelFactoryToViewModel.CreateContingentViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ContingentViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ContingentViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateContingent(ContingentRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateContingent(model);
			try
			{
				_context.Contingents.Add(entity);
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

		public ResultService UpdateContingent(ContingentRecordBindingModel model)
		{
			try
			{
				var entity = _context.Contingents
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateContingent(model, entity);
				
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

		public ResultService DeleteContingent(ContingentGetBindingModel model)
		{
			try
			{
				var entity = _context.Contingents
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
