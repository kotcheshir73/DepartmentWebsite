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
    public class SeasonDatesService : ISeasonDatesService
	{
		private readonly DepartmentDbContext _context;

        private readonly IAcademicYearService _serviceAY;

        private readonly AccessOperation _serviceOperation = AccessOperation.Даты_семестра;

		public SeasonDatesService(DepartmentDbContext context, IAcademicYearService serviceAY)
		{
			_context = context;
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
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по датам семестра");
				}

				int countPages = 0;
				var query = _context.SeasonDates.Where(sd => !sd.IsDeleted).AsQueryable();

                if (model.AcademicYearId.HasValue)
                {
                    query = query.Where(sd => sd.AcademicYearId == model.AcademicYearId);
                }

                query = query.OrderBy(sd => sd.Id);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
                }

                query = query.Include(sd => sd.AcademicYear);

                var result = new SeasonDatesPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateSeasonDatesViewModel).ToList()
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
					throw new Exception("Нет доступа на чтение данных по датам семестра");
				}

				var entity = string.IsNullOrEmpty(model.Title) ?
					                    _context.SeasonDates.Include(sd => sd.AcademicYear).FirstOrDefault(sd => sd.Id == model.Id && !sd.IsDeleted) :
					                    _context.SeasonDates.Include(sd => sd.AcademicYear).FirstOrDefault(sd => sd.Title == model.Title && !sd.IsDeleted);
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
					throw new Exception("Нет доступа на изменение данных по датам семестра");
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
					throw new Exception("Нет доступа на изменение данных по датам семестра");
				}

				var entity = _context.SeasonDates.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
					throw new Exception("Нет доступа на удаление данных по датам семестра");
				}

				var entity = _context.SeasonDates.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
