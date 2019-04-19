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
    public class SoftwareService : ISoftwareService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.УстановленоеПО;

        private readonly string _entity = "Установленое ПО";

        public ResultService<SoftwarePageViewModel> GetSoftwares(SoftwareGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;

                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Softwares.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.SoftwareName).ThenBy(x => x.SoftwareKey);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new SoftwarePageViewModel
                    {
                        MaxCount = countPages,
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

        public ResultService<SoftwareViewModel> GetSoftware(SoftwareGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Softwares
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<SoftwareViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<SoftwareViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<SoftwareViewModel>.Success(LaboratoryHeadModelFactoryToViewModel.CreateSoftwareViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<SoftwareViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateSoftware(SoftwareSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = LaboratoryHeadModelFacotryFromBindingModel.CreateSoftware(model);

                    var exsistEntity = context.Softwares.FirstOrDefault(x => x.SoftwareName == entity.SoftwareName && x.SoftwareKey == model.SoftwareKey);
                    if (exsistEntity == null)
                    {
                        context.Softwares.Add(entity);
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

        public ResultService UpdateSoftware(SoftwareSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Softwares.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = LaboratoryHeadModelFacotryFromBindingModel.CreateSoftware(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteSoftware(SoftwareSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.Softwares.FirstOrDefault(x => x.Id == model.Id);
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