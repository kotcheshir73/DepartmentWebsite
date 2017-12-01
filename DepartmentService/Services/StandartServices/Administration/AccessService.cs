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
	public class AccessService : IAccessService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Доступы;

		public AccessService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<AccessPageViewModel> GetAccesses(AccessGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по доступам");
				}

				int countPages = 0;
				var query = _context.Accesses.Where(c => !c.IsDeleted).AsQueryable();

				if (model.RoleId.HasValue)
				{
					query = query.Where(e => e.RoleId == model.RoleId);
				}

				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(e => e.Operation).ThenBy(e => e.AccessType)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new AccessPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateAccesses(query).ToList()
				};

				return ResultService<AccessPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AccessPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AccessPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<AccessViewModel> GetAccess(AccessGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по доступам");
				}

				var entity = model.Id.HasValue ?_context.Accesses
								.FirstOrDefault(e => e.Id == model.Id.Value && !e.IsDeleted) :
								model.RoleId.HasValue && !string.IsNullOrEmpty(model.Operation) ? _context.Accesses
								.FirstOrDefault(e => e.RoleId == model.RoleId.Value && e.Operation.ToString() == model.Operation && !e.IsDeleted) : null ;
				if (entity == null)
				{
					return ResultService<AccessViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<AccessViewModel>.Success(ModelFactoryToViewModel.CreateAccessViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AccessViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AccessViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateAccess(AccessRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по доступам");
				}

				var entity = ModelFacotryFromBindingModel.CreateAccess(model);

				_context.Accesses.Add(entity);
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

		public ResultService UpdateAccess(AccessRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по доступам");
				}

				var entity = _context.Accesses
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateAccess(model, entity);

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

		public ResultService DeleteAccess(AccessGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по доступам");
				}

				var entity = _context.Accesses
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
