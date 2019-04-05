using AuthenticationImplementations;
using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.ViewModels;
using AuthenticationServiceInterfaces.Interfaces;
using DatabaseContext;
using Interfaces;
using Models.Enums;
using System;
using System.Linq;

namespace AuthenticationServiceImplementations.Implementations
{
    public class RoleService : IRoleService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Роли;

        private readonly string _entity = "Роли";

        public ResultService<RolePageViewModel> GetRoles(RoleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DepartmentRoles.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.RoleName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new RolePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AuthenticationModelFactoryToViewModel.CreateRoleViewModel).ToList()
                    };

                    return ResultService<RolePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<RolePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<RoleViewModel> GetRole(RoleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentRoles
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<RoleViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<RoleViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<RoleViewModel>.Success(AuthenticationModelFactoryToViewModel.CreateRoleViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<RoleViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateRole(RoleSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AuthenticationModelFacotryFromBindingModel.CreateRole(model);

                    var exsistEntity = context.DepartmentRoles.FirstOrDefault(x => x.RoleName == entity.RoleName);
                    if (exsistEntity == null)
                    {
                        context.DepartmentRoles.Add(entity);
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

		public ResultService UpdateRole(RoleSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentRoles.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = AuthenticationModelFacotryFromBindingModel.CreateRole(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteRole(RoleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentRoles.FirstOrDefault(x => x.Id == model.Id);
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