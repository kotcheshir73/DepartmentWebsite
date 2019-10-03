using DatabaseContext;
using Enums;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using System;
using System.Linq;
using Tools;

namespace ScheduleImplementations.Services
{
    public class StreamingLessonService : IStreamingLessonService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

        private readonly string _entity = "Расписание";

        public ResultService<StreamingLessonPageViewModel> GetStreamingLessons(StreamingLessonGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                int countPages = 0;
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.StreamingLessons.Where(x => !x.IsDeleted).AsQueryable();

                    query = query.OrderBy(x => x.Id);

                    if (model.PageNumber.HasValue && model.PageSize.HasValue)
                    {
                        countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
                        query = query
                                    .Skip(model.PageSize.Value * model.PageNumber.Value)
                                    .Take(model.PageSize.Value);
                    }

                    var result = new StreamingLessonPageViewModel
                    {
                        MaxCount = countPages,
                        List = query.Select(ScheduleModelFactoryToViewModel.CreateStreamingLessonViewModel).ToList()
                    };

                    return ResultService<StreamingLessonPageViewModel>.Success(result);
                }
            }
            catch (Exception ex)
            {
                return ResultService<StreamingLessonPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<StreamingLessonViewModel> GetStreamingLesson(StreamingLessonGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = string.IsNullOrEmpty(model.IncomingGroups) ? context.StreamingLessons.FirstOrDefault(x => x.Id == model.Id) :
                                                                                context.StreamingLessons.FirstOrDefault(x => x.IncomingGroups == model.IncomingGroups);
                    if (entity == null)
                    {
                        return ResultService<StreamingLessonViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService<StreamingLessonViewModel>.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    return ResultService<StreamingLessonViewModel>.Success(ScheduleModelFactoryToViewModel.CreateStreamingLessonViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<StreamingLessonViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateStreamingLesson(StreamingLessonSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = ScheduleModelFacotryFromBindingModel.CreateStreamingLesson(model);

                    var exsistEntity = context.StreamingLessons.FirstOrDefault(x => x.StreamName == entity.StreamName && x.IncomingGroups == model.IncomingGroups);
                    if (exsistEntity == null)
                    {
                        context.StreamingLessons.Add(entity);
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

		public ResultService UpdateStreamingLesson(StreamingLessonSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamingLessons.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    else if (entity.IsDeleted)
                    {
                        return ResultService.Error("Error:", "Элемент был удален", ResultServiceStatusCode.WasDelete);
                    }

                    entity = ScheduleModelFacotryFromBindingModel.CreateStreamingLesson(model, entity);

                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteStreamingLesson(StreamingLessonGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.StreamingLessons.FirstOrDefault(x => x.Id == model.Id);
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