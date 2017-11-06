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
	public class EducationDirectionService : IEducationDirectionService
	{
		private readonly DepartmentDbContext _context;

		public EducationDirectionService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<EducationDirectionViewModel>> GetEducationDirections()
		{
			try
			{
				return ResultService<List<EducationDirectionViewModel>>.Success(ModelFactoryToViewModel.CreateEducationDirections(
						_context.EducationDirections
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<EducationDirectionViewModel>>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<EducationDirectionViewModel>>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<EducationDirectionViewModel> GetEducationDirection(EducationDirectionGetBindingModel model)
		{
			try
			{
				var entity = _context.EducationDirections
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<EducationDirectionViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				return ResultService<EducationDirectionViewModel>.Success(ModelFactoryToViewModel.CreateEducationDirectionViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<EducationDirectionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<EducationDirectionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateEducationDirection(EducationDirectionRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateEducationDirection(model);
			try
			{
				_context.EducationDirections.Add(entity);
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

		public ResultService UpdateEducationDirection(EducationDirectionRecordBindingModel model)
		{
			try
			{
				var entity = _context.EducationDirections
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateEducationDirection(model, entity);
				
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

		public ResultService DeleteEducationDirection(EducationDirectionGetBindingModel model)
		{
			try
			{
				var entity = _context.EducationDirections
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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
