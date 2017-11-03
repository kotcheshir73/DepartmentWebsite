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
using System.Data.Entity.Validation;

namespace DepartmentService.Services
{
	public class LoadDistributionMissionService : ILoadDistributionMissionService
	{
		private readonly DepartmentDbContext _context;

		public LoadDistributionMissionService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<LoadDistributionMissionViewModel>> GetLoadDistributionMissions(LoadDistributionMissionGetBindingModel model)
		{
			try
			{
				if(model.LecturerId.HasValue)
				{
					return ResultService<List<LoadDistributionMissionViewModel>>.Success(
						ModelFactoryToViewModel.CreateLoadDistributionMissions(_context.LoadDistributionMissions
								.Where(e => !e.IsDeleted && e.LecturerId == model.LecturerId.Value))
						.ToList());
				}
				if (model.LoadDistributionRecordId.HasValue)
				{
					return ResultService<List<LoadDistributionMissionViewModel>>.Success(
						ModelFactoryToViewModel.CreateLoadDistributionMissions(_context.LoadDistributionMissions
								.Where(e => !e.IsDeleted && e.LoadDistributionRecordId == model.LoadDistributionRecordId.Value))
						.ToList());
				}
				return ResultService<List<LoadDistributionMissionViewModel>>.Success(
					ModelFactoryToViewModel.CreateLoadDistributionMissions(_context.LoadDistributionMissions
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<LoadDistributionMissionViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<LoadDistributionMissionViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LoadDistributionMissionViewModel> GetLoadDistributionMission(LoadDistributionMissionGetBindingModel model)
		{
			try
			{
				if(!model.Id.HasValue)
				{
					throw new Exception("Id is null");
				}
				var entity = _context.LoadDistributionMissions
								.FirstOrDefault(e => e.Id == model.Id.Value && !e.IsDeleted);
				if (entity == null)
					return ResultService<LoadDistributionMissionViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<LoadDistributionMissionViewModel>.Success(
					ModelFactoryToViewModel.CreateLoadDistributionMissionViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LoadDistributionMissionViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LoadDistributionMissionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLoadDistributionMission(LoadDistributionMissionRecordBindingModel model)
		{
			var entity = new LoadDistributionMission
			{
				LoadDistributionRecordId = model.LoadDistributionRecordId,
				LecturerId = model.LecturerId,
				Hours = model.Hours,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
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
				var entity = _context.LoadDistributionMissions
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.LecturerId = model.LecturerId;
				entity.Hours = model.Hours;

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

		public ResultService DeleteLoadDistributionMission(LoadDistributionMissionGetBindingModel model)
		{
			try
			{
				var entity = _context.LoadDistributionMissions
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
