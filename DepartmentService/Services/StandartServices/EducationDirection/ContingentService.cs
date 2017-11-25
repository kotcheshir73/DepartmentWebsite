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
	public class ContingentService : IContingentService
	{
		private readonly DepartmentDbContext _context;

		private readonly AccessOperation _serviceOperation = AccessOperation.Контингент;

		private readonly IAcademicYearService _serviceAY;

		private readonly IEducationDirectionService _serviceED;

		public ContingentService(DepartmentDbContext context, IEducationDirectionService serviceED, IAcademicYearService serviceAY)
		{
			_context = context;
			_serviceAY = serviceAY;
			_serviceED = serviceED;
		}


		public ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model)
		{
			return _serviceAY.GetAcademicYears(model);
		}

		public ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model)
		{
			return _serviceED.GetEducationDirections(model);
		}


		public ResultService<ContingentPageViewModel> GetContingents(ContingentGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по контингенту");
				}

				int countPages = 0;
				var query = _context.Contingents.Where(c => !c.IsDeleted).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(e => e.AcademicYearId).ThenBy(e => e.EducationDirectionId)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(ap => ap.AcademicYear).Include(s => s.EducationDirection);

				var result = new ContingentPageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateContingents(query).ToList()
				};

				return ResultService<ContingentPageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ContingentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ContingentPageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ContingentViewModel> GetContingent(ContingentGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по контингенту");
				}

				var entity = _context.Contingents.Include(ap => ap.AcademicYear).Include(s => s.EducationDirection)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<ContingentViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<ContingentViewModel>.Success(ModelFactoryToViewModel.CreateContingentViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ContingentViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ContingentViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateContingent(ContingentRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по контингенту");
				}

				var entity = ModelFacotryFromBindingModel.CreateContingent(model);

				_context.Contingents.Add(entity);
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

		public ResultService UpdateContingent(ContingentRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по контингенту");
				}

				var entity = _context.Contingents
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateContingent(model, entity);
				
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

		public ResultService DeleteContingent(ContingentGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по контингенту");
				}

				var entity = _context.Contingents
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
