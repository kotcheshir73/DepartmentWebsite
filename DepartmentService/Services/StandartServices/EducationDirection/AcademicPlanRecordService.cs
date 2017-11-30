﻿using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class AcademicPlanRecordService : IAcademicPlanRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Учебные_планы;

		private readonly IAcademicPlanService _serviceAP;

		private readonly IDisciplineService _serviceD;

		private readonly IKindOfLoadService _serviceKL;

		public AcademicPlanRecordService(DepartmentDbContext context, IAcademicPlanService serviceAP,
			IDisciplineService serviceD, IKindOfLoadService serviceKL)
		{
			_context = context;
			_serviceAP = serviceAP;
			_serviceD = serviceD;
			_serviceKL = serviceKL;
		}


		public ResultService<AcademicPlanPageViewModel> GetAcademicPlans(AcademicPlanGetBindingModel model)
		{
			return _serviceAP.GetAcademicPlans(model);
		}

		public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
		{
			return _serviceD.GetDisciplines(model);
		}

		public ResultService<KindOfLoadPageViewModel> GetKindOfLoads(KindOfLoadGetBindingModel model)
		{
			return _serviceKL.GetKindOfLoads(model);
		}


		public ResultService<AcademicPlanRecordPageViewModel> GetAcademicPlanRecords(AcademicPlanRecordGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по записям учекбных планов");
				}
				if (!model.AcademicPlanId.HasValue)
				{
					throw new Exception("Не указан учебный план");
				}

					int countPages = 0;
				var query = _context.AcademicPlanRecords.Where(c => !c.IsDeleted && c.AcademicPlanId == model.AcademicPlanId).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(e => e.Semester).ThenBy(e => e.Discipline.DisciplineName)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(ar => ar.Discipline).Include(ar => ar.KindOfLoad);

				var result = new AcademicPlanRecordPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateAcademicPlanRecords(query).ToList()
				};

				return ResultService<AcademicPlanRecordPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AcademicPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AcademicPlanRecordPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<AcademicPlanRecordViewModel> GetAcademicPlanRecord(AcademicPlanRecordGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по записям учекбных планов");
				}

				var entity = _context.AcademicPlanRecords.Include(ar => ar.Discipline).Include(ar => ar.KindOfLoad)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<AcademicPlanRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<AcademicPlanRecordViewModel>.Success(ModelFactoryToViewModel.CreateAcademicPlanRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AcademicPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AcademicPlanRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateAcademicPlanRecord(AcademicPlanRecordRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по записям учекбных планов");
				}

				var entity = ModelFacotryFromBindingModel.CreateAcademicPlanRecord(model);

				_context.AcademicPlanRecords.Add(entity);
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

		public ResultService UpdateAcademicPlanRecord(AcademicPlanRecordRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по записям учекбных планов");
				}

				var entity = _context.AcademicPlanRecords
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateAcademicPlanRecord(model, entity);
				
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

		public ResultService DeleteAcademicPlanRecord(AcademicPlanRecordGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по записям учекбных планов");
				}

				var entity = _context.AcademicPlanRecords
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