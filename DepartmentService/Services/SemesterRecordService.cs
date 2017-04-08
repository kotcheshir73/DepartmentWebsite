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
	public class SemesterRecordService : ISemesterRecordService
	{
		private readonly DepartmentDbContext _context;

		public SemesterRecordService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<SemesterRecordViewModel> GetSemesterRecord(SemesterRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.SemesterRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
					return ResultService<SemesterRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<SemesterRecordViewModel>.Success(
					ModelFactory.CreateSemesterRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<SemesterRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<SemesterRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateSemesterRecord(SemesterRecordRecordBindingModel model)
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

			var entry = _context.SemesterRecords.FirstOrDefault(sr => sr.Week == model.Week && sr.Day == model.Day && sr.Lesson == model.Lesson &&
																			(sr.ClassroomId == model.ClassroomId && model.ClassroomId != null) &&
																			(sr.StudentGroupId == model.StudentGroupId && model.StudentGroupId != null) &&
																			sr.LessonType != LessonTypes.удл &&
																			sr.SeasonDatesId == seasonDate.Id);

			if (entry != null)
			{
				return ResultService.Error("Error:", "Exsist SemesterRecord",
					ResultServiceStatusCode.ExsistItem);
			}
			var entity = new SemesterRecord
			{
				Id = model.Id,
				Week = model.Week,
				Day = model.Day,
				Lesson = model.Lesson,
				SeasonDatesId = seasonDate.Id,
				IsStreaming = model.IsStreaming,

				LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType),
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
				_context.SemesterRecords.Add(entity);
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

		public ResultService UpdateSemesterRecord(SemesterRecordRecordBindingModel model)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var entity = _context.SemesterRecords
									.FirstOrDefault(e => e.Id == model.Id);
					if (entity == null)
					{
						return ResultService.Error("Error:", "Entity not found",
							ResultServiceStatusCode.NotFound);
					}
					//Возможность обработки сразу нескольких аналогичных записей
					var entries = _context.SemesterRecords.AsQueryable();
					bool flag = false;
					if (model.ApplyToAnalogRecordsByTextData)
					{
						if (model.ApplyToAnalogRecordsByClassroom)
						{
							entries = entries.Where(e =>
											e.LessonClassroom == entity.LessonClassroom);
							flag = true;
						}
						if (model.ApplyToAnalogRecordsByDiscipline)
						{
							entries = entries.Where(e =>
											e.LessonDiscipline == entity.LessonDiscipline);
							flag = true;
						}
						if (model.ApplyToAnalogRecordsByGroup)
						{
							entries = entries.Where(e =>
											e.LessonGroup == entity.LessonGroup);
							flag = true;
						}
						if (model.ApplyToAnalogRecordsByLecturer)
						{
							entries = entries.Where(e =>
											e.LessonLecturer == entity.LessonLecturer);
							flag = true;
						}
					}
					else
					{
						if (model.ApplyToAnalogRecordsByClassroom)
						{
							entries = entries.Where(e =>
											e.ClassroomId == entity.ClassroomId);
							flag = true;
						}
						//if (model.ApplyToAnalogRecordsByDiscipline)
						//{
						//    entries = entries.Where(e =>
						//                    e. == entity.LessonDiscipline);
						//    flag = true;
						//}
						if (model.ApplyToAnalogRecordsByGroup)
						{
							entries = entries.Where(e =>
											e.StudentGroupId == entity.StudentGroupId);
							flag = true;
						}
						if (model.ApplyToAnalogRecordsByLecturer)
						{
							entries = entries.Where(e =>
											e.LecturerId == entity.LecturerId);
							flag = true;
						}
					}
					if (model.ApplyToAnalogRecordsByLessonType)
					{
						entries = entries.Where(e =>
										e.LessonType == entity.LessonType);
						flag = true;
					}

					if (flag)
					{
						var entriesList = entries.ToList();
						entity = null;
						foreach (var record in entriesList)
						{
							if (model.ApplyToAnalogRecordsByLessonType)
							{
								record.LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType);
							}
							if (model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByClassroom)
							{
								record.LessonClassroom = model.LessonClassroom;
							}
							if (!model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByClassroom)
							{
								record.ClassroomId = model.ClassroomId;
							}
							if (model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByDiscipline)
							{
								record.LessonDiscipline = model.LessonDiscipline;
							}
							if (!model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByDiscipline)
							{
								// record.LessonDiscipline = model.LessonDiscipline;
							}
							if (model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByLecturer)
							{
								record.LessonLecturer = model.LessonLecturer;
							}
							if (!model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByLecturer)
							{
								record.LecturerId = model.LecturerId;
							}
							if (model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByGroup)
							{
								record.LessonGroup = model.LessonGroup;
							}
							if (!model.ApplyToAnalogRecordsByTextData && model.ApplyToAnalogRecordsByGroup)
							{
								record.StudentGroupId = model.StudentGroupId;
							}
							_context.Entry(record).State = System.Data.Entity.EntityState.Modified;
							_context.SaveChanges();
						}
					}
					else
					{
						entity.LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType);
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
					}
					transaction.Commit();
					return ResultService.Success();
				}
				catch (DbEntityValidationException ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return ResultService.Error(ex, ResultServiceStatusCode.Error);
				}
			}
		}

		public ResultService DeleteSemesterRecord(SemesterRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.SemesterRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.SemesterRecords.Remove(entity);
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
