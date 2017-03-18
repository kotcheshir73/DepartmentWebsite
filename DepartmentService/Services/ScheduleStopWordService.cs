using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using DepartmentDAL.Enums;
using System.Data.Entity.Validation;

namespace DepartmentService.Services
{
	public class ScheduleStopWordService : IScheduleStopWordService
	{
		private readonly DepartmentDbContext _context;

		public ScheduleStopWordService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<List<ScheduleStopWordViewModel>> GetScheduleStopWords()
		{
			try
			{
				return ResultService<List<ScheduleStopWordViewModel>>.Success(
					ModelFactory.CreateScheduleStopWords(_context.ScheduleStopWords)
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ScheduleStopWordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ScheduleStopWordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ScheduleStopWordViewModel> GetScheduleStopWord(ScheduleStopWordGetBindingModel model)
		{
			try
			{
				var entity = _context.ScheduleStopWords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
					return ResultService<ScheduleStopWordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<ScheduleStopWordViewModel>.Success(
					ModelFactory.CreateScheduleStopWordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ScheduleStopWordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ScheduleStopWordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateScheduleStopWord(ScheduleStopWordRecordBindingModel model)
		{
			var entity = new ScheduleStopWord
			{
				StopWord = model.StopWord,
				StopWordType = (ScheduleStopWordTypes)Enum.Parse(typeof(ScheduleStopWordTypes), model.StopWordType)
			};
			try
			{
				_context.ScheduleStopWords.Add(entity);
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

		public ResultService UpdateScheduleStopWord(ScheduleStopWordRecordBindingModel model)
		{
			try
			{
				var entity = _context.ScheduleStopWords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.StopWord = model.StopWord;
				entity.StopWordType = (ScheduleStopWordTypes)Enum.Parse(typeof(ScheduleStopWordTypes), model.StopWordType);

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

		public ResultService DeleteScheduleStopWord(ScheduleStopWordGetBindingModel model)
		{
			try
			{
				var entity = _context.ScheduleStopWords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.ScheduleStopWords.Remove(entity);
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
