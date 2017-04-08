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
	public class StreamingLessonService : IStreamingLessonService
	{
		private readonly DepartmentDbContext _context;

		public StreamingLessonService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<List<StreamingLessonViewModel>> GetStreamingLessons()
		{
			try
			{
				return ResultService<List<StreamingLessonViewModel>>.Success(
					ModelFactory.CreateStreamingLessons(_context.StreamingLessons
							.Where(e => !e.IsDeleted))
					.ToList());
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<List<StreamingLessonViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<List<StreamingLessonViewModel>>.Error(ex,
					ResultServiceStatusCode.Error);
			}
		}

		public ResultService<StreamingLessonViewModel> GetStreamingLesson(StreamingLessonGetBindingModel model)
		{
			try
			{
				var entity = _context.StreamingLessons
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
					return ResultService<StreamingLessonViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);

				return ResultService<StreamingLessonViewModel>.Success(
					ModelFactory.CreateStreamingLessonViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<StreamingLessonViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<StreamingLessonViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateStreamingLesson(StreamingLessonRecordBindingModel model)
		{
			var entity = new StreamingLesson
			{
				Id = model.Id,
				IncomingGroups = model.IncomingGroups,
				StreamName = model.StreamName,
				DateCreate = DateTime.Now,
				IsDeleted = false
			};
			try
			{
				_context.StreamingLessons.Add(entity);
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

		public ResultService UpdateStreamingLesson(StreamingLessonRecordBindingModel model)
		{
			try
			{
				var entity = _context.StreamingLessons
								.FirstOrDefault(e => e.Id == model.Id && !e.IsDeleted);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity.IncomingGroups = model.IncomingGroups;
				entity.StreamName = model.StreamName;

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

		public ResultService DeleteStreamingLesson(StreamingLessonGetBindingModel model)
		{
			try
			{
				var entity = _context.StreamingLessons
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
