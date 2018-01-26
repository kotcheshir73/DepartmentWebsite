using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class KindOfLoadService : IKindOfLoadService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Виды_нагрузок;

		public KindOfLoadService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<KindOfLoadPageViewModel> GetKindOfLoads(KindOfLoadGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по видам нагрузки");
				}

				int countPages = 0;
				var query = _context.KindOfLoads.Where(kol => !kol.IsDeleted).AsQueryable();

                query = query.OrderBy(kol => kol.KindOfLoadType).ThenBy(kol => kol.KindOfLoadName);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new KindOfLoadPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateKindOfLoadViewModel).ToList()
                };

				return ResultService<KindOfLoadPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<KindOfLoadPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<KindOfLoadPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<KindOfLoadViewModel> GetKindOfLoad(KindOfLoadGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по видам нагрузки");
				}

				var entity = _context.KindOfLoads
								.FirstOrDefault(kol => kol.Id == model.Id && !kol.IsDeleted);
				if (entity == null)
				{
					return ResultService<KindOfLoadViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<KindOfLoadViewModel>.Success(ModelFactoryToViewModel.CreateKindOfLoadViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<KindOfLoadViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<KindOfLoadViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateKindOfLoad(KindOfLoadRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по видам нагрузки");
				}

				var entity = ModelFacotryFromBindingModel.CreateKindOfLoad(model);

				_context.KindOfLoads.Add(entity);
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

		public ResultService UpdateKindOfLoad(KindOfLoadRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по видам нагрузки");
				}

				var entity = _context.KindOfLoads.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateKindOfLoad(model, entity);
				
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

		public ResultService DeleteKindOfLoad(KindOfLoadGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по видам нагрузки");
				}

				var entity = _context.KindOfLoads.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
