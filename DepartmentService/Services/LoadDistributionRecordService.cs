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
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DepartmentService.Services
{
	public class LoadDistributionRecordService : ILoadDistributionRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly IAcademicPlanRecordService _serviceAPR;

		private readonly IContingentService _serviceC;

		private readonly ITimeNormService _serviceTN;

		public LoadDistributionRecordService(DepartmentDbContext context, IAcademicPlanRecordService serviceAPR, IContingentService serviceC, ITimeNormService serviceTN)
		{
			_context = context;
			_serviceAPR = serviceAPR;
			_serviceC = serviceC;
			_serviceTN = serviceTN;
		}


		public ResultService<List<AcademicPlanRecordViewModel>> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
		{
			return _serviceAPR.GetAcademicPlanRecords(model);
		}

		public ResultService<List<ContingentViewModel>> GetContingents()
		{
			return _serviceC.GetContingents();
		}

		public ResultService<List<TimeNormViewModel>> GetTimeNorms()
		{
			return _serviceTN.GetTimeNorms();
		}

		
		public ResultService<List<LoadDistributionRecordViewModel>> GetLoadDistributionRecords(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				if(!model.LoadDistributionId.HasValue)
				{
					throw new Exception("Отсутсвует идентификатор рапределения нагрузок");
				}
				return ResultService<List<LoadDistributionRecordViewModel>>.Success(
					ModelFactory.CreateLoadDistributionRecords(_context.LoadDistributionRecords
					.Where(e => e.LoadDistributionId == model.LoadDistributionId.Value)
						.Include(e => e.AcademicPlanRecord).Include(e => e.Contingent).Include(e => e.TimeNorm)
						.Include(e => e.AcademicPlanRecord.Discipline).Include(e => e.AcademicPlanRecord.KindOfLoad)
						.Include(e => e.Contingent.AcademicYear).Include(e => e.Contingent.StudentGroup)
						.Include(e => e.TimeNorm.KindOfLoad)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<LoadDistributionRecordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<LoadDistributionRecordViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LoadDistributionRecordViewModel> GetLoadDistribution(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributionRecords
						.Include(e => e.AcademicPlanRecord).Include(e => e.Contingent).Include(e => e.TimeNorm)
						.Include(e => e.AcademicPlanRecord.Discipline).Include(e => e.AcademicPlanRecord.KindOfLoad)
						.Include(e => e.Contingent.AcademicYear).Include(e => e.Contingent.StudentGroup)
						.Include(e => e.TimeNorm.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<LoadDistributionRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<LoadDistributionRecordViewModel>.Success(
					ModelFactory.CreateLoadDistributionRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLoadDistributionRecord(LoadDistributionRecordRecordBindingModel model)
		{
			var entity = new LoadDistributionRecord
			{
				LoadDistributionId = model.LoadDistributionId,
				AcademicPlanRecordId = model.AcademicPlanRecordId,
				ContingentId = model.ContingentId,
				TimeNormId = model.TimeNormId,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
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
				var entity = _context.LoadDistributionRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.AcademicPlanRecordId = model.AcademicPlanRecordId;
				entity.ContingentId = model.ContingentId;
				entity.TimeNormId = model.TimeNormId;

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

		public ResultService DeleteLoadDistributionRecord(LoadDistributionRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributionRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.IsDeleted = true;
				entity.DateDelete = DateTime.Now;

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
	}
}
