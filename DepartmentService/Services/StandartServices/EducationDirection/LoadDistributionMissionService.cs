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
	public class LoadDistributionMissionService : ILoadDistributionMissionService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Расчет_штатов;

		public LoadDistributionMissionService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<LoadDistributionMissionPageViewModel> GetLoadDistributionMissions(LoadDistributionMissionGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по нагрузкам расчетов штата");
				}

				int countPages = 0;
				var query = _context.LoadDistributionMissions.Where(ldm => !ldm.IsDeleted).AsQueryable();
				if (model.LecturerId.HasValue)
				{
					query = query.Where(ldm => ldm.LecturerId == model.LecturerId.Value);
				}
				if (model.LoadDistributionRecordId.HasValue)
				{
					query = query.Where(ldm => ldm.LoadDistributionRecordId == model.LoadDistributionRecordId.Value);
                }

                query = query.OrderBy(ldm => ldm.Id);

                if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new LoadDistributionMissionPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateLoadDistributionMissions(query).ToList()
				};

				return ResultService<LoadDistributionMissionPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionMissionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionMissionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LoadDistributionMissionViewModel> GetLoadDistributionMission(LoadDistributionMissionGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по нагрузкам расчетов штата");
				}

				var entity = _context.LoadDistributionMissions
								.FirstOrDefault(ldm => ldm.Id == model.Id.Value && !ldm.IsDeleted);
				if (entity == null)
				{
					return ResultService<LoadDistributionMissionViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<LoadDistributionMissionViewModel>.Success(ModelFactoryToViewModel.CreateLoadDistributionMissionViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionMissionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionMissionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLoadDistributionMission(LoadDistributionMissionRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нагрузкам расчетов штата");
				}

				var entity = ModelFacotryFromBindingModel.CreateLoadDistributionMission(model);

				_context.LoadDistributionMissions.Add(entity);
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

		public ResultService UpdateLoadDistributionMission(LoadDistributionMissionRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по нагрузкам расчетов штата");
				}

				var entity = _context.LoadDistributionMissions.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateLoadDistributionMission(model, entity);

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

		public ResultService DeleteLoadDistributionMission(LoadDistributionMissionGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по нагрузкам расчетов штата");
				}

				var entity = _context.LoadDistributionMissions.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
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
