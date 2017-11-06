using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class RoleService : IRoleService
	{
		private readonly DepartmentDbContext _context;

		public RoleService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<List<RoleViewModel>> GetRoles()
		{
			try
			{
				return ResultService<List<RoleViewModel>>.Success(
					ModelFactoryToViewModel.CreateRoles(_context.Roles
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<RoleViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<RoleViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<RoleViewModel> GetRole(RoleGetBindingModel model)
		{
			try
			{
				var entity = _context.Roles
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<RoleViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<RoleViewModel>.Success(
					ModelFactoryToViewModel.CreateRoleViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<RoleViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<RoleViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateRole(RoleRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateRole(model);
			try
			{
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
				var entity = _context.Roles
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
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
				var entity = _context.Roles
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
