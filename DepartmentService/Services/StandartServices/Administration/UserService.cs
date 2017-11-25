﻿using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class UserService : IUserService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Пользователи;

		private readonly IRoleService _serviceR;

		private readonly IStudentService _serviceS;

		private readonly ILecturerService _serviceL;

		public UserService(DepartmentDbContext context, IRoleService serviceR, IStudentService serviceS, ILecturerService serviceL)
		{
			_context = context;
			_serviceR = serviceR;
			_serviceS = serviceS;
			_serviceL = serviceL;
		}


		public ResultService<RolePageViewModel> GetRoles(RoleGetBindingModel model)
		{
			return _serviceR.GetRoles(model);
		}

		public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
		{
			return _serviceS.GetStudents(model);
		}

		public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
		{
			return _serviceL.GetLecturers(model);
		}


		public ResultService<UserPageViewModel> GetUsers(UserGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по пользователям");
				}

				int countPages = 0;
				var query = _context.Users.Where(u => !u.IsDeleted);
				if (model.RoleId.HasValue)
				{
					query = query.Where(u => u.RoleId == model.RoleId.Value);
				}
				if (model.IsBanned.HasValue)
				{
					query = query.Where(u => u.IsBanned == model.IsBanned.Value);
				}

				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(e => e.Role.RoleName).ThenBy(e => e.Login).ThenBy(e => e.DateLastVisit)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(u => u.Role);

				var result = new UserPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateUsers(query).ToList()
				};

				return ResultService<UserPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<UserPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<UserPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<UserViewModel> GetUser(UserGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по пользователям");
				}

				var entity = _context.Users
								.FirstOrDefault(u => u.Id == model.Id && !u.IsDeleted);
				if (entity == null)
				{
					return ResultService<UserViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<UserViewModel>.Success(ModelFactoryToViewModel.CreateUserViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<UserViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<UserViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateUser(UserRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по пользователям");
				}

				var entity = ModelFacotryFromBindingModel.CreateUser(model);

				_context.Users.Add(entity);
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

		public ResultService UpdateUser(UserRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по пользователям");
				}

				var entity = _context.Users
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateUser(model, entity);

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

		public ResultService DeleteUser(UserGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по пользователям");
				}

				var entity = _context.Users
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
