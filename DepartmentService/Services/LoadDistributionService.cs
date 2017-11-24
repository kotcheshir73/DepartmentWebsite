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
	public class LoadDistributionService : ILoadDistributionService
	{
		private readonly DepartmentDbContext _context;

		private readonly IAcademicYearService _serviceAY;

		public LoadDistributionService(DepartmentDbContext context, IAcademicYearService serviceAY)
		{
			_context = context;
			_serviceAY = serviceAY;
		}


		public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
		{
			return _serviceAY.GetAcademicYears(model);
		}


		public ResultService<List<LoadDistributionViewModel>> GetLoadDistributions()
		{
			try
			{
				return ResultService<List<LoadDistributionViewModel>>.Success(
					ModelFactoryToViewModel.CreateLoadDistributions(_context.LoadDistributions
						.Include(ap => ap.AcademicYear)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<LoadDistributionViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<LoadDistributionViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LoadDistributionViewModel> GetLoadDistribution(LoadDistributionGetBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributions.Include(ap => ap.AcademicYear)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<LoadDistributionViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<LoadDistributionViewModel>.Success(
					ModelFactoryToViewModel.CreateLoadDistributionViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLoadDistribution(LoadDistributionRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateLoadDistribution(model);
			try
			{
				_context.LoadDistributions.Add(entity);
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

		public ResultService UpdateLoadDistribution(LoadDistributionRecordBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributions
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateLoadDistribution(model, entity);
				
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

		public ResultService DeleteLoadDistribution(LoadDistributionGetBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributions
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
