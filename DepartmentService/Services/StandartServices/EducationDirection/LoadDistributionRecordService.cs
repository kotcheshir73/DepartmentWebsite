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
    public class LoadDistributionRecordService : ILoadDistributionRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Расчет_штатов;

		private readonly IAcademicPlanRecordService _serviceAPR;

		private readonly IContingentService _serviceC;

		private readonly ILecturerService _serviceL;

		private readonly ITimeNormService _serviceTN;

		public LoadDistributionRecordService(DepartmentDbContext context, IAcademicPlanRecordService serviceAPR, IContingentService serviceC,
			ILecturerService serviceL, ITimeNormService serviceTN)
		{
			_context = context;
			_serviceAPR = serviceAPR;
			_serviceC = serviceC;
			_serviceL = serviceL;
			_serviceTN = serviceTN;
		}


		public ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
		{
			return _serviceAPR.GetAcademicPlanRecords(model);
		}

		public ResultService<ContingentPageViewModel> GetContingents(ContingentGetBindingModel model)
		{
			return _serviceC.GetContingents(model);
		}

		public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
		{
			return _serviceL.GetLecturers(model);
		}

		public ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model)
		{
			return _serviceTN.GetTimeNorms(model);
		}


		public ResultService<LoadDistributionRecordPageViewModel> GetLoadDistributionRecords(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по записям расчета штата");
				}
				if (!model.LoadDistributionId.HasValue)
				{
					throw new Exception("Отсутсвует идентификатор рапределения нагрузок");
				}

				int countPages = 0;
				var query = _context.LoadDistributionRecords.Where(ldr => !ldr.IsDeleted && ldr.LoadDistributionId == model.LoadDistributionId).AsQueryable();

                query = query.OrderBy(ldr => ldr.Id);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(ldr => ldr.AcademicPlanRecord)
						.Include(ldr => ldr.Contingent)
						.Include(ldr => ldr.TimeNorm)
						.Include(ldr => ldr.AcademicPlanRecord.AcademicPlan.EducationDirection)
						.Include(ldr => ldr.AcademicPlanRecord.Discipline)
						.Include(ldr => ldr.AcademicPlanRecord.Discipline.DisciplineBlock)
						.Include(ldr => ldr.AcademicPlanRecord.KindOfLoad)
						.Include(ldr => ldr.Contingent.AcademicYear)
						.Include(ldr => ldr.TimeNorm.KindOfLoad);

				var result = new LoadDistributionRecordPageViewModel
				{
					MaxCount = countPages,
                    List = query.Select(ModelFactoryToViewModel.CreateLoadDistributionRecordViewModel).ToList()
                };

				return ResultService<LoadDistributionRecordPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LoadDistributionRecordViewModel> GetLoadDistributionRecord(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по записям расчета штата");
				}

				var entity = _context.LoadDistributionRecords
						.Include(ldr => ldr.AcademicPlanRecord)
                        .Include(ldr => ldr.Contingent)
                        .Include(ldr => ldr.TimeNorm)
						.Include(ldr => ldr.AcademicPlanRecord.Discipline)
                        .Include(ldr => ldr.AcademicPlanRecord.KindOfLoad)
						.Include(ldr => ldr.Contingent.AcademicYear)
						.Include(ldr => ldr.TimeNorm.KindOfLoad)
                        .FirstOrDefault(ldr => ldr.Id == model.Id && !ldr.IsDeleted);
				if (entity == null)
				{
					return ResultService<LoadDistributionRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<LoadDistributionRecordViewModel>.Success(ModelFactoryToViewModel.CreateLoadDistributionRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по записям расчета штата");
				}

				var entity = ModelFacotryFromBindingModel.CreateLoadDistributionRecord(model);

				_context.LoadDistributionRecords.Add(entity);
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

		public ResultService UpdateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по записям расчета штата");
				}

				var entity = _context.LoadDistributionRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateLoadDistributionRecord(model, entity);
				
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

		public ResultService DeleteLoadDistributionRecord(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по записям расчета штата");
				}

				var entity = _context.LoadDistributionRecords.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
