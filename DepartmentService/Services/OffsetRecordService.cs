using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Linq;
using System.Data.Entity.Validation;

namespace DepartmentService.Services
{
	public class OffsetRecordService : IOffsetRecordService
	{
		private readonly DepartmentDbContext _context;

		public OffsetRecordService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<OffsetRecordViewModel> GetOffsetRecord(OffsetRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.OffsetRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
					return ResultService<OffsetRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<OffsetRecordViewModel>.Success(
					ModelFactory.CreateOffsetRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<OffsetRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<OffsetRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateOffsetRecord(OffsetRecordRecordBindingModel model)
		{
			var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
			if (currentSetting == null)
			{
				return ResultService.Error("Error:", "CurrentSetting not found",
					ResultServiceStatusCode.NotFound);
			}
			var seasonDate = _context.SeasonDates.FirstOrDefault(sd => sd.Title == currentSetting.Value);
			if (seasonDate == null)
			{
				return ResultService.Error("Error:", "SeasonDate not found",
					ResultServiceStatusCode.NotFound);
			}

			var entry = _context.OffsetRecords.FirstOrDefault(sr => sr.Week == model.Week && sr.Day == model.Day && sr.Lesson == model.Lesson &&
																			sr.ClassroomId == model.ClassroomId &&
																			sr.SeasonDatesId == seasonDate.Id);

			if (entry != null)
			{
				return ResultService.Error("Error:", "Exsist OffsetRecord",
					ResultServiceStatusCode.ExsistItem);
			}
			var entity = new OffsetRecord
			{
				Id = model.Id,
				Week = model.Week,
				Day = model.Day,
				Lesson = model.Lesson,
				SeasonDatesId = seasonDate.Id,

				LessonDiscipline = model.LessonDiscipline,
				LessonLecturer = model.LessonLecturer,
				LessonGroup = model.LessonGroup,
				LessonClassroom = model.LessonClassroom,

				ClassroomId = model.ClassroomId,
				LecturerId = model.LecturerId,
				StudentGroupId = model.StudentGroupId
			};
			try
			{
				_context.OffsetRecords.Add(entity);
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

		public ResultService UpdateOffsetRecord(OffsetRecordRecordBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var entity = _context.OffsetRecords
									.FirstOrDefault(e => e.Id == model.Id);
					if (entity == null)
					{
						return ResultService.Error("Error:", "Entity not found",
							ResultServiceStatusCode.NotFound);
					}
					entity.LessonDiscipline = model.LessonDiscipline;
					entity.LessonGroup = model.LessonGroup;
					entity.LessonLecturer = model.LessonLecturer;
					entity.LessonClassroom = model.LessonClassroom;
					if (!string.IsNullOrEmpty(model.ClassroomId))
					{
						entity.ClassroomId = model.ClassroomId;
					}
					entity.LecturerId = model.LecturerId;
					entity.StudentGroupId = model.StudentGroupId;
					_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
					_context.SaveChanges();
					transaction.Commit();
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

		public ResultService DeleteOffsetRecord(OffsetRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.OffsetRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.OffsetRecords.Remove(entity);
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
