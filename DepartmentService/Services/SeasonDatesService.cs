using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class SeasonDatesService : ISeasonDatesService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Даты_семестра;

		public SeasonDatesService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<SeasonDatesPageViewModel> GetSeasonDaties(SeasonDatesGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по датам семестра");
				}

				int countPages = 0;
				var query = _context.SeasonDates.AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(c => c.Id)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new SeasonDatesPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateSeasonDaties(query).ToList()
				};

				return ResultService<SeasonDatesPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<SeasonDatesPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по аудиториям");
				}

				var entity = string.IsNullOrEmpty(model.Title) ?
					_context.SeasonDates.FirstOrDefault(sd => sd.Id == model.Id) :
					_context.SeasonDates.FirstOrDefault(sd => sd.Title == model.Title);
				if (entity == null)
				{
					return ResultService<SeasonDatesViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<SeasonDatesViewModel>.Success(ModelFactoryToViewModel.CreateSeasonDatesViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<SeasonDatesViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<SeasonDatesViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateSeasonDates(SeasonDatesRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по аудиториям");
				}

				var entity = ModelFacotryFromBindingModel.CreateSeasonDates(model);

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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по аудиториям");
				}

				var entity = _context.SeasonDates
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по аудиториям");
				}

				var entity = _context.SeasonDates
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
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
