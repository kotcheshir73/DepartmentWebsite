using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class DisciplineService : IDisciplineService
	{
		private readonly DepartmentDbContext _context;

		private readonly IDisciplineBlockService _serviceDB;

		public DisciplineService(DepartmentDbContext context, IDisciplineBlockService serviceDB)
		{
			_context = context;
			_serviceDB = serviceDB;
		}


		public ResultService<List<DisciplineBlockViewModel>> GetDisciplineBlocks()
		{
			return _serviceDB.GetDisciplineBlocks();
		}


		public ResultService<List<DisciplineViewModel>> GetDisciplines()
		{
			try
			{
				return ResultService<List<DisciplineViewModel>>.Success(ModelFactoryToViewModel.CreateDisciplines(
						_context.Disciplines
							.Include(d => d.DisciplineBlock)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<DisciplineViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<DisciplineViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<DisciplineViewModel> GetDiscipline(DisciplineGetBindingModel model)
		{
			try
			{
				var entity = _context.Disciplines
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<DisciplineViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<DisciplineViewModel>.Success(
					ModelFactoryToViewModel.CreateDisciplineViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<DisciplineViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<DisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateDiscipline(DisciplineRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateDiscipline(model);
			try
			{
				_context.Disciplines.Add(entity);
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

		public ResultService UpdateDiscipline(DisciplineRecordBindingModel model)
		{
			try
			{
				var entity = _context.Disciplines
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateDiscipline(model, entity);
				
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

		public ResultService DeleteDiscipline(DisciplineGetBindingModel model)
		{
			try
			{
				var entity = _context.Disciplines
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
