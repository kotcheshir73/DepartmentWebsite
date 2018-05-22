using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class ScheduleLessonTimeService : IScheduleLessonTimeService
	{
		private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Расписание_интервалы_пар;

        public ScheduleLessonTimeService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<ScheduleLessonTimePageViewModel> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по расписанию пар");
                }

                int countPages = 0;
                var query = _context.ScheduleLessonTimes.Where(slt => !slt.IsDeleted).AsQueryable();

                if (!string.IsNullOrEmpty(model.Title))
                {
                    query = query.Where(slt => slt.Title.Contains(model.Title));
                }

                query = query.OrderBy(slt => slt.Order).ThenBy(slt => slt.Id);

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
                    List = query.Select(ModelFactoryToViewModel.CreateScheduleLessonTimeViewModel).ToList()
                };

                return ResultService<ScheduleLessonTimePageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ScheduleLessonTimePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService< ScheduleLessonTimePageViewModel >.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ScheduleLessonTimeViewModel> GetScheduleLessonTime(ScheduleLessonTimeGetBindingModel model)
		{
			try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по расписанию пар");
                }

                var entity = string.IsNullOrEmpty(model.Title) ? _context.ScheduleLessonTimes.FirstOrDefault(slt => slt.Id == model.Id && !slt.IsDeleted) :
																_context.ScheduleLessonTimes.FirstOrDefault(slt => slt.Title == model.Title && !slt.IsDeleted);
                if (entity == null)
                {
                    return ResultService<ScheduleLessonTimeViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

				return ResultService<ScheduleLessonTimeViewModel>.Success(ModelFactoryToViewModel.CreateScheduleLessonTimeViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ScheduleLessonTimeViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по расписанию пар");
                }

                var entity = ModelFacotryFromBindingModel.CreateScheduleLessonTime(model);

                _context.ScheduleLessonTimes.Add(entity);
				_context.SaveChanges();

				return ResultService.Success(entity.Id);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по расписанию пар");
                }

                var entity = _context.ScheduleLessonTimes.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateScheduleLessonTime(model, entity);
				
				_context.SaveChanges();

				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
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
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по расписанию пар");
                }

                var entity = _context.ScheduleLessonTimes.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity.IsDeleted = true;
                entity.DateDelete = DateTime.Now;
                
				_context.SaveChanges();

				return ResultService.Success();
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService.Error(ex, ResultServiceStatusCode.Error);
			}
		}
	}
}
