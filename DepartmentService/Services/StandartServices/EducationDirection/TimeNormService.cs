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
	public class TimeNormService : ITimeNormService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Нормы_времени;

		private readonly IKindOfLoadService _serviceKL;

		public TimeNormService(DepartmentDbContext context, IKindOfLoadService serviceKL)
		{
			_context = context;
			_serviceKL = serviceKL;
		}


		public ResultService<KindOfLoadPageViewModel> GetKindOfLoads(KindOfLoadGetBindingModel model)
		{
			return _serviceKL.GetKindOfLoads(model);
		}


		public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по нормам времени");
				}

				int countPages = 0;
				var query = _context.TimeNorms.Where(c => !c.IsDeleted).AsQueryable();

                query = query.OrderBy(e => e.KindOfLoad.KindOfLoadName).ThenBy(e => e.Title);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(tn => tn.KindOfLoad);

				var result = new TimeNormPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateTimeNorms(query).ToList()
				};

				return ResultService<TimeNormPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<TimeNormPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<TimeNormPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<TimeNormViewModel> GetTimeNorm(TimeNormGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по нормам времени");
				}

				var entity = _context.TimeNorms.Include(tn => tn.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<TimeNormViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<TimeNormViewModel>.Success(ModelFactoryToViewModel.CreateTimeNormViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<TimeNormViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<TimeNormViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateTimeNorm(TimeNormRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нормам времени");
				}

				var entity = ModelFacotryFromBindingModel.CreateTimeNorm(model);

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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нормам времени");
				}

				var entity = _context.TimeNorms
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нормам времени");
				}

				var entity = _context.TimeNorms
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
