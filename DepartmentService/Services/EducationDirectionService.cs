using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;

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
				return ResultService<List<EducationDirectionViewModel>>.Success(ModelFactory.CreateEducationDirections(
						_context.EducationDirections
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<EducationDirectionViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<EducationDirectionViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<EducationDirectionViewModel> GetEducationDirection(EducationDirectionGetBindingModel model)
		{
			try
			{
				var entity = _context.EducationDirections
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<EducationDirectionViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<EducationDirectionViewModel>.Success(
					ModelFactory.CreateEducationDirectionViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<EducationDirectionViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<EducationDirectionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateEducationDirection(EducationDirectionRecordBindingModel model)
		{
			var entity = new EducationDirection
			{
				Cipher = model.Cipher,
				Description = model.Description,
				Title = model.Title,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
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
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.Cipher = model.Cipher;
				entity.Description = model.Description;
				entity.Title = model.Title;

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

		public ResultService DeleteEducationDirection(EducationDirectionGetBindingModel model)
		{
			try
			{
				var entity = _context.EducationDirections
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
