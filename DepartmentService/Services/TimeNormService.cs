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
	public class TimeNormService : ITimeNormService
	{
		private readonly DepartmentDbContext _context;

		private readonly IKindOfLoadService _serviceKL;

		public TimeNormService(DepartmentDbContext context, IKindOfLoadService serviceKL)
		{
			_context = context;
			_serviceKL = serviceKL;
		}


		public ResultService<List<KindOfLoadViewModel>> GetKindOfLoads()
		{
			return _serviceKL.GetKindOfLoads();
		}


		public ResultService<List<TimeNormViewModel>> GetTimeNorms()
		{
			try
			{
				return ResultService<List<TimeNormViewModel>>.Success(ModelFactoryToViewModel.CreateTimeNorms(
						_context.TimeNorms.Include(tn => tn.KindOfLoad)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<TimeNormViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<TimeNormViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<TimeNormViewModel> GetTimeNorm(TimeNormGetBindingModel model)
		{
			try
			{
				var entity = _context.TimeNorms.Include(tn => tn.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<TimeNormViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<TimeNormViewModel>.Success(
					ModelFactoryToViewModel.CreateTimeNormViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<TimeNormViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<TimeNormViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateTimeNorm(TimeNormRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateTimeNorm(model);
			try
			{
				_context.TimeNorms.Add(entity);
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

		public ResultService UpdateTimeNorm(TimeNormRecordBindingModel model)
		{
			try
			{
				var entity = _context.TimeNorms
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateTimeNorm(model, entity);
				
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

		public ResultService DeleteTimeNorm(TimeNormGetBindingModel model)
		{
			try
			{
				var entity = _context.TimeNorms
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
