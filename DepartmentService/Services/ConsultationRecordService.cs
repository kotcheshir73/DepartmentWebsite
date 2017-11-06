using DepartmentDAL;
using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DepartmentService.Services
{
	public class ConsultationRecordService : IConsultationRecordService
	{
		private readonly DepartmentDbContext _context;

		private readonly IScheduleLessonTimeService _serviceSLT;

		public ConsultationRecordService(DepartmentDbContext context, IScheduleLessonTimeService serviceSLT)
		{
			_context = context;
			_serviceSLT = serviceSLT;
		}


		public ResultService<ConsultationRecordViewModel> GetConsultationRecord(ConsultationRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.ConsultationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
					return ResultService<ConsultationRecordViewModel>.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				return ResultService<ConsultationRecordViewModel>.Success(
					ModelFactoryToViewModel.CreateConsultationRecordViewModel(entity));
			}
			catch (DbEntityValidationException ex)
			{
				return ResultService<ConsultationRecordViewModel>.Error(ex,
					ResultServiceStatusCode.Error);
			}
			catch (Exception ex)
			{
				return ResultService<ConsultationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
			}
		}

		public ResultService CreateConsultationRecord(ConsultationRecordRecordBindingModel model)
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
			var result = CheckCreateConsultation(model, ModelFactoryToViewModel.CreateSeasonDatesViewModel(seasonDate));
			if (!result.Succeeded)
			{
				return result;
			}

			var entity = ModelFacotryFromBindingModel.CreateConsultationRecord(model);
			try
			{
				_context.ConsultationRecords.Add(entity);
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

		public ResultService UpdateConsultationRecord(ConsultationRecordRecordBindingModel model)
		{
			try
			{
				var entity = _context.ConsultationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}
				entity = ModelFacotryFromBindingModel.CreateConsultationRecord(model, entity);
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

		public ResultService DeleteConsultationRecord(ConsultationRecordGetBindingModel model)
		{
			try
			{
				var entity = _context.ConsultationRecords
								.FirstOrDefault(e => e.Id == model.Id);
				if (entity == null)
				{
					return ResultService.Error("Error:", "Entity not found",
						ResultServiceStatusCode.NotFound);
				}

				_context.ConsultationRecords.Remove(entity);
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


		public ResultService CheckCreateConsultation(ConsultationRecordRecordBindingModel model, SeasonDatesViewModel seasonDate)
		{
			DateTime[] lessons;
			var dateBeginSemester = Convert.ToDateTime(seasonDate.DateBeginSemester);
			var dateEndSemester = Convert.ToDateTime(seasonDate.DateEndSemester);
			if (dateBeginSemester < model.DateConsultation && dateEndSemester > model.DateConsultation)
			{//консультация назначается в семестре, определяем неделю, день и пару
				int day = ((int)(model.DateConsultation - dateBeginSemester).TotalDays % 14);
				int week = day < 8 ? 0 : 1;
				day = day % 7;
				int lesson = 7;
				var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				var times = result.Result;
				lessons = new DateTime[times.Count];
				for (int i = 0; i < times.Count; ++i)
				{
					lessons[i] = new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day,
						times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
				}

				for (int i = 0; i < lessons.Length - 1; ++i)
				{
					if (lessons[i] >= model.DateConsultation && lessons[i + 1] >= model.DateConsultation)
					{
						lesson = i;
						break;
					}
				}
				var entry = _context.SemesterRecords.FirstOrDefault(sr => sr.Week == week && sr.Day == day && sr.Lesson == lesson &&
																		   sr.ClassroomId == model.ClassroomId && sr.LessonType != LessonTypes.удл);
				if (entry != null)
				{
					return ResultService.Error("Error:", "Exsist SemesterRecord",
						ResultServiceStatusCode.ExsistItem);
				}
				model.Week = week;
				model.Day = day;
				model.Lesson = lesson;
			}
			var dateBeginOffset = Convert.ToDateTime(seasonDate.DateBeginOffset);
			var dateEndOffset = Convert.ToDateTime(seasonDate.DateEndOffset);
			if (dateBeginOffset < model.DateConsultation && dateEndOffset > model.DateConsultation)
			{//консультация ставится на зачетной неделе
				int day = ((int)(model.DateConsultation - dateBeginOffset).TotalDays % 14);
				int week = day < 8 ? 0 : 1;
				day = day % 7;
				int lesson = 7;
				var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				var times = result.Result;
				lessons = new DateTime[times.Count];
				for (int i = 0; i < times.Count; ++i)
				{
					lessons[i] = new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day,
						times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
				}

				for (int i = 0; i < lessons.Length - 1; ++i)
				{
					if (lessons[i] >= model.DateConsultation && lessons[i + 1] >= model.DateConsultation)
					{
						lesson = i;
						break;
					}
				}
				var entry = _context.OffsetRecords.FirstOrDefault(sr => sr.Week == week && sr.Day == day && sr.Lesson == lesson &&
																		   sr.ClassroomId == model.ClassroomId);
				if (entry != null)
				{
					return ResultService.Error("Error:", "Exsist OffsetRecord",
						ResultServiceStatusCode.ExsistItem);
				}
				model.Week = week;
				model.Day = day;
				model.Lesson = lesson;
			}

			var dateBeginExamination = Convert.ToDateTime(seasonDate.DateBeginExamination);
			var dateEndExamination = Convert.ToDateTime(seasonDate.DateEndExamination);
			if (dateBeginExamination < model.DateConsultation && dateEndExamination > model.DateConsultation)
			{//консультация назначается в сессию
				int day = ((int)(model.DateConsultation - dateBeginExamination).TotalDays);
				int lesson = 2;
				var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				var times = result.Result;
				result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
				if (!result.Succeeded)
				{
					return ResultService.Error("Error:", "LessonTime not found",
						ResultServiceStatusCode.NotFound);
				}
				times.AddRange(result.Result);
				lessons = new DateTime[times.Count];
				for (int i = 0; i < times.Count; ++i)
				{
					lessons[i] = new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day,
						times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
				}
				for (int i = 0; i < lessons.Length - 1; ++i)
				{
					if (lessons[i] >= model.DateConsultation && lessons[i + 1] >= model.DateConsultation)
					{
						lesson = i;
						break;
					}
				}

				var entry = _context.ExaminationRecords.FirstOrDefault(sr =>
									 ((sr.DateExamination.Year == model.DateConsultation.Year && sr.DateExamination.Month == model.DateConsultation.Month &&
									 sr.DateExamination.Day == model.DateConsultation.Day &&
									 (sr.DateExamination.Hour >= model.DateConsultation.Hour && sr.DateExamination.Hour + 3 < model.DateConsultation.Hour))
									 //попадает на момент проведения экзамена (3 часа на экзамен)
									 ||
									 (sr.DateConsultation.Year == model.DateConsultation.Year && sr.DateConsultation.Month == model.DateConsultation.Month &&
									 sr.DateConsultation.Day == model.DateConsultation.Day &&
									 (sr.DateConsultation.Hour >= model.DateConsultation.Hour && sr.DateConsultation.Hour + 1 < model.DateConsultation.Hour)))
									 //попадает на момент проведения консультации (1  на консультацию)
									 && sr.ClassroomId == model.ClassroomId);
				if (entry != null)
				{
					return ResultService.Error("Error:", "Exsist ExaminationRecord",
						ResultServiceStatusCode.ExsistItem);
				}
				model.Week = 0;
				model.Day = day;
				model.Lesson = lesson;
			}
			return ResultService.Success();
		}
	}
}
