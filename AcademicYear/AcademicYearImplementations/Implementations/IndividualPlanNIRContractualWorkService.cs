using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class IndividualPlanNIRContractualWorkService : IIndividualPlanNIRContractualWorkService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Индивидуальный_план;

        private readonly string _entity = "Индивидуальный план";

        private readonly IIndividualPlanService _serviceIP;

        public IndividualPlanNIRContractualWorkService(IIndividualPlanService serviceIP)
        {
            _serviceIP = serviceIP;
        }

        public ResultService<IndividualPlanPageViewModel> GetIndividualPlans(IndividualPlanGetBindingModel model)
        {
            return _serviceIP.GetIndividualPlans(model);
        }

        public ResultService<IndividualPlanNIRContractualWorkPageViewModel> GetIndividualPlanNIRContractualWorks(IndividualPlanNIRContractualWorkGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.IndividualPlanNIRContractualWorks.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.IndividualPlanId.HasValue)
                    {
                        query = query.Where(x => x.IndividualPlanId == model.IndividualPlanId);
                    }

                    query = query.OrderBy(x => x.JobContent);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.IndividualPlan);

                    var result = new IndividualPlanNIRContractualWorkPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateIndividualPlanNIRContractualWorkViewModel).ToList()
                    };

                    return ResultService<IndividualPlanNIRContractualWorkPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRContractualWorkPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanNIRContractualWorkViewModel> GetIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanNIRContractualWorks
                                .Include(x => x.IndividualPlan)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<IndividualPlanNIRContractualWorkViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<IndividualPlanNIRContractualWorkViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<IndividualPlanNIRContractualWorkViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateIndividualPlanNIRContractualWorkViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRContractualWorkViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanNIRContractualWork(model);

                    var exsistEntity = context.IndividualPlanNIRContractualWorks.FirstOrDefault(x => x.IndividualPlanId == entity.IndividualPlanId &&
                            x.JobContent == entity.JobContent);
                    if (exsistEntity == null)
                    {
                        context.IndividualPlanNIRContractualWorks.Add(entity);
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

        public ResultService UpdateIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanNIRContractualWorks.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanNIRContractualWork(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteIndividualPlanNIRContractualWork(IndividualPlanNIRContractualWorkGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanNIRContractualWorks.FirstOrDefault(x => x.Id == model.Id);
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