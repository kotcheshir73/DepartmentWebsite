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
	public class AccessService : IAccessService
	{
		private readonly DepartmentDbContext _context;

		public AccessService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<List<AccessViewModel>> GetAccesses(AccessGetBindingModel model)
		{
			try
			{
				if (model.RoleId.HasValue)
				{
					return ResultService<List<AccessViewModel>>.Success(
						ModelFactoryToViewModel.CreateAccesses(_context.Accesses
								.Where(e => e.RoleId == model.RoleId && !e.IsDeleted))
						.ToList());
				}
				return ResultService<List<AccessViewModel>>.Success(
					ModelFactoryToViewModel.CreateAccesses(_context.Accesses
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<AccessViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<AccessViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<AccessViewModel> GetAccess(AccessGetBindingModel model)
		{
			try
			{
				var entity = model.Id.HasValue ?_context.Accesses
								.FirstOrDefault(e => e.Id == model.Id.Value && !e.IsDeleted) :
								model.RoleId.HasValue && !string.IsNullOrEmpty(model.Operation) ? _context.Accesses
								.FirstOrDefault(e => e.RoleId == model.RoleId.Value && e.Operation == model.Operation && !e.IsDeleted) : null ;
				if (entity == null)
					return ResultService<AccessViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<AccessViewModel>.Success(
					ModelFactoryToViewModel.CreateAccessViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AccessViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AccessViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateAccess(AccessRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateAccess(model);
			try
			{
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
				var entity = _context.Accesses
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
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
				var entity = _context.Accesses
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
