﻿using DepartmentDAL;
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
	public class DisciplineService : IDisciplineService
	{
		private readonly DepartmentDbContext _context;

		private readonly IDisciplineBlockService _serviceDB;

		private readonly AccessOperation _serviceOperation = AccessOperation.Дисциплины;

		public DisciplineService(DepartmentDbContext context, IDisciplineBlockService serviceDB)
		{
			_context = context;
			_serviceDB = serviceDB;
		}


		public ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model)
		{
			return _serviceDB.GetDisciplineBlocks(model);
		}


		public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по дисциплинам");
				}

				int countPages = 0;
				var query = _context.Disciplines.Where(c => !c.IsDeleted).AsQueryable();
				if (model.PageNumber.HasValue && model.PageSize.HasValue)
				{
					countPages = (int)Math.Ceiling((double)query.Count() / model.PageSize.Value);
					query = query
								.OrderBy(c => c.DisciplineName)
								.Skip(model.PageSize.Value * model.PageNumber.Value)
								.Take(model.PageSize.Value);
				}

				query = query.Include(d => d.DisciplineBlock);

				var result = new DisciplinePageViewModel
				{
					MaxCount = countPages,
					List = ModelFactoryToViewModel.CreateDisciplines(query).ToList()
				};

				return ResultService<DisciplinePageViewModel>.Success(result);
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<DisciplinePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<DisciplinePageViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService<DisciplineViewModel> GetDiscipline(DisciplineGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
				{
					throw new Exception("Нет доступа на чтение данных по дисциплинам");
				}

				var entity = _context.Disciplines
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService<DisciplineViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}

				return ResultService<DisciplineViewModel>.Success(ModelFactoryToViewModel.CreateDisciplineViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<DisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<DisciplineViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateDiscipline(DisciplineRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по дисциплинам");
				}

				var entity = ModelFacotryFromBindingModel.CreateDiscipline(model);

				_context.Disciplines.Add(entity);
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

		public ResultService UpdateDiscipline(DisciplineRecordBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
				{
					throw new Exception("Нет доступа на изменение данных по дисциплинам");
				}

				var entity = _context.Disciplines
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateDiscipline(model, entity);
				
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

		public ResultService DeleteDiscipline(DisciplineGetBindingModel model)
		{
			try
			{
				if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
				{
					throw new Exception("Нет доступа на удаление данных по дисциплинам");
				}

				var entity = _context.Disciplines
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