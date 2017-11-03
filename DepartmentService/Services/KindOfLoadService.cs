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
	public class KindOfLoadService : IKindOfLoadService
	{
		private readonly DepartmentDbContext _context;

		public KindOfLoadService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<KindOfLoadViewModel>> GetKindOfLoads()
		{
			try
			{
				return ResultService<List<KindOfLoadViewModel>>.Success(ModelFactoryToViewModel.CreateKindOfLoads(
						_context.KindOfLoads
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<KindOfLoadViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<KindOfLoadViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<KindOfLoadViewModel> GetKindOfLoad(KindOfLoadGetBindingModel model)
		{
			try
			{
				var entity = _context.KindOfLoads
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<KindOfLoadViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<KindOfLoadViewModel>.Success(
					ModelFactoryToViewModel.CreateKindOfLoadViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<KindOfLoadViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<KindOfLoadViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateKindOfLoad(KindOfLoadRecordBindingModel model)
		{
			var entity = new KindOfLoad
			{
				KindOfLoadName = model.KindOfLoadName,
				KindOfLoadType = (KindOfLoadType)Enum.Parse(typeof(KindOfLoadType), model.KindOfLoadType),
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.KindOfLoads.Add(entity);
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

		public ResultService UpdateKindOfLoad(KindOfLoadRecordBindingModel model)
		{
			try
			{
				var entity = _context.KindOfLoads
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.KindOfLoadName = model.KindOfLoadName;
				entity.KindOfLoadType = (KindOfLoadType)Enum.Parse(typeof(KindOfLoadType), model.KindOfLoadType);

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

		public ResultService DeleteKindOfLoad(KindOfLoadGetBindingModel model)
		{
			try
			{
				var entity = _context.KindOfLoads
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
