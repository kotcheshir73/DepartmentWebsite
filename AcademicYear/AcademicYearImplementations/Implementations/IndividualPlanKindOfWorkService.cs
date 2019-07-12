using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Implementations
{
    public class IndividualPlanKindOfWorkService : IIndividualPlanKindOfWorkService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Индивидуальный_план;

        private readonly string _entity = "Индивидуальный план";

        private readonly IIndividualPlanTitleService _serviceIPT;

        public IndividualPlanKindOfWorkService(IIndividualPlanTitleService serviceIPT)
        {
            _serviceIPT = serviceIPT;
        }

        public ResultService<IndividualPlanTitlePageViewModel> GetIndividualPlanTitles(IndividualPlanTitleGetBindingModel model)
        {
            return _serviceIPT.GetIndividualPlanTitles(model);
        }

        public ResultService<IndividualPlanKindOfWorkPageViewModel> GetIndividualPlanKindOfWorks(IndividualPlanKindOfWorkGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.IndividualPlanKindOfWorks.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.IndividualPlanTitleId.HasValue)
                    {
                        query = query.Where(x => x.IndividualPlanTitleId == model.IndividualPlanTitleId);
                    }
                    if (!string.IsNullOrEmpty(model.Name))
                    {
                        query = query.Where(x => x.Name == model.Name);
                    }

                    query = query.OrderBy(x => x.Order);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.IndividualPlanTitle);

                    var result = new IndividualPlanKindOfWorkPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateIndividualPlanKindOfWorkViewModel).ToList()
                    };

                    return ResultService<IndividualPlanKindOfWorkPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanKindOfWorkPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<IndividualPlanKindOfWorkViewModel> GetIndividualPlanKindOfWork(IndividualPlanKindOfWorkGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanKindOfWorks
                                .Include(x => x.IndividualPlanTitle)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<IndividualPlanKindOfWorkViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<IndividualPlanKindOfWorkViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<IndividualPlanKindOfWorkViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateIndividualPlanKindOfWorkViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<IndividualPlanKindOfWorkViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateIndividualPlanKindOfWork(IndividualPlanKindOfWorkSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanKindOfWork(model);

                    var exsistEntity = context.IndividualPlanKindOfWorks.FirstOrDefault(x => x.IndividualPlanTitleId == entity.IndividualPlanTitleId &&
                            x.Name == entity.Name);
                    if (exsistEntity == null)
                    {
                        context.IndividualPlanKindOfWorks.Add(entity);
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

        public ResultService UpdateIndividualPlanKindOfWork(IndividualPlanKindOfWorkSetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanKindOfWorks.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateIndividualPlanKindOfWork(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService DeleteIndividualPlanKindOfWork(IndividualPlanKindOfWorkGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.IndividualPlanKindOfWorks.FirstOrDefault(x => x.Id == model.Id);
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