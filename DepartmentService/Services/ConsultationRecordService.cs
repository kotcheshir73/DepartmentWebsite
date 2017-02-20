using DepartmentService.IServices;
using System;
using System.Collections.Generic;
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

        private readonly IClassroomService _serviceC;

        public ConsultationRecordService(DepartmentDbContext context, IClassroomService serviceC)
        {
            _context = context;
            _serviceC = serviceC;
        }

        public List<ClassroomViewModel> GetClassrooms()
        {
            return _serviceC.GetClassrooms();
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

            if (seasonDate.DateBeginSemester < model.DateConsultation && seasonDate.DateEndSemester > model.DateConsultation)
            {//консультация назначается в семестре, определяем неделю, день и пару
                int day = ((int)(model.DateConsultation - seasonDate.DateBeginSemester).TotalDays % 14);
                int week = day < 8 ? 0 : 1;
                day = day % 7;
                int lesson = 7;
                DateTime[] lessons = new DateTime[]
                {
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 8, 0, 0),
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 9, 40, 0),
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 11, 30, 0),
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 13, 10, 0),
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 14, 50, 0),
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 16, 30, 0),
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 18, 10, 0),
                    new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day, 19, 50, 0)
                };
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
                entity.ClassroomId = model.ClassroomId;
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
    }
}
