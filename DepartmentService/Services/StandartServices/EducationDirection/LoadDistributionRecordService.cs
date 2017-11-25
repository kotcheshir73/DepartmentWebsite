using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
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
				var query = _context.LoadDistributionRecords.Where(c => !c.IsDeleted && c.LoadDistributionId == model.LoadDistributionId).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(c => c.Id)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new LoadDistributionRecordPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateLoadDistributionRecords(query).ToList()
				};

				query = query.Include(e => e.AcademicPlanRecord)
						.Include(e => e.Contingent)
						.Include(e => e.TimeNorm)
						.Include(e => e.AcademicPlanRecord.AcademicPlan.EducationDirection)
						.Include(e => e.AcademicPlanRecord.Discipline)
						.Include(e => e.AcademicPlanRecord.Discipline.DisciplineBlock)
						.Include(e => e.AcademicPlanRecord.KindOfLoad)
						.Include(e => e.Contingent.AcademicYear)
						.Include(e => e.TimeNorm.KindOfLoad);

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
						.Include(e => e.AcademicPlanRecord).Include(e => e.Contingent).Include(e => e.TimeNorm)
						.Include(e => e.AcademicPlanRecord.Discipline).Include(e => e.AcademicPlanRecord.KindOfLoad)
						.Include(e => e.Contingent.AcademicYear)
						.Include(e => e.TimeNorm.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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

				var entity = _context.LoadDistributionRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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

				var entity = _context.LoadDistributionRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
