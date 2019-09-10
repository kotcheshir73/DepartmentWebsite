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
    public class SeasonDatesService : ISeasonDatesService
	{
        private readonly IAcademicYearService _serviceAY;

        private readonly AccessOperation _serviceOperation = AccessOperation.Даты_семестра;

        private readonly string _entity = "Даты семестра";

        public SeasonDatesService(IAcademicYearService serviceAY)
		{
            _serviceAY = serviceAY;
        }

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<SeasonDatesPageViewModel> GetSeasonDaties(SeasonDatesGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.SeasonDates.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    query = query.OrderBy(x => x.Id);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicYear);

                    var result = new SeasonDatesPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateSeasonDatesViewModel).ToList()
                    };

                    return ResultService<SeasonDatesPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<SeasonDatesPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<SeasonDatesViewModel> GetSeasonDates(SeasonDatesGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = string.IsNullOrEmpty(model.Title) ?
                                        context.SeasonDates.Include(x => x.AcademicYear).FirstOrDefault(x => x.Id == model.Id) :
                                        context.SeasonDates.Include(x => x.AcademicYear).FirstOrDefault(x => x.Title == model.Title);
                    if (entity == null)
                    {
                        return ResultService<SeasonDatesViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<SeasonDatesViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<SeasonDatesViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateSeasonDatesViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<SeasonDatesViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateSeasonDates(SeasonDatesSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateSeasonDates(model);

                    var exsistEntity = context.SeasonDates.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.Title == entity.Title);
                    if (exsistEntity == null)
                    {
                        context.SeasonDates.Add(entity);
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

		public ResultService UpdateSeasonDates(SeasonDatesSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SeasonDates.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateSeasonDates(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteSeasonDates(SeasonDatesGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SeasonDates.FirstOrDefault(x => x.Id == model.Id);
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