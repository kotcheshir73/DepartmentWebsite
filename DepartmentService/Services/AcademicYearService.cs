﻿using DepartmentDAL;
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
	public class AcademicYearService : IAcademicYearService
	{
		private readonly DepartmentDbContext _context;

		public AcademicYearService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<AcademicYearViewModel>> GetAcademicYears()
		{
			try
			{
				return ResultService<List<AcademicYearViewModel>>.Success(
					ModelFactoryToViewModel.CreateAcademicYears(_context.AcademicYears
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<AcademicYearViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<AcademicYearViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<AcademicYearViewModel> GetAcademicYear(AcademicYearGetBindingModel model)
		{
			try
			{
				var entity = _context.AcademicYears
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<AcademicYearViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<AcademicYearViewModel>.Success(
					ModelFactoryToViewModel.CreateAcademicYearViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<AcademicYearViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<AcademicYearViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateAcademicYear(AcademicYearRecordBindingModel model)
		{
			var entity = new AcademicYear
			{
				Title = model.Title,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.AcademicYears.Add(entity);
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

		public ResultService UpdateAcademicYear(AcademicYearRecordBindingModel model)
		{
			{
				try
				{
					var entity = _context.AcademicYears
									.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
					if (entity == null)
					{
						return ResultService.Error("Error:", "Entity not found",
							ResultServiceStatusCode.NotFound);
					}
					entity.Title = model.Title;

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

		public ResultService DeleteAcademicYear(AcademicYearGetBindingModel model)
		{
			try
			{
				var entity = _context.AcademicYears
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
