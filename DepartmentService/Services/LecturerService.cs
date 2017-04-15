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
	public class LecturerService : ILecturerService
	{
		private readonly DepartmentDbContext _context;

		public LecturerService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<LecturerViewModel>> GetLecturers()
		{
			try
			{
				return ResultService<List<LecturerViewModel>>.Success(ModelFactory.CreateLecturers(
						_context.Lecturers
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<LecturerViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<LecturerViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<LecturerViewModel> GetLecturer(LecturerGetBindingModel model)
		{
			try
			{
				var entity = _context.Lecturers
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<LecturerViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<LecturerViewModel>.Success(
					ModelFactory.CreateLecturerViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<LecturerViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<LecturerViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateLecturer(LecturerRecordBindingModel model)
		{
			var entity = new Lecturer
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Patronymic = model.Patronymic,
				Abbreviation = model.Abbreviation,
				DateBirth = model.DateBirth,
				Post = model.Post,
				Rank = model.Rank,
				Address = model.Address,
				HomeNumber = model.HomeNumber,
				MobileNumber = model.MobileNumber,
				Email = model.Email,
				Description = model.Description,
				Photo = model.Photo,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.Lecturers.Add(entity);
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

		public ResultService UpdateLecturer(LecturerRecordBindingModel model)
		{
			{
				try
				{
					var entity = _context.Lecturers
									.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
					if (entity == null)
					{
						return ResultService.Error("Error:", "Entity not found",
							ResultServiceStatusCode.NotFound);
					}
					entity.FirstName = model.FirstName;
					entity.LastName = model.LastName;
					entity.Patronymic = model.Patronymic;
					entity.Abbreviation = model.Abbreviation;
					entity.DateBirth = model.DateBirth;
					entity.Post = model.Post;
					entity.Rank = model.Rank;
					entity.Address = model.Address;
					entity.HomeNumber = model.HomeNumber;
					entity.MobileNumber = model.MobileNumber;
					entity.Email = model.Email;
					entity.Description = model.Description;
					entity.Photo = model.Photo;

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

		public ResultService DeleteLecturer(LecturerGetBindingModel model)
		{
			try
			{
				var entity = _context.Lecturers
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
