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
	public class ClassroomService : IClassroomService
	{
		private readonly DepartmentDbContext _context;

		public ClassroomService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<ClassroomViewModel>> GetClassrooms()
		{
			try
			{
				return ResultService<List<ClassroomViewModel>>.Success(
					ModelFactoryToViewModel.CreateClassrooms(_context.Classrooms
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ClassroomViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ClassroomViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ClassroomViewModel> GetClassroom(ClassroomGetBindingModel model)
		{
			try
			{
				var entity = _context.Classrooms
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<ClassroomViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<ClassroomViewModel>.Success(
					ModelFactoryToViewModel.CreateClassroomViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ClassroomViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ClassroomViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateClassroom(ClassroomRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateClassroom(model);
			try
			{
				_context.Classrooms.Add(entity);
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

		public ResultService UpdateClassroom(ClassroomRecordBindingModel model)
		{
			try
			{
				var entity = _context.Classrooms
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateClassroom(model, entity);
				
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

		public ResultService DeleteClassroom(ClassroomGetBindingModel model)
		{
			try
			{
				var entity = _context.Classrooms
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
