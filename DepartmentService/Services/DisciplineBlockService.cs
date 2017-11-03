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
	public class DisciplineBlockService : IDisciplineBlockService
	{
		private readonly DepartmentDbContext _context;

		public DisciplineBlockService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<DisciplineBlockViewModel>> GetDisciplineBlocks()
		{
			try
			{
				return ResultService<List<DisciplineBlockViewModel>>.Success(ModelFactoryToViewModel.CreateDisciplineBlocks(
						_context.DisciplineBlocks
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<DisciplineBlockViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<DisciplineBlockViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<DisciplineBlockViewModel> GetDisciplineBlock(DisciplineBlockGetBindingModel model)
		{
			try
			{
				var entity = _context.DisciplineBlocks
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<DisciplineBlockViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<DisciplineBlockViewModel>.Success(
					ModelFactoryToViewModel.CreateDisciplineBlockViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<DisciplineBlockViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<DisciplineBlockViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateDisciplineBlock(DisciplineBlockRecordBindingModel model)
		{
			var entity = new DisciplineBlock
			{
				Title = model.Title,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.DisciplineBlocks.Add(entity);
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

		public ResultService UpdateDisciplineBlock(DisciplineBlockRecordBindingModel model)
		{
			try
			{
				var entity = _context.DisciplineBlocks
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
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

		public ResultService DeleteDisciplineBlock(DisciplineBlockGetBindingModel model)
		{
			try
			{
				var entity = _context.DisciplineBlocks
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
