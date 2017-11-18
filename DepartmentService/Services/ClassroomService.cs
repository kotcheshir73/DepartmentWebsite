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
	public class ClassroomService : IClassroomService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Аудитории;

		public ClassroomService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model)
		{
			try
			{
				if(!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных");
				}

				int countPages = 0;
				var query = _context.Classrooms.Where(c => !c.IsDeleted).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Floor((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(c => c.Id)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new ClassroomPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateClassrooms(query).ToList()
				};

				return ResultService<ClassroomPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ClassroomPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ClassroomPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ClassroomViewModel> GetClassroom(ClassroomGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных");
				}

				var entity = _context.Classrooms
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<ClassroomViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<ClassroomViewModel>.Success(ModelFactoryToViewModel.CreateClassroomViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ClassroomViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ClassroomViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateClassroom(ClassroomRecordBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
					{
						throw new Exception("Нет доступа на изменение данных");
					}

					var entity = ModelFacotryFromBindingModel.CreateClassroom(model);

					_context.Classrooms.Add(entity);
					_context.SaveChanges();

					transaction.Commit();

					return ResultService.Success(entity.Id);
				}
				catch (DbEntityValidationException ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}

		public ResultService UpdateClassroom(ClassroomRecordBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
					{
						throw new Exception("Нет доступа на изменение данных");
					}

					var entity = _context.Classrooms
									.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
					if (entity == null)
					{
						return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
					}
					entity = ModelFacotryFromBindingModel.CreateClassroom(model, entity);

					_context.SaveChanges();

					transaction.Commit();

					return ResultService.Success();
				}
				catch (DbEntityValidationException ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}

		public ResultService DeleteClassroom(ClassroomGetBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
					{
						throw new Exception("Нет доступа на удаление данных");
					}

					var entity = _context.Classrooms
									.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
					if (entity == null)
					{
						return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
					}
					entity.IsDeleted = true;
					entity.DateDelete = DateTime.Now;

					_context.SaveChanges();

					transaction.Commit();

					return ResultService.Success();
				}
				catch (DbEntityValidationException ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}
	}
}
