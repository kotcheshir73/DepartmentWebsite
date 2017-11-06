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
	public class StudentHistoryService : IStudentHistoryService
	{
		private readonly DepartmentDbContext _context;

		public StudentHistoryService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<StudentHistoryViewModel>> GetStudentHistorys(StudentHistoryGetBindingModel model)
		{
			try
			{
				if (!string.IsNullOrEmpty(model.NumberOfBook))
				{
					return ResultService<List<StudentHistoryViewModel>>.Success(
						ModelFactoryToViewModel.CreateStudentHistorys(_context.StudentHistorys
										   .Where(sh => sh.StudentId == model.NumberOfBook))
									.ToList());
				}
				return ResultService<List<StudentHistoryViewModel>>.Success(
					ModelFactoryToViewModel.CreateStudentHistorys(_context.StudentHistorys)
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<StudentHistoryViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<StudentHistoryViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<StudentHistoryViewModel> GetStudentHistory(StudentHistoryGetBindingModel model)
		{
			try
			{
				var entity = _context.StudentHistorys
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
					return ResultService<StudentHistoryViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<StudentHistoryViewModel>.Success(
					ModelFactoryToViewModel.CreateStudentHistoryViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentHistoryViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentHistoryViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateStudentHistory(StudentHistoryRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateStudentHistory(model);
			try
			{
				_context.StudentHistorys.Add(entity);
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

		public ResultService UpdateStudentHistory(StudentHistoryRecordBindingModel model)
		{
			try
			{
				var entity = _context.StudentHistorys
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateStudentHistory(model, entity);
				
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

		public ResultService DeleteStudentHistory(StudentHistoryGetBindingModel model)
		{
			try
			{
				var entity = _context.StudentHistorys
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.StudentHistorys.Remove(entity);

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
