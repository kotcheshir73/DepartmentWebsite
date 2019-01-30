using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Context;
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
				var query = _context.Users.Where(u => !u.IsDeleted).AsQueryable();

                // TODO
                //if (!string.IsNullOrEmpty(model.RoleType))
                //{
                //    var roleType = (RoleType)Enum.Parse(typeof(RoleType), model.RoleType);
                //    query = query.Where(u => u.RoleType == roleType);
                //}
				if (model.IsBanned.HasValue)
				{
					query = query.Where(u => u.LockoutEnabled == model.IsBanned.Value);
                }

                query = query.OrderBy(u => u.UserName).ThenBy(u => u.DateLastVisit);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new UserPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateUserViewModel).ToList()
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

		public ResultService CreateUser(UserSetBindingModel model)
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

		public ResultService UpdateUser(UserSetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по пользователям");
				}

				var entity = _context.Users.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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

				var entity = _context.Users.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
