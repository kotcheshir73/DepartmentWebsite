using DatabaseContext;
using Enums;
using LaboratoryHeadImplementations;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using LaboratoryHeadInterfaces.ViewModels;
using System;
using System.Linq;
using Tools;

namespace DepartmentService.Services
{
    public class MaterialTechnicalValueGroupService : IMaterialTechnicalValueGroupService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.МатериальноТехническиеЦенности;

        private readonly string _entity = "Материально Технические Ценности";

        public ResultService<MaterialTechnicalValueGroupPageViewModel> GetMaterialTechnicalValueGroups(MaterialTechnicalValueGroupGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.MaterialTechnicalValueGroups.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.Order);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new MaterialTechnicalValueGroupPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LaboratoryHeadModelFactoryToViewModel.CreateMaterialTechnicalValueGroupViewModel).ToList()
                    };

                    return ResultService<MaterialTechnicalValueGroupPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueGroupPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MaterialTechnicalValueGroupViewModel> GetMaterialTechnicalValueGroup(MaterialTechnicalValueGroupGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValueGroups
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<MaterialTechnicalValueGroupViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<MaterialTechnicalValueGroupViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<MaterialTechnicalValueGroupViewModel>.Success(LaboratoryHeadModelFactoryToViewModel.CreateMaterialTechnicalValueGroupViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueGroupViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LaboratoryHeadModelFacotryFromBindingModel.CreateMaterialTechnicalValueGroup(model);

                    var exsistEntity = context.MaterialTechnicalValueGroups.FirstOrDefault(x => x.GroupName == entity.GroupName);
                    if (exsistEntity == null)
                    {
                        context.MaterialTechnicalValueGroups.Add(entity);
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

        public ResultService UpdateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValueGroups.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LaboratoryHeadModelFacotryFromBindingModel.CreateMaterialTechnicalValueGroup(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValueGroups.FirstOrDefault(x => x.Id == model.Id);
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