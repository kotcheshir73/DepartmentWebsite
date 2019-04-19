using Enums;
using LaboratoryHeadImplementations;
using LaboratoryHeadImplementations.Helpers;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using LaboratoryHeadInterfaces.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models.LaboratoryHead;
using System;
using System.Linq;
using Tools;

namespace DepartmentService.Services
{
    public class LaboratoryProcess : ILaboratoryProcess
    {
        public ResultService MakeClone(LaboratoryProcessMakeCloneBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.МатериальноТехническиеЦенности, AccessType.Change, "Материально Технические Ценности");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.MaterialTechnicalValues.FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }

                    var newEntity = new MaterialTechnicalValue
                    {
                        ClassroomId = entity.ClassroomId,
                        DateCreate = entity.DateCreate,
                        InventoryNumber = entity.InventoryNumber,
                        FullName = entity.FullName,
                        Description = entity.Description,
                        Location = entity.Location,
                        Cost = entity.Cost
                    };

                    context.MaterialTechnicalValues.Add(newEntity);
                    context.SaveChanges();
                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService ApplyMTVRecords(LaboratoryProcessApplyMTVRecordsBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.МатериальноТехническиеЦенности, AccessType.Change, "Материально Технические Ценности");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entities = context.MaterialTechnicalValueRecords.Where(x => x.MaterialTechnicalValueId == model.Id && !x.IsDeleted);
                    if (entities == null || entities.Count() == 0)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        for (int i = 0; i < model.ApllyIds.Count; ++i)
                        {
                            foreach (var entity in entities)
                            {
                                var newEntity = new MaterialTechnicalValueRecord
                                {
                                    MaterialTechnicalValueId = model.ApllyIds[i],
                                    MaterialTechnicalValueGroupId = entity.MaterialTechnicalValueGroupId,
                                    FieldName = entity.FieldName,
                                    FieldValue = entity.FieldValue,
                                    Order = entity.Order
                                };

                                context.MaterialTechnicalValueRecords.Add(newEntity);
                                context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService ApplyInfoByAnotherSoftwareReocrds(LaboratoryProcessApplyInfoByAnotherSoftwareReocrdsBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.УстановленоеПО, AccessType.Change, "Установленое ПО");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SoftwareRecords.FirstOrDefault(x => x.Id == model.Id && !x.IsDeleted);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }

                    var entityes = context.SoftwareRecords
                        .Where(x => x.SoftwareId == entity.SoftwareId && x.ClaimNumber == entity.ClaimNumber && x.Id != entity.Id)
                        .ToList();

                    foreach (var updEntity in entityes)
                    {
                        updEntity.SetupDescription = entity.SetupDescription;
                    }
                    // TODO обновление отображается только после перезапуска проекта
                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByClassrooms(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.УстановленоеПО, AccessType.Change, "Установленое ПО");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.SoftwareRecords.Where(x => !x.IsDeleted && x.MaterialTechnicalValue.ClassroomId == model.ClassroomId).Distinct();

                    query = query.OrderBy(x => x.Software.SoftwareName).ThenBy(x => x.DateCreate);

                    query = query.Include(x => x.MaterialTechnicalValue).Include(x => x.Software);

                    var result = query.ToList();

                    return ResultService<LaboratoryProcessSoftwareRecordPageViewModel>.Success(new LaboratoryProcessSoftwareRecordPageViewModel
                    {
                        ListFirst = result
                            .Select(x => new LaboratoryProcessSoftwareRecordsViewModels
                            {
                                DateSetup = x.DateCreate.Date,
                                SoftwareName = x.Software.SoftwareName,
                                SoftwareDescription = x.Software.SoftwareDescription,
                                SoftwareKey = x.Software.SoftwareKey,
                                ClaimNumber = x.ClaimNumber
                            })
                            .Distinct(new ComparerLaboratoryProcessSoftwareRecord())
                            .ToList(),

                        ListSecond = result
                            .Select(x => new LaboratoryProcessMaterialTechincalValuesViewModels
                            {
                                InventoryNumber = x.MaterialTechnicalValue.InventoryNumber
                            })
                            .OrderBy(x => x.InventoryNumber)
                            .Distinct()
                            .ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                return ResultService<LaboratoryProcessSoftwareRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByClaimNumber(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.УстановленоеПО, AccessType.View, "Установленое ПО");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.SoftwareRecords.Where(x => !x.IsDeleted && x.ClaimNumber == model.ClaimNumber).Distinct();

                    query = query.OrderBy(x => x.Software.SoftwareName).ThenBy(x => x.DateCreate);

                    query = query.Include(x => x.MaterialTechnicalValue).Include(x => x.Software);

                    var result = query.ToList();

                    return ResultService<LaboratoryProcessSoftwareRecordPageViewModel>.Success(new LaboratoryProcessSoftwareRecordPageViewModel
                    {
                        ListFirst = result
                            .Select(x => new LaboratoryProcessSoftwareRecordsViewModels
                            {
                                DateSetup = x.DateCreate.Date,
                                SoftwareName = x.Software.SoftwareName,
                                SoftwareDescription = x.Software.SoftwareDescription,
                                SoftwareKey = x.Software.SoftwareKey,
                                ClaimNumber = x.ClaimNumber
                            })
                            .Distinct(new ComparerLaboratoryProcessSoftwareRecord())
                            .ToList(),

                        ListSecond = result
                            .Select(x => new LaboratoryProcessMaterialTechincalValuesViewModels
                            {
                                InventoryNumber = x.MaterialTechnicalValue.InventoryNumber
                            })
                            .OrderBy(x => x.InventoryNumber)
                            .Distinct()
                            .ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                return ResultService<LaboratoryProcessSoftwareRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByInventoryNumber(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.УстановленоеПО, AccessType.View, "Установленое ПО");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.SoftwareRecords.Where(x => !x.IsDeleted && x.MaterialTechnicalValue.InventoryNumber == model.InventoryNumber).Distinct();

                    query = query.OrderBy(x => x.Software.SoftwareName).ThenBy(x => x.DateCreate);

                    query = query.Include(x => x.MaterialTechnicalValue).Include(x => x.Software);

                    var result = query.ToList();

                    return ResultService<LaboratoryProcessSoftwareRecordPageViewModel>.Success(new LaboratoryProcessSoftwareRecordPageViewModel
                    {
                        ListFirst = result
                            .Select(x => new LaboratoryProcessSoftwareRecordsViewModels
                            {
                                DateSetup = x.DateCreate.Date,
                                SoftwareName = x.Software.SoftwareName,
                                SoftwareDescription = x.Software.SoftwareDescription,
                                SoftwareKey = x.Software.SoftwareKey,
                                ClaimNumber = x.ClaimNumber
                            })
                            .Distinct(new ComparerLaboratoryProcessSoftwareRecord())
                            .ToList(),

                        ListSecond = result
                            .Select(x => new LaboratoryProcessMaterialTechincalValuesViewModels
                            {
                                InventoryNumber = x.MaterialTechnicalValue.InventoryNumber
                            })
                            .OrderBy(x => x.InventoryNumber)
                            .Distinct()
                            .ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                return ResultService<LaboratoryProcessSoftwareRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<SoftwarePageViewModel> GetSoftwareByInvNumbers(LaboratoryProcessGetSoftwareByInvNumbersBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.УстановленоеПО, AccessType.View, "Установленое ПО");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.SoftwareRecords.Include(x => x.MaterialTechnicalValue).Include(x => x.Software).Where(x => !x.IsDeleted &&
                                        model.InventoryNumbers.Contains(x.MaterialTechnicalValue.InventoryNumber)).Select(x => x.Software).Distinct();

                    query = query.OrderBy(x => x.SoftwareName).ThenBy(x => x.SoftwareKey);

                    var result = new SoftwarePageViewModel
                    {
                        MaxCount = 0,
                        List = query.Select(LaboratoryHeadModelFactoryToViewModel.CreateSoftwareViewModel).ToList()
                    };

                    return ResultService<SoftwarePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<SoftwarePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService InstallSoftware(LaboratoryProcessInstalSoftwareBindingModel model)
        {
            try
            {
                if (model.SoftwareNames.Count == 0)
                {
                    throw new Exception("Список устанавливаемого ПО пуст");
                }

                if (model.InventoryNumbers.Count == 0)
                {
                    throw new Exception("Список инвентарных номеров пуст");
                }

                DepartmentUserManager.CheckAccess(AccessOperation.УстановленоеПО, AccessType.View, "Установленое ПО");

                using (var context = DepartmentUserManager.GetContext)
                {
                    foreach (var soft in model.SoftwareNames)
                    {
                        var softNameAndKey = soft.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        var softName = softNameAndKey[0];
                        var softKey = softNameAndKey.Length > 1 ? softNameAndKey[1] : string.Empty;
                        var softWare = context.Softwares.FirstOrDefault(x => x.SoftwareName == softName && x.SoftwareKey == softKey && !x.IsDeleted);
                        if (softWare == null)
                        {
                            throw new Exception(string.Format("Не найдено ПО с названием {0} и ключем {1}", softName, softKey));
                        }
                        foreach (var invNumber in model.InventoryNumbers)
                        {
                            var mtv = context.MaterialTechnicalValues.FirstOrDefault(x => x.InventoryNumber == invNumber && !x.IsDeleted);
                            if (mtv == null)
                            {
                                throw new Exception(string.Format("Не найден МТЦ с инв. номером {0}", invNumber));
                            }

                            var entity = LaboratoryHeadModelFacotryFromBindingModel.CreateSoftwareRecord(new SoftwareRecordSetBindingModel
                            {
                                MaterialTechnicalValueId = mtv.Id,
                                SoftwareId = softWare.Id,
                                DateSetup = model.DateSetup,
                                SetupDescription = model.SetupDescription,
                                ClaimNumber = model.ClaimNumber
                            });

                            context.SoftwareRecords.Add(entity);
                            context.SaveChanges();
                        }
                    }

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UnInstallSoftware(LaboratoryProcessUnInstalSoftwareBindingModel model)
        {
            try
            {
                if (model.SoftwareNames.Count == 0)
                {
                    throw new Exception("Список устанавливаемого ПО пуст");
                }

                if (model.InventoryNumbers.Count == 0)
                {
                    throw new Exception("Список инвентарных номеров пуст");
                }

                DepartmentUserManager.CheckAccess(AccessOperation.УстановленоеПО, AccessType.View, "Установленое ПО");

                using (var context = DepartmentUserManager.GetContext)
                {
                    foreach (var soft in model.SoftwareNames)
                    {
                        var softNameAndKey = soft.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        var softName = softNameAndKey[0];
                        var softKey = softNameAndKey.Length > 1 ? softNameAndKey[1] : string.Empty;
                        var softWare = context.Softwares.FirstOrDefault(x => x.SoftwareName == softName && x.SoftwareKey == softKey && !x.IsDeleted);
                        if (softWare == null)
                        {
                            throw new Exception(string.Format("Не найдено ПО с названием {0} и ключем {1}", softName, softKey));
                        }
                        foreach (var invNumber in model.InventoryNumbers)
                        {
                            var mtv = context.MaterialTechnicalValues.FirstOrDefault(x => x.InventoryNumber == invNumber && !x.IsDeleted);
                            if (mtv == null)
                            {
                                throw new Exception(string.Format("Не найден МТЦ с инв. номером {0}", invNumber));
                            }

                            var entity = context.SoftwareRecords.FirstOrDefault(x => x.SoftwareId == softWare.Id && x.MaterialTechnicalValueId == mtv.Id);
                            if (entity != null)
                            {
                                entity.IsDeleted = true;
                                entity.DateDelete = model.DateDelete;
                                entity.SetupDescription = string.Format("Прична удаления: {0}", model.DeleteReason);

                                context.SaveChanges();

                            }
                        }
                    }

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