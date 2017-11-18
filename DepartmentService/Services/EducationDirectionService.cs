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
	public class EducationDirectionService : IEducationDirectionService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Направления;

		public EducationDirectionService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по направлениям");
				}

				int countPages = 0;
				var query = _context.EducationDirections.Where(c => !c.IsDeleted).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(c => c.Id)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				var result = new EducationDirectionPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateEducationDirections(query).ToList()
				};

				return ResultService<EducationDirectionPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<EducationDirectionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<EducationDirectionPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<EducationDirectionViewModel> GetEducationDirection(EducationDirectionGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по направлениям");
				}

				var entity = _context.EducationDirections
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<EducationDirectionViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				return ResultService<EducationDirectionViewModel>.Success(ModelFactoryToViewModel.CreateEducationDirectionViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<EducationDirectionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<EducationDirectionViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateEducationDirection(EducationDirectionRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по направлениям");
				}

				var entity = ModelFacotryFromBindingModel.CreateEducationDirection(model);
				_context.EducationDirections.Add(entity);
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

		public ResultService UpdateEducationDirection(EducationDirectionRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по направлениям");
				}

				var entity = _context.EducationDirections
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateEducationDirection(model, entity);

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

		public ResultService DeleteEducationDirection(EducationDirectionGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по направлениям");
				}

				var entity = _context.EducationDirections
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
