using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;

namespace DepartmentService.Services
{
	public class ScheduleLessonTimeService : IScheduleLessonTimeService
	{
		private readonly DepartmentDbContext _context;

		public ScheduleLessonTimeService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<ScheduleLessonTimeViewModel>> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model)
		{
			try
			{
				if (string.IsNullOrEmpty(model.Title))
				{
					return ResultService<List<ScheduleLessonTimeViewModel>>.Success(
						ModelFactoryToViewModel.CreateScheduleLessonTimes(_context.ScheduleLessonTimes)
						.ToList());
				}
				else
				{
					return ResultService<List<ScheduleLessonTimeViewModel>>.Success(
						ModelFactoryToViewModel.CreateScheduleLessonTimes(_context.ScheduleLessonTimes
							.Where(slt => slt.Title.Contains(model.Title)))
						.ToList());
				}
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ScheduleLessonTimeViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ScheduleLessonTimeViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ScheduleLessonTimeViewModel> GetScheduleLessonTime(ScheduleLessonTimeGetBindingModel model)
		{
			try
			{
				var entity = string.IsNullOrEmpty(model.Title) ? _context.ScheduleLessonTimes.FirstOrDefault(e => e.Id == model.Id) :
																_context.ScheduleLessonTimes.FirstOrDefault(e => e.Title == model.Title);
				if (entity == null)
					return ResultService<ScheduleLessonTimeViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<ScheduleLessonTimeViewModel>.Success(
					ModelFactoryToViewModel.CreateScheduleLessonTimeViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ScheduleLessonTimeViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ScheduleLessonTimeViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateScheduleLessonTime(ScheduleLessonTimeRecordBindingModel model)
		{
			var entity = new ScheduleLessonTime
			{
				Title = model.Title,
				DateBeginLesson = model.DateBeginLesson,
				DateEndLesson = model.DateEndLesson
			};
			try
			{
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

		public ResultService UpdateScheduleLessonTime(ScheduleLessonTimeRecordBindingModel model)
		{
			try
			{
				var entity = _context.ScheduleLessonTimes
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.Title = model.Title;
				entity.DateBeginLesson = model.DateBeginLesson;
				entity.DateEndLesson = model.DateEndLesson;

				_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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
				var entity = _context.ScheduleLessonTimes
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.ScheduleLessonTimes.Remove(entity);
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
