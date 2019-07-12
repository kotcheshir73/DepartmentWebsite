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
    public class SoftwareRecordService : ISoftwareRecordService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.УстановленоеПО;

        private readonly string _entity = "Установленое ПО";

        private readonly IMaterialTechnicalValueService _serviceM;

        private readonly ISoftwareService _serviceS;

        public SoftwareRecordService(IMaterialTechnicalValueService serviceM, ISoftwareService serviceS)
        {
            _serviceM = serviceM;
            _serviceS = serviceS;
        }

        public ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model)
        {
            return _serviceM.GetMaterialTechnicalValues(model);
        }

        public ResultService<SoftwarePageViewModel> GetSoftwares(SoftwareGetBindingModel model)
        {
            return _serviceS.GetSoftwares(model);
        }


        public ResultService<SoftwareRecordPageViewModel> GetSoftwareRecords(SoftwareRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;

                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.SoftwareRecords.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.MaterialTechnicalValueId.HasValue)
                    {
                        query = query.Where(x => x.MaterialTechnicalValueId == model.MaterialTechnicalValueId);
                    }

                    if (model.SoftwareId.HasValue)
                    {
                        query = query.Where(x => x.SoftwareId == model.SoftwareId);
                    }

                    query = query.OrderBy(x => x.MaterialTechnicalValue.InventoryNumber).ThenBy(x => x.Software.SoftwareName).ThenBy(x => x.DateCreate);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.MaterialTechnicalValue).Include(x => x.Software);

                    var result = new SoftwareRecordPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(LaboratoryHeadModelFactoryToViewModel.CreateSoftwareRecordViewModel).ToList()
                    };

                    return ResultService<SoftwareRecordPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<SoftwareRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<SoftwareRecordViewModel> GetSoftwareRecord(SoftwareRecordGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SoftwareRecords
                                .Include(x => x.MaterialTechnicalValue)
                                .Include(x => x.Software)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<SoftwareRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<SoftwareRecordViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<SoftwareRecordViewModel>.Success(LaboratoryHeadModelFactoryToViewModel.CreateSoftwareRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<SoftwareRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateSoftwareRecord(SoftwareRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LaboratoryHeadModelFacotryFromBindingModel.CreateSoftwareRecord(model);

                    var exsistEntity = context.SoftwareRecords.FirstOrDefault(x => x.SoftwareId == entity.SoftwareId && x.MaterialTechnicalValueId == model.MaterialTechnicalValueId);
                    if (exsistEntity == null)
                    {
                        context.SoftwareRecords.Add(entity);
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

        public ResultService UpdateSoftwareRecord(SoftwareRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SoftwareRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LaboratoryHeadModelFacotryFromBindingModel.CreateSoftwareRecord(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteSoftwareRecord(SoftwareRecordSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SoftwareRecords.FirstOrDefault(x => x.Id == model.Id);
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