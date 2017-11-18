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
	public class UserService : IUserService
	{
		private readonly DepartmentDbContext _context;

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


		public ResultService<List<RoleViewModel>> GetRoles()
		{
			return _serviceR.GetRoles();
		}

		public ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model)
		{
			return _serviceS.GetStudents(model);
		}

		public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
		{
			return _serviceL.GetLecturers(model);
		}


		public ResultService<List<UserViewModel>> GetUsers(UserGetBindingModel model)
		{
			try
			{
				var result = _context.Users.Include(u => u.Role).Where(u => !u.IsDeleted);
				if (model.RoleId.HasValue)
				{
					result = result.Where(u => u.RoleId == model.RoleId.Value);
				}
				if (model.IsBanned.HasValue)
				{
					result = result.Where(u => u.IsBanned == model.IsBanned.Value);
				}
				// TODO skip&take сделать везде
				if (model.Skip.HasValue)
				{
					result = result.Skip(model.Skip.Value);
				}
				if (model.Take.HasValue)
				{
					result = result.Take(model.Take.Value);
				}
				return ResultService<List<UserViewModel>>.Success(ModelFactoryToViewModel.CreateUsers(result).ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<UserViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<UserViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<UserViewModel> GetUser(UserGetBindingModel model)
		{
			try
			{
				var entity = _context.Users
								.FirstOrDefault(u => u.Id == model.Id && !u.IsDeleted);
				if (entity == null)
				{
					return ResultService<UserViewModel>.Error("Error:", "Entity not found",
						  ResultServiceStatusCode.NotFound);
				}

				return ResultService<UserViewModel>.Success(
					ModelFactoryToViewModel.CreateUserViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<UserViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<UserViewModel>.Error(ex, 
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateUser(UserRecordBindingModel model)
		{
			var entity = ModelFacotryFromBindingModel.CreateUser(model);
			try
			{
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
				var entity = _context.Users
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
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
				var entity = _context.Users
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
