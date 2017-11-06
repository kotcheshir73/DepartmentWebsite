using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

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
					ModelFactoryToViewModel.CreateSeasonDaties(_context.SeasonDates)
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
					ModelFactoryToViewModel.CreateSeasonDatesViewModel(entity));
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
			var entity = ModelFacotryFromBindingModel.CreateSeasonDates(model);
			try
			{
				_context.SeasonDates.Add(entity);
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
				entity = ModelFacotryFromBindingModel.CreateSeasonDates(model, entity);
				
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
