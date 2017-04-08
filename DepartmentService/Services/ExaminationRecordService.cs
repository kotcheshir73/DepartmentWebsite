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
	public class ExaminationRecordService : IExaminationRecordService
	{
		private readonly DepartmentDbContext _context;

		public ExaminationRecordService(DepartmentDbContext context)
		{
			_context = context;
		}


		public ResultService<ExaminationRecordViewModel> GetExaminationRecord(ExaminationRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.ExaminationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
					return ResultService<ExaminationRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<ExaminationRecordViewModel>.Success(
					ModelFactory.CreateExaminationRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ExaminationRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ExaminationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateExaminationRecord(ExaminationRecordRecordBindingModel model)
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

			var entry = _context.ExaminationRecords.FirstOrDefault(sr => sr.DateConsultation == model.DateConsultation && sr.DateExamination == model.DateExamination &&
																			sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == seasonDate.Id);

			if (entry != null)
			{
				return ResultService.Error("Error:", "Exsist ExaminationRecord",
					ResultServiceStatusCode.ExsistItem);
			}
			var entity = new ExaminationRecord
			{
				Id = model.Id,
				DateConsultation = model.DateConsultation,
				DateExamination = model.DateExamination,
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
				_context.ExaminationRecords.Add(entity);
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

		public ResultService UpdateExaminationRecord(ExaminationRecordRecordBindingModel model)
		{
			try
			{
				var entity = _context.ExaminationRecords
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

		public ResultService DeleteExaminationRecord(ExaminationRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.ExaminationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.ExaminationRecords.Remove(entity);
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
