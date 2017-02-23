using DepartmentService.IServices;
using System;
using System.Linq;
using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using DepartmentDAL.Context;
using DepartmentDAL.Models;

namespace DepartmentService.Services
{
    public class ExaminationRecordService : IExaminationRecordService
    {
        private readonly DepartmentDbContext _context;

        public ExaminationRecordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public ExaminationRecordViewModel GetExaminationRecord(ExaminationRecordGetBindingModel model)
        {
            var entity = _context.ExaminationRecords
                            .FirstOrDefault(e => e.Id == model.Id);
            if (entity == null)
                return null;
            return ModelFactory.CreateExaminationRecordViewModel(entity);
        }

        public ResultService CreateExaminationRecord(ExaminationRecordRecordBindingModel model)
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

            var entry = _context.ExaminationRecords.FirstOrDefault(sr => sr.DateConsultation == model.DateConsultation && sr.DateExamination == model.DateExamination &&
                                                                            sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == seasonDate.Id);

            if (entry != null)
            {
                return ResultService.Error("exsist_item", "На эту пару уже стоит занятие", 401);
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
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
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

        public ResultService DeleteExaminationRecord(ExaminationRecordGetBindingModel model)
        {
            try
            {
                var entity = _context.ExaminationRecords
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }

                _context.ExaminationRecords.Remove(entity);
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
