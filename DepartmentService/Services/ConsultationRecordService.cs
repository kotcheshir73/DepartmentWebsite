using DepartmentService.IServices;
using System;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;
using DepartmentDAL.Enums;

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

        public ConsultationRecordViewModel GetConsultationRecord(ConsultationRecordGetBindingModel model)
        {
            var entity = _context.ConsultationRecords
                            .FirstOrDefault(e => e.Id == model.Id);
            if (entity == null)
                return null;
            return ModelFactory.CreateConsultationRecordViewModel(entity);
        }

        public ResultService CreateConsultationRecord(ConsultationRecordRecordBindingModel model)
        {
            var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
            if (currentSetting == null)
            {
                return ResultService.Error("error", "currentSetting not found", 404);
            }
            var seasonDate = _context.SeasonDates.FirstOrDefault(sd => sd.Title == currentSetting.Value);
            if (seasonDate == null)
            {
                return ResultService.Error("error", "seasonDate not found", 404);
            }
            var result = CheckCreateConsultation(model, seasonDate);
            if(!result.Succeeded)
            {
                return result;
            }

            var entity = new ConsultationRecord
            {
                Id = model.Id,
                DateConsultation = model.DateConsultation,
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
                _context.ConsultationRecords.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
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
                    return ResultService.Error("entity", "not_found", 404);
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
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
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
                    return ResultService.Error("entity", "not_found", 404);
                }

                _context.ConsultationRecords.Remove(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService CheckCreateConsultation(ConsultationRecordRecordBindingModel model, SeasonDates seasonDate)
        {
            DateTime[] lessons;
            if (seasonDate.DateBeginSemester < model.DateConsultation && seasonDate.DateEndSemester > model.DateConsultation)
            {//консультация назначается в семестре, определяем неделю, день и пару
                int day = ((int)(model.DateConsultation - seasonDate.DateBeginSemester).TotalDays % 14);
                int week = day < 8 ? 0 : 1;
                day = day % 7;
                int lesson = 7;
                var times = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
                lessons = new DateTime[times.Count];
                for(int i = 0; i < times.Count; ++i)
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
                    return ResultService.Error("exsist_item", "На эту пару уже стоит занятие", 401);
                }
                model.Week = week;
                model.Day = day;
                model.Lesson = lesson;
            }
            if (seasonDate.DateBeginOffset < model.DateConsultation && seasonDate.DateEndOffset > model.DateConsultation)
            {//консультация ставится на зачетной неделе
                int day = ((int)(model.DateConsultation - seasonDate.DateBeginOffset).TotalDays % 14);
                int week = day < 8 ? 0 : 1;
                day = day % 7;
                int lesson = 7;
                var times = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
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
                    return ResultService.Error("exsist_item", "На эту пару уже стоит зачет", 401);
                }
                model.Week = week;
                model.Day = day;
                model.Lesson = lesson;
            }

            if (seasonDate.DateBeginExamination < model.DateConsultation && seasonDate.DateEndExamination > model.DateConsultation)
            {//консультация назначается в сессию
                int day = ((int)(model.DateConsultation - seasonDate.DateBeginExamination).TotalDays);
                int lesson = 2;
                var times = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
                times.AddRange(_serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" }));
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
                    return ResultService.Error("exsist_item", "На эту пару уже стоит экзамен/консультация", 401);
                }
                model.Week = 0;
                model.Day = day;
                model.Lesson = lesson;
            }
            return ResultService.Success();
        }
    }
}
