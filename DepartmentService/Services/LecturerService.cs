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
	public class LecturerService : ILecturerService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Преподаватели;

		public LecturerService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<LecturerViewModel>> GetLecturers(LecturerGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных");
				}
				return ResultService<List<LecturerViewModel>>.Success(ModelFactoryToViewModel.CreateLecturers(
						_context.Lecturers
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<LecturerViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<LecturerViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LecturerViewModel> GetLecturer(LecturerGetBindingModel model)
		{
			try
			{
				var entity = _context.Lecturers
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<LecturerViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<LecturerViewModel>.Success(
					ModelFactoryToViewModel.CreateLecturerViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LecturerViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLecturer(LecturerRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateLecturer(model);
			try
			{
				_context.Lecturers.Add(entity);
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

		public ResultService UpdateLecturer(LecturerRecordBindingModel model)
		{
			{
				try
				{
					var entity = _context.Lecturers
									.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
					if (entity == null)
					{
						return ResultService.Error("Error:", "Entity not found",
							ResultServiceStatusCode.NotFound);
					}
					entity = ModelFacotryFromBindingModel.CreateLecturer(model, entity);
					
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

		public ResultService DeleteLecturer(LecturerGetBindingModel model)
		{
			try
			{
				var entity = _context.Lecturers
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
