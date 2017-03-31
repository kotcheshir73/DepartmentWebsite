using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using System.Data.Entity.Validation;
using DepartmentDAL.Models;

namespace DepartmentService.Services
{
	public class DisciplineService : IDisciplineService
	{
		private readonly DepartmentDbContext _context;

		public DisciplineService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<List<DisciplineViewModel>> GetDisciplines()
		{
			try
			{
				return ResultService<List<DisciplineViewModel>>.Success(ModelFactory.CreateDisciplines(
						_context.Disciplines
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
					ModelFactory.CreateDisciplineViewModel(entity));
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
				DisciplineName = model.DisciplineName,
				DateCreate = DateTime.Now,
				IsDeleted = false,
			};
			try
			{
				_context.Disciplines.Add(entity);
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
