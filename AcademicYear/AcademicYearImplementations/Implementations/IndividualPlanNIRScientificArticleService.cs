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
    public class IndividualPlanNIRScientificArticleService : IIndividualPlanNIRScientificArticleService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Индивидуальный_план;

        private readonly string _entity = "Индивидуальный план";

        private readonly IIndividualPlanService _serviceIP;

        public IndividualPlanNIRScientificArticleService(IIndividualPlanService serviceIP)
        {
            _serviceIP = serviceIP;
        }

        public ResultService<IndividualPlanPageViewModel> GetIndividualPlans(IndividualPlanGetBindingModel model)
        {
            return _serviceIP.GetIndividualPlans(model);
        }

        public ResultService<IndividualPlanNIRScientificArticlePageViewModel> GetIndividualPlanNIRScientificArticles(IndividualPlanNIRScientificArticleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.IndividualPlanNIRScientificArticles.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.IndividualPlanId.HasValue)
                    {
                        query = query.Where(x => x.IndividualPlanId == model.IndividualPlanId);
                    }
                    if (model.Status != null)
                    {
                        query = query.Where(x => x.Status.Equals(model.Status));
                    }

                    query = query.OrderBy(apre => apre.Name);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.IndividualPlan); // для вложенных запросов

                    var result = new IndividualPlanNIRScientificArticlePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateIndividualPlanNIRScientificArticleViewModel).ToList()
                    };

                    return ResultService<IndividualPlanNIRScientificArticlePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRScientificArticlePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanNIRScientificArticleViewModel> GetIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanNIRScientificArticles
                                .Include(x => x.IndividualPlan)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<IndividualPlanNIRScientificArticleViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<IndividualPlanNIRScientificArticleViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<IndividualPlanNIRScientificArticleViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateIndividualPlanNIRScientificArticleViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanNIRScientificArticleViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanNIRScientificArticle(model);

                    var exsistEntity = context.IndividualPlanNIRScientificArticles.FirstOrDefault(x => x.IndividualPlanId == entity.IndividualPlanId &&
                            x.Name == entity.Name);
                    if (exsistEntity == null)
                    {
                        context.IndividualPlanNIRScientificArticles.Add(entity);
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

        public ResultService UpdateIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanNIRScientificArticles.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanNIRScientificArticle(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteIndividualPlanNIRScientificArticle(IndividualPlanNIRScientificArticleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanNIRScientificArticles.FirstOrDefault(x => x.Id == model.Id);
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