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
using System.Data.Entity;
using System.Data.Entity.Validation;

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
			var entity = new Discipline
			{
				DisciplineBlockId = model.DisciplineBlockId,
				DisciplineName = model.DisciplineName,
				DateCreate = DateTime.Now,
				IsDeleted = false,
			};
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
				entity.DisciplineBlockId = model.DisciplineBlockId;
				entity.DisciplineName = model.DisciplineName;

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
