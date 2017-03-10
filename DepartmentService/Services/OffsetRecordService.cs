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
    public class OffsetRecordService : IOffsetRecordService
    {
        private readonly DepartmentDbContext _context;

        public OffsetRecordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public OffsetRecordViewModel GetOffsetRecord(OffsetRecordGetBindingModel model)
        {
            var entity = _context.OffsetRecords
                            .FirstOrDefault(e => e.Id == model.Id);
            if (entity == null)
                return null;
            return ModelFactory.CreateOffsetRecordViewModel(entity);
        }

        public ResultService CreateOffsetRecord(OffsetRecordRecordBindingModel model)
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

            var entry = _context.OffsetRecords.FirstOrDefault(sr => sr.Week == model.Week && sr.Day == model.Day && sr.Lesson == model.Lesson &&
                                                                            sr.ClassroomId == model.ClassroomId &&
                                                                            sr.SeasonDatesId == seasonDate.Id);

            if (entry != null)
            {
                return ResultService.Error("exsist_item", "На эту пару уже стоит занятие", 401);
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
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
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
                    transaction.Commit();
                    return ResultService.Success();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ResultService.Error("error", ex.Message, 400);
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
                    return ResultService.Error("entity", "not_found", 404);
                }

                _context.OffsetRecords.Remove(entity);
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
