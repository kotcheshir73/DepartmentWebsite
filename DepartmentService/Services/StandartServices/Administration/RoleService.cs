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
	public class RoleService : IRoleService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Роли;

		public RoleService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<RolePageViewModel> GetRoles(RoleGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по ролям");
				}

				int countPages = 0;
				var query = _context.Roles.Where(c => !c.IsDeleted).AsQueryable();

				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(e => e.RoleName)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new RolePageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateRoles(query).ToList()
				};

				return ResultService<RolePageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<RolePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<RolePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<RoleViewModel> GetRole(RoleGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по ролям");
				}

				var entity = _context.Roles
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<RoleViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<RoleViewModel>.Success(ModelFactoryToViewModel.CreateRoleViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<RoleViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<RoleViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateRole(RoleRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по ролям");
				}

				var entity = ModelFacotryFromBindingModel.CreateRole(model);

				_context.Roles.Add(entity);
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

		public ResultService UpdateRole(RoleRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по ролям");
				}

				var entity = _context.Roles
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateRole(model, entity);

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

		public ResultService DeleteRole(RoleGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по ролям");
				}

				var entity = _context.Roles
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
