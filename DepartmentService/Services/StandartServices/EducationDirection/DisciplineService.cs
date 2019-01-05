using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
    public class DisciplineService : IDisciplineService
	{
		private readonly DepartmentDbContext _context;

		private readonly IDisciplineBlockService _serviceDB;

        private readonly IAcademicYearService _serviceAY;

        private readonly ISeasonDatesService _serviceSD;

        private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

		public DisciplineService(DepartmentDbContext context, IDisciplineBlockService serviceDB, IAcademicYearService serviceAY, ISeasonDatesService serviceSD)
		{
			_context = context;
			_serviceDB = serviceDB;
            _serviceAY = serviceAY;
            _serviceSD = serviceSD;
        }
        
		public ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model)
		{
			return _serviceDB.GetDisciplineBlocks(model);
		}

        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<SeasonDatesPageViewModel> GetSeasonDaties(SeasonDatesGetBindingModel model)
        {
            return _serviceSD.GetSeasonDaties(model);
        }


		public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по дисциплинам");
				}

				int countPages = 0;
				var query = _context.Disciplines.Where(d => !d.IsDeleted).AsQueryable();

                query = query.OrderBy(d => d.DisciplineBlock.DisciplineBlockOrder).ThenBy(d => d.DisciplineName);

				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
                }

                query = query.Include(d => d.DisciplineBlock);

				var result = new DisciplinePageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateDisciplineViewModel).ToList()
                };

				return ResultService<DisciplinePageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<DisciplinePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<DisciplinePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<DisciplineViewModel> GetDiscipline(DisciplineGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по дисциплинам");
				}

				var entity = _context.Disciplines
								.FirstOrDefault(d => d.Id == model.Id && !d.IsDeleted);
				if (entity == null)
				{
					return ResultService<DisciplineViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<DisciplineViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<DisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<DisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateDiscipline(DisciplineSetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по дисциплинам");
				}

				var entity = ModelFacotryFromBindingModel.CreateDiscipline(model);

				_context.Disciplines.Add(entity);
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

		public ResultService UpdateDiscipline(DisciplineSetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по дисциплинам");
				}

				var entity = _context.Disciplines.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateDiscipline(model, entity);
				
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

		public ResultService DeleteDiscipline(DisciplineGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по дисциплинам");
				}

				var entity = _context.Disciplines.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
