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
	public class ContingentService : IContingentService
	{
		private readonly DepartmentDbContext _context;

		private readonly IAcademicYearService _serviceAY;

		private readonly IStudentGroupService _serviceSG;

		public ContingentService(DepartmentDbContext context, IStudentGroupService serviceSG, IAcademicYearService serviceAY)
		{
			_context = context;
			_serviceAY = serviceAY;
			_serviceSG = serviceSG;
		}


		public ResultService<List<AcademicYearViewModel>> GetAcademicYears()
		{
			return _serviceAY.GetAcademicYears();
		}

		public ResultService<List<StudentGroupViewModel>> GetStudentGroups()
		{
			return _serviceSG.GetStudentGroups();
		}


		public ResultService<List<ContingentViewModel>> GetContingents()
		{
			try
			{
				return ResultService<List<ContingentViewModel>>.Success(
					ModelFactory.CreateContingents(_context.Contingents
						.Include(ap => ap.AcademicYear).Include(s => s.StudentGroup)
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<ContingentViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<ContingentViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<ContingentViewModel> GetContingent(ContingentGetBindingModel model)
		{
			try
			{
				var entity = _context.Contingents.Include(ap => ap.AcademicYear).Include(s => s.StudentGroup)
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<ContingentViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<ContingentViewModel>.Success(
					ModelFactory.CreateContingentViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ContingentViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ContingentViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateContingent(ContingentRecordBindingModel model)
		{
			var entity = new Contingent
			{
				AcademicYearId = model.AcademicYearId,
				StudentGroupId = model.StudentGroupId,
				CountStudetns = model.CountStudents,
				CountSubgroups = model.CountSubgroups,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
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
				var entity = _context.Contingents
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.AcademicYearId = model.AcademicYearId;
				entity.StudentGroupId = model.StudentGroupId;
				entity.CountStudetns = model.CountStudents;
				entity.CountSubgroups = model.CountSubgroups;

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

		public ResultService DeleteContingent(ContingentGetBindingModel model)
		{
			try
			{
				var entity = _context.Contingents
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
