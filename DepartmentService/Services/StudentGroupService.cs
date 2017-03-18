﻿using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using System.Data.Entity.Validation;
using DepartmentDAL.Enums;

namespace DepartmentService.Services
{
	public class StudentGroupService : IStudentGroupService
	{
		private readonly DepartmentDbContext _context;

		private readonly IEducationDirectionService _serviceED;

		public StudentGroupService(DepartmentDbContext context, IEducationDirectionService serviceED)
		{
			_context = context;
			_serviceED = serviceED;
		}

		public ResultService<List<StudentGroupViewModel>> GetStudentGroups()
		{
			try
			{
				return ResultService<List<StudentGroupViewModel>>.Success(
					ModelFactory.CreateStudentGroups(_context.StudentGroups
						.Include(s => s.EducationDirection).Include(s => s.Students)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<StudentGroupViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<StudentGroupViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<List<EducationDirectionViewModel>> GetEducationDirections()
		{
			return _serviceED.GetEducationDirections();
		}

		public ResultService<StudentGroupViewModel> GetStudentGroup(StudentGroupGetBindingModel model)
		{
			try
			{
				var entity = _context.StudentGroups.Include(s => s.EducationDirection).Include(s => s.Steward)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<StudentGroupViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<StudentGroupViewModel>.Success(
					ModelFactory.CreateStudentGroupViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StudentGroupViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StudentGroupViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateStudentGroup(StudentGroupRecordBindingModel model)
		{
			var entity = new StudentGroup
			{
				EducationDirectionId = model.EducationDirectionId,
				DateCreate = DateTime.Now,
				GroupName = model.GroupName,
				IsDeleted = false,
				Kurs = model.Kurs
			};
			try
			{
				_context.StudentGroups.Add(entity);
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

		public ResultService UpdateStudentGroup(StudentGroupRecordBindingModel model)
		{
			try
			{
				var entity = _context.StudentGroups
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.GroupName = model.GroupName;
				entity.Kurs = model.Kurs;
				if (!string.IsNullOrEmpty(model.StewardId))
				{
					entity.StewardId = model.StewardId;
				}

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

		public ResultService DeleteStudentGroup(StudentGroupGetBindingModel model)
		{
			try
			{
				var entity = _context.StudentGroups
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
