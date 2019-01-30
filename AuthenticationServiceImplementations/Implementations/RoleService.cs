﻿using AuthenticationServiceImplementations;
using AuthenticationServiceInterfaces.BindingModels;
using AuthenticationServiceInterfaces.Interfaces;
using AuthenticationServiceInterfaces.ViewModels;
using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace AuthenticationServiceImplementations.Implementations
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
				var query = _context.Roles.Where(r => !r.IsDeleted).AsQueryable();

                query = query.OrderBy(r => r.Name);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new RolePageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(AuthenticationModelFactoryToViewModel.CreateRoleViewModel).ToList()
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
								.FirstOrDefault(r => r.Id == model.Id && !r.IsDeleted);
				if (entity == null)
				{
					return ResultService<RoleViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<RoleViewModel>.Success(AuthenticationModelFactoryToViewModel.CreateRoleViewModel(entity));
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

		public ResultService CreateRole(RoleSetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по ролям");
				}

				var entity = AuthenticationModelFacotryFromBindingModel.CreateRole(model);

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

		public ResultService UpdateRole(RoleSetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по ролям");
				}

				var entity = _context.Roles.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = AuthenticationModelFacotryFromBindingModel.CreateRole(model, entity);

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

				var entity = _context.Roles.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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