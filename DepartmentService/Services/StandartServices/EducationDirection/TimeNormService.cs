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
    public class TimeNormService : ITimeNormService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Нормы_времени;

        private readonly IAcademicYearService _serviceAY;

        private readonly IDisciplineBlockService _serviceDB;

        public TimeNormService(DepartmentDbContext context, IAcademicYearService serviceAY, IDisciplineBlockService serviceDB)
		{
			_context = context;
            _serviceAY = serviceAY;
            _serviceDB = serviceDB;
        }

        
        public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
        {
            return _serviceAY.GetAcademicYears(model);
        }

        public ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model)
        {
            return _serviceDB.GetDisciplineBlocks(model);
        }


        public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по нормам времени");
				}

				int countPages = 0;
				var query = _context.TimeNorms.Where(tn => !tn.IsDeleted).AsQueryable();

                if (model.AcademicYearId.HasValue)
                {
                    query = query.Where(tn => tn.AcademicYearId == model.AcademicYearId);
                }

                if (model.DisciplineBlockId.HasValue)
                {
                    query = query.Where(tn => tn.DisciplineBlockId == model.DisciplineBlockId);
                }

                if (model.AcademicPlanRecordId.HasValue)
                {
                    var apr = _context.AcademicPlanRecords.Include(x => x.AcademicPlan).FirstOrDefault(x => x.Id == model.AcademicPlanRecordId);
                    query = query.Where(tn => tn.AcademicYearId == apr.AcademicPlan.AcademicYearId);
                }

                query = query.OrderBy(tn => tn.TimeNormOrder).ThenBy(tn => tn.TimeNormName);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(tn => tn.AcademicYear).Include(x => x.DisciplineBlock);

				var result = new TimeNormPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateTimeNormViewModel).ToList()
                };

				return ResultService<TimeNormPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<TimeNormPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<TimeNormPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<TimeNormViewModel> GetTimeNorm(TimeNormGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по нормам времени");
				}

				var entity = _context.TimeNorms
                                .Include(tn => tn.AcademicYear)
                                .Include(x => x.DisciplineBlock)
                                .FirstOrDefault(tn => tn.Id == model.Id && !tn.IsDeleted);
				if (entity == null)
				{
					return ResultService<TimeNormViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<TimeNormViewModel>.Success(ModelFactoryToViewModel.CreateTimeNormViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<TimeNormViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<TimeNormViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateTimeNorm(TimeNormRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нормам времени");
				}

				var entity = ModelFacotryFromBindingModel.CreateTimeNorm(model);

				_context.TimeNorms.Add(entity);
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

		public ResultService UpdateTimeNorm(TimeNormRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нормам времени");
				}

				var entity = _context.TimeNorms.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateTimeNorm(model, entity);
				
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

		public ResultService DeleteTimeNorm(TimeNormGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нормам времени");
				}

				var entity = _context.TimeNorms.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
