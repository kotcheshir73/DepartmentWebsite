using AcademicYearImplementations;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tools;

namespace DepartmentService.Services
{
    public class TimeNormService : ITimeNormService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Нормы_времени;

        private readonly string _entity = "Нормы времени";

        private readonly IAcademicYearService _serviceAY;

        private readonly IDisciplineBlockService _serviceDB;

        public TimeNormService(IAcademicYearService serviceAY, IDisciplineBlockService serviceDB)
		{
            _serviceAY = serviceAY;
            _serviceDB = serviceDB;
        }
        
        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model)
        {
            return _serviceDB.GetDisciplineBlocks(model);
        }

        public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.TimeNorms.Where(x => !x.IsDeleted).AsQueryable();

                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }

                    if (model.DisciplineBlockId.HasValue)
                    {
                        query = query.Where(x => x.DisciplineBlockId == model.DisciplineBlockId);
                    }

                    if (model.AcademicPlanRecordId.HasValue)
                    {
                        var apr = context.AcademicPlanRecords.Include(x => x.AcademicPlan).FirstOrDefault(x => x.Id == model.AcademicPlanRecordId);
                        query = query.Where(x => x.AcademicYearId == apr.AcademicPlan.AcademicYearId);
                    }

                    query = query.OrderBy(x => x.TimeNormOrder).ThenBy(x => x.TimeNormName);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    query = query.Include(x => x.AcademicYear).Include(x => x.DisciplineBlock);

                    var result = new TimeNormPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(AcademicYearModelFactoryToViewModel.CreateTimeNormViewModel).ToList()
                    };

                    return ResultService<TimeNormPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<TimeNormPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<TimeNormViewModel> GetTimeNorm(TimeNormGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TimeNorms
                                .Include(x => x.AcademicYear)
                                .Include(x => x.DisciplineBlock)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<TimeNormViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<TimeNormViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<TimeNormViewModel>.Success(AcademicYearModelFactoryToViewModel.CreateTimeNormViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<TimeNormViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateTimeNorm(TimeNormSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = AcademicYearModelFacotryFromBindingModel.CreateTimeNorm(model);

                    var exsistEntity = context.TimeNorms.FirstOrDefault(x => x.AcademicYearId == entity.AcademicYearId && x.TimeNormName == entity.TimeNormName);
                    if (exsistEntity == null)
                    {
                        context.TimeNorms.Add(entity);
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

		public ResultService UpdateTimeNorm(TimeNormSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TimeNorms.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = AcademicYearModelFacotryFromBindingModel.CreateTimeNorm(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteTimeNorm(TimeNormGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.TimeNorms.FirstOrDefault(x => x.Id == model.Id);
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