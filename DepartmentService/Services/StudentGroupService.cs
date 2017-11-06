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
	public class StudentGroupService : IStudentGroupService
	{
		private readonly DepartmentDbContext _context;

		private readonly IEducationDirectionService _serviceED;

		public StudentGroupService(DepartmentDbContext context, IEducationDirectionService serviceED)
		{
			_context = context;
			_serviceED = serviceED;
		}


		public ResultService<List<EducationDirectionViewModel>> GetEducationDirections()
		{
			return _serviceED.GetEducationDirections();
		}


		public ResultService<List<StudentGroupViewModel>> GetStudentGroups()
		{
			try
			{
				return ResultService<List<StudentGroupViewModel>>.Success(
					ModelFactoryToViewModel.CreateStudentGroups(_context.StudentGroups
						.Include(s => s.EducationDirection).Include(s => s.Students)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<StudentGroupViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<StudentGroupViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<StudentGroupViewModel> GetStudentGroup(StudentGroupGetBindingModel model)
		{
			try
			{
				var entity = _context.StudentGroups.Include(s => s.EducationDirection).Include(s => s.Steward)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<StudentGroupViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<StudentGroupViewModel>.Success(
					ModelFactoryToViewModel.CreateStudentGroupViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentGroupViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentGroupViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateStudentGroup(StudentGroupRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateStudentGroup(model);
			try
			{
				_context.StudentGroups.Add(entity);
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

		public ResultService UpdateStudentGroup(StudentGroupRecordBindingModel model)
		{
			try
			{
				var entity = _context.StudentGroups
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateStudentGroup(model, entity);
				
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

		public ResultService DeleteStudentGroup(StudentGroupGetBindingModel model)
		{
			try
			{
				var entity = _context.StudentGroups
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
