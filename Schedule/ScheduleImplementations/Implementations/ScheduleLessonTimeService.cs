using Enums;
using ScheduleImplementations;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using System;
using System.Linq;
using Tools;

namespace ScheduleServiceImplementations.Services
{
    public class ScheduleLessonTimeService : IScheduleLessonTimeService
	{
        private readonly AccessOperation _serviceOperation = AccessOperation.Расписание_интервалы_пар;

        private readonly string _entity = "Расписание интервалы пар";

        public ResultService<ScheduleLessonTimePageViewModel> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.ScheduleLessonTimes.Where(x => !x.IsDeleted).AsQueryable();

                    if (!string.IsNullOrEmpty(model.Title))
                    {
                        query = query.Where(x => x.Title.Contains(model.Title));
                    }

                    query = query.OrderBy(x => x.Order).ThenBy(x => x.Id);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new ScheduleLessonTimePageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ScheduleModelFactoryToViewModel.CreateScheduleLessonTimeViewModel).ToList()
                    };

                    return ResultService<ScheduleLessonTimePageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<ScheduleLessonTimePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<ScheduleLessonTimeViewModel> GetScheduleLessonTime(ScheduleLessonTimeGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = string.IsNullOrEmpty(model.Title) ? context.ScheduleLessonTimes.FirstOrDefault(x => x.Id == model.Id) :
                                                                context.ScheduleLessonTimes.FirstOrDefault(x => x.Title == model.Title);
                    if (entity == null)
                    {
                        return ResultService<ScheduleLessonTimeViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<ScheduleLessonTimeViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<ScheduleLessonTimeViewModel>.Success(ScheduleModelFactoryToViewModel.CreateScheduleLessonTimeViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ScheduleLessonTimeViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateScheduleLessonTime(ScheduleLessonTimeSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ScheduleModelFacotryFromBindingModel.CreateScheduleLessonTime(model);

                    var exsistEntity = context.ScheduleLessonTimes.FirstOrDefault(x => x.Title == entity.Title);
                    if (exsistEntity == null)
                    {
                        context.ScheduleLessonTimes.Add(entity);
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

		public ResultService UpdateScheduleLessonTime(ScheduleLessonTimeSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ScheduleLessonTimes.FirstOrDefault(e => e.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ScheduleModelFacotryFromBindingModel.CreateScheduleLessonTime(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteScheduleLessonTime(ScheduleLessonTimeGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ScheduleLessonTimes.FirstOrDefault(e => e.Id == model.Id);
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