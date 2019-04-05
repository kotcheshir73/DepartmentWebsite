using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.ViewModels;
using AuthenticationServiceImplementations;
using AuthenticationServiceInterfaces.Interfaces;
using DatabaseContext;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using System;
using System.Linq;

namespace AuthenticationImplementations.Implementations
{
    public class AccessService : IAccessService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Доступы;

        private readonly string _entity = "Доступы";

		public ResultService<AccessPageViewModel> GetAccesses(AccessGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.DepartmentAccesses.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.RoleId.HasValue)
                    {
                        query = query.Where(x => x.RoleId == model.RoleId);
                    }

                    query = query.OrderBy(x => x.Operation).ThenBy(x => x.AccessType);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.Role);

                    var result = new AccessPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AuthenticationModelFactoryToViewModel.CreateAccessViewModel).ToList()
                    };

                    return ResultService<AccessPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<AccessPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<AccessViewModel> GetAccess(AccessGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = model.Id.HasValue ?
                                context.DepartmentAccesses.FirstOrDefault(x => x.Id == model.Id.Value)
                                :
                            model.RoleId.HasValue && !string.IsNullOrEmpty(model.Operation) ?
                                context.DepartmentAccesses.FirstOrDefault(x => x.RoleId == model.RoleId.Value && x.Operation.ToString() == model.Operation)
                                :
                                null;
                    if (entity == null)
                    {
                        return ResultService<AccessViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<AccessViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<AccessViewModel>.Success(AuthenticationModelFactoryToViewModel.CreateAccessViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<AccessViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateAccess(AccessSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AuthenticationModelFacotryFromBindingModel.CreateAccess(model);

                    var exsistEntity = context.DepartmentAccesses.FirstOrDefault(x => x.Operation == entity.Operation && x.RoleId == entity.RoleId && x.AccessType == entity.AccessType);
                    if (exsistEntity == null)
                    {
                        context.DepartmentAccesses.Add(entity);
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

		public ResultService UpdateAccess(AccessSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentAccesses.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }
                    entity = AuthenticationModelFacotryFromBindingModel.CreateAccess(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteAccess(AccessGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.DepartmentAccesses.FirstOrDefault(x => x.Id == model.Id);
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