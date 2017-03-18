using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL;
using DepartmentDAL.Models;
using System.Data.Entity.Validation;
using DepartmentDAL.Enums;

namespace DepartmentService.Services
{
	public class SeasonDatesService : ISeasonDatesService
	{
		private readonly DepartmentDbContext _context;

		public SeasonDatesService(DepartmentDbContext context)
		{
			_context = context;
		}

		public ResultService<List<SeasonDatesViewModel>> GetSeasonDaties()
		{
			try
			{
				return ResultService<List<SeasonDatesViewModel>>.Success(
					ModelFactory.CreateSeasonDaties(_context.SeasonDates)
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<SeasonDatesViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<SeasonDatesViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<SeasonDatesViewModel> GetSeasonDates(SeasonDatesGetBindingModel model)
		{
			try
			{
				var entity = string.IsNullOrEmpty(model.Title) ?
					_context.SeasonDates.FirstOrDefault(sd => sd.Id == model.Id) :
					_context.SeasonDates.FirstOrDefault(sd => sd.Title == model.Title);
				if (entity == null)
					return ResultService<SeasonDatesViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<SeasonDatesViewModel>.Success(
					ModelFactory.CreateSeasonDatesViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<SeasonDatesViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<SeasonDatesViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateSeasonDates(SeasonDatesRecordBindingModel model)
		{
			var entity = new SeasonDates
			{
				Id = model.Id,
				Title = model.Title,
				DateBeginExamination = model.DateBeginExamination,
				DateBeginOffset = model.DateBeginOffset,
				DateBeginPractice = model.DateBeginPractice,
				DateBeginSemester = model.DateBeginSemester,
				DateEndExamination = model.DateEndExamination,
				DateEndOffset = model.DateEndOffset,
				DateEndPractice = model.DateEndPractice,
				DateEndSemester = model.DateEndSemester
			};
			try
			{
				_context.SeasonDates.Add(entity);
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

		public ResultService UpdateSeasonDates(SeasonDatesRecordBindingModel model)
		{
			try
			{
				var entity = _context.SeasonDates
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.Title = model.Title;
				entity.DateBeginExamination = model.DateBeginExamination;
				entity.DateBeginOffset = model.DateBeginOffset;
				entity.DateBeginPractice = model.DateBeginPractice;
				entity.DateBeginSemester = model.DateBeginSemester;
				entity.DateEndExamination = model.DateEndExamination;
				entity.DateEndOffset = model.DateEndOffset;
				entity.DateEndPractice = model.DateEndPractice;
				entity.DateEndSemester = model.DateEndSemester;

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

		public ResultService DeleteSeasonDates(SeasonDatesGetBindingModel model)
		{
			try
			{
				var entity = _context.SeasonDates
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.SeasonDates.Remove(entity);
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
