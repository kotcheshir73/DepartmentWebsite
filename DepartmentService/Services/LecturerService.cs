using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
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


		public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по преподавателям");
				}

				int countPages = 0;
				var query = _context.Lecturers.Where(c => !c.IsDeleted).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(c => c.LastName)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new LecturerPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateLecturers(query).ToList()
				};

				return ResultService<LecturerPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LecturerPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LecturerPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LecturerViewModel> GetLecturer(LecturerGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по аудиториям");
				}

				var entity = _context.Lecturers
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<LecturerViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<LecturerViewModel>.Success(ModelFactoryToViewModel.CreateLecturerViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLecturer(LecturerRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по аудиториям");
				}

				var entity = ModelFacotryFromBindingModel.CreateLecturer(model);

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
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по аудиториям");
				}

				var entity = _context.Lecturers
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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

		public ResultService DeleteLecturer(LecturerGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по аудиториям");
				}

				var entity = _context.Lecturers
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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
