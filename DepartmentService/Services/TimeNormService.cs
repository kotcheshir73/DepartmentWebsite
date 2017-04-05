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
	public class TimeNormService : ITimeNormService
	{
		private readonly DepartmentDbContext _context;

		private readonly IKindOfLoadService _serviceKL;

		public TimeNormService(DepartmentDbContext context, IKindOfLoadService serviceKL)
		{
			_context = context;
			_serviceKL = serviceKL;
		}

		public ResultService<List<TimeNormViewModel>> GetTimeNorms()
		{
			try
			{
				return ResultService<List<TimeNormViewModel>>.Success(ModelFactory.CreateTimeNorms(
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

		public ResultService<List<KindOfLoadViewModel>> GetKindOfLoads()
		{
			return _serviceKL.GetKindOfLoads();
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
					ModelFactory.CreateTimeNormViewModel(entity));
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
			var entity = new TimeNorm
			{
				Title = model.Title,
				KindOfLoadId = model.KindOfLoadId,
				ParentTimeNormId = model.ParentTimeNormId,
				Hours = model.Hours,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.TimeNorms.Add(entity);
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
				entity.KindOfLoadId = model.KindOfLoadId;
				entity.ParentTimeNormId = model.ParentTimeNormId;
				entity.Hours = model.Hours;

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
