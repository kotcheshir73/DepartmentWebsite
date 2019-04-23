using Enums;
using LaboratoryHeadImplementations;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using LaboratoryHeadInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace DepartmentService.Services
{
    public class MaterialTechnicalValueRecordService : IMaterialTechnicalValueRecordService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.МатериальноТехническиеЦенности;

        private readonly string _entity = "Материально Технические Ценности";

        private readonly IMaterialTechnicalValueService _serviceMTV;

        private readonly IMaterialTechnicalValueGroupService _serviceMTVG;

        public MaterialTechnicalValueRecordService(IMaterialTechnicalValueService serviceMTV, IMaterialTechnicalValueGroupService serviceMTVG)
        {
            _serviceMTV = serviceMTV;
            _serviceMTVG = serviceMTVG;
        }


        public ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model)
        {
            return _serviceMTV.GetMaterialTechnicalValues(model);
        }

        public ResultService<MaterialTechnicalValueGroupPageViewModel> GetMaterialTechnicalValueGroups(MaterialTechnicalValueGroupGetBindingModel model)
        {
            return _serviceMTVG.GetMaterialTechnicalValueGroups(model);
        }


        public ResultService<MaterialTechnicalValueRecordPageViewModel> GetMaterialTechnicalValueRecords(MaterialTechnicalValueRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.MaterialTechnicalValueRecords.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.MaterialTechnicalValueId.HasValue)
                    {
                        query = query.Where(x => x.MaterialTechnicalValueId == model.MaterialTechnicalValueId);
                    }

                    if (model.MaterialTechnicalValueGroupId.HasValue)
                    {
                        query = query.Where(x => x.MaterialTechnicalValueGroupId == model.MaterialTechnicalValueGroupId);
                    }

                    query = query.OrderBy(x => x.MaterialTechnicalValueGroup.Order).ThenBy(x => x.Order);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.MaterialTechnicalValue).Include(x => x.MaterialTechnicalValueGroup);

                    var result = new MaterialTechnicalValueRecordPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LaboratoryHeadModelFactoryToViewModel.CreateMaterialTechnicalValueRecordViewModel).ToList()
                    };

                    return ResultService<MaterialTechnicalValueRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MaterialTechnicalValueRecordViewModel> GetMaterialTechnicalValueRecord(MaterialTechnicalValueRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValueRecords
                                .Include(x => x.MaterialTechnicalValue)
                                .Include(x => x.MaterialTechnicalValueGroup)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<MaterialTechnicalValueRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<MaterialTechnicalValueRecordViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<MaterialTechnicalValueRecordViewModel>.Success(LaboratoryHeadModelFactoryToViewModel.CreateMaterialTechnicalValueRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<MaterialTechnicalValueRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LaboratoryHeadModelFacotryFromBindingModel.CreateMaterialTechnicalValueRecord(model);

                    var exsistEntity = context.MaterialTechnicalValueRecords.FirstOrDefault(x => x.FieldName == entity.FieldName && x.MaterialTechnicalValueId == model.MaterialTechnicalValueId);
                    if (exsistEntity == null)
                    {
                        context.MaterialTechnicalValueRecords.Add(entity);
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

        public ResultService UpdateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValueRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LaboratoryHeadModelFacotryFromBindingModel.CreateMaterialTechnicalValueRecord(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValueRecords.FirstOrDefault(x => x.Id == model.Id);
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