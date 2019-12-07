using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class IndividualPlanTitleService : IIndividualPlanTitleService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Индивидуальный_план;

        private readonly string _entity = "Индивидуальный план";

        public ResultService<IndividualPlanTitlePageViewModel> GetIndividualPlanTitles(IndividualPlanTitleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.IndividualPlanTitles.Where(x => !x.IsDeleted).AsQueryable();

                    if (!string.IsNullOrEmpty(model.Title))
                    {
                        query = query.Where(x => x.Title == model.Title);
                    }

                    query = query.OrderBy(x => x.Order);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new IndividualPlanTitlePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateIndividualPlanTitleViewModel).ToList()
                    };

                    return ResultService<IndividualPlanTitlePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanTitlePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanTitleViewModel> GetIndividualPlanTitle(IndividualPlanTitleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanTitles
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<IndividualPlanTitleViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<IndividualPlanTitleViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<IndividualPlanTitleViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateIndividualPlanTitleViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanTitleViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanTitle(IndividualPlanTitleSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanTitle(model);

                    var exsistEntity = context.IndividualPlanTitles.FirstOrDefault(x => x.Title == entity.Title);
                    if (exsistEntity == null)
                    {
                        context.IndividualPlanTitles.Add(entity);
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

        public ResultService UpdateIndividualPlanTitle(IndividualPlanTitleSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanTitles.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanTitle(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteIndividualPlanTitle(IndividualPlanTitleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanTitles.FirstOrDefault(x => x.Id == model.Id);
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