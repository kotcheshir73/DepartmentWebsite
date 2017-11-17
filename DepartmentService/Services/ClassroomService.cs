﻿using DepartmentDAL;
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
				if (!model.UserId.HasValue)
				{
					throw new Exception("Неизвестный пользователь");
				}
				if(!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View, model.UserId.Value))
				{
					throw new Exception("Нет доступа на чтение данных");
				}

				int countPages = 0;
				var query = _context.Classrooms.Where(c => !c.IsDeleted).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = query.Count();
					countPages = countPages / model.PageSize.Value + (countPages % model.PageSize.Value == 0 ? 0 : 1);
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
				if (!model.UserId.HasValue)
				{
					throw new Exception("Неизвестный пользователь");
				}
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View, model.UserId.Value))
				{
					throw new Exception("Нет доступа на чтение данных");
				}

				var entity = _context.Classrooms
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<ClassroomViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<ClassroomViewModel>.Success(
							ModelFactoryToViewModel.CreateClassroomViewModel(entity));
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
					if (!model.UserId.HasValue)
					{
						throw new Exception("Неизвестный пользователь");
					}
					if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change, model.UserId.Value))
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
			try
			{
				if (!model.UserId.HasValue)
				{
					throw new Exception("Неизвестный пользователь");
				}
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change, model.UserId.Value))
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
				if (!model.UserId.HasValue)
				{
					throw new Exception("Неизвестный пользователь");
				}
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete, model.UserId.Value))
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
