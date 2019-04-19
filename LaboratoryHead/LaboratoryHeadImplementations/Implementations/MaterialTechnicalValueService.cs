using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using LaboratoryHeadImplementations;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.IServices;
using LaboratoryHeadInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace DepartmentService.Services
{
    public class MaterialTechnicalValueService : IMaterialTechnicalValueService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.МатериальноТехническиеЦенности;

        private readonly string _entity = "Материально Технические Ценности";

        private readonly IClassroomService _serviceC;

        public MaterialTechnicalValueService(IClassroomService serviceC)
        {
            _serviceC = serviceC;
        }

        public ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model)
        {
            return _serviceC.GetClassrooms(model);
        }


        public ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.MaterialTechnicalValues.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.ClassroomId.HasValue)
                    {
                        query = query.Where(x => x.ClassroomId == model.ClassroomId);
                    }

                    if (!string.IsNullOrEmpty(model.InventoryNumber))
                    {
                        query = query.Where(x => x.InventoryNumber.Contains(model.InventoryNumber));
                    }

                    query = query.OrderBy(x => x.Classroom.Number).ThenBy(x => x.InventoryNumber).ThenBy(x => x.DateCreate);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.Classroom);

                    var result = new MaterialTechnicalValuePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LaboratoryHeadModelFactoryToViewModel.CreateMaterialTechnicalValueViewModel).ToList()
                    };

                    return ResultService<MaterialTechnicalValuePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValuePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MaterialTechnicalValueViewModel> GetMaterialTechnicalValue(MaterialTechnicalValueGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValues
                                .Include(ap => ap.Classroom)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<MaterialTechnicalValueViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<MaterialTechnicalValueViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<MaterialTechnicalValueViewModel>.Success(LaboratoryHeadModelFactoryToViewModel.CreateMaterialTechnicalValueViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LaboratoryHeadModelFacotryFromBindingModel.CreateMaterialTechnicalValue(model);

                    var exsistEntity = context.MaterialTechnicalValues.FirstOrDefault(x => x.InventoryNumber == entity.InventoryNumber);
                    if (exsistEntity == null)
                    {
                        context.MaterialTechnicalValues.Add(entity);
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

        public ResultService UpdateMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValues.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LaboratoryHeadModelFacotryFromBindingModel.CreateMaterialTechnicalValue(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValues.FirstOrDefault(x => x.Id == model.Id);
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
                    entity.DeleteReason = model.DeleteReason;

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