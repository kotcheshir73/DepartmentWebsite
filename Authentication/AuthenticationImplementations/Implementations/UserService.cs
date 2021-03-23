using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using AuthenticationInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using System;
using System.Linq;
using Tools;

namespace AuthenticationImplementations.Implementations
{
    public class UserService : IUserService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Пользователи;

        private readonly string _entity = "Пользователи";

        private readonly IRoleService _serviceR;

		private readonly IStudentService _serviceS;

		private readonly ILecturerService _serviceL;

		public UserService(IRoleService serviceR, IStudentService serviceS, ILecturerService serviceL)
		{
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
                if (!DepartmentUserManager.CheckAccess(model, _serviceOperation, AccessType.View, _entity))
                {
                    return ResultService<UserPageViewModel>.Error(new MethodAccessException(DepartmentUserManager.ErrorMessage), ResultServiceStatusCode.Error);
                }

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DepartmentUsers.Where(x => !x.IsDeleted).AsQueryable();

                    // TODO
                    //if (!string.IsNullOrEmpty(model.RoleType))
                    //{
                    //    var roleType = (RoleType)Enum.Parse(typeof(RoleType), model.RoleType);
                    //    query = query.Where(x => x.RoleType == roleType);
                    //}
                    if (model.IsBanned.HasValue)
                    {
                        query = query.Where(x => x.IsLocked == model.IsBanned.Value);
                    }
                    if(model.LecturerIds != null)
                    {
                        query = query.Where(x => x.LecturerId.HasValue && model.LecturerIds.Contains(x.LecturerId.Value));
                    }

                    query = query.OrderBy(x => x.UserName).ThenBy(x => x.DateLastVisit);

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
                        List = query.Select(AuthenticationModelFactoryToViewModel.CreateUserViewModel).ToList()
                    };

                    return ResultService<UserPageViewModel>.Success(result);
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentUsers
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<UserViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<UserViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<UserViewModel>.Success(AuthenticationModelFactoryToViewModel.CreateUserViewModel(entity));
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AuthenticationModelFacotryFromBindingModel.CreateUser(model);

                    var exsistEntity = context.DepartmentUsers.FirstOrDefault(x => x.UserName == entity.UserName);
                    if (exsistEntity == null)
                    {
                        context.DepartmentUsers.Add(entity);
                        context.SaveChanges();
                        return ResultService.Success(entity.Id);
                    }
                    else
                    {
                        if (exsistEntity.IsDeleted)
                        {
                            exsistEntity.IsDeleted = false;
                            context.SaveChanges();
                            return ResultService.Success(exsistEntity.Id);
                        }
                        else
                        {
                            return ResultService.Error("Error:", "Элемент уже существует", ResultServiceStatusCode.ExsistItem);
                        }
                    }
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentUsers.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = AuthenticationModelFacotryFromBindingModel.CreateUser(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentUsers.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity.IsDeleted = true;
                    entity.DateDelete = DateTime.Now;

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}
	}
}