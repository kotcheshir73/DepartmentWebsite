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
    public class SemesterRecordService : ISemesterRecordService
    {
        private readonly DepartmentDbContext _context;

        public SemesterRecordService(DepartmentDbContext context)
        {
            _context = context;
        }

        public SemesterRecordViewModel GetSemesterRecord(SemesterRecordGetBindingModel model)
        {
            var entity = _context.SemesterRecords
                            .FirstOrDefault(e => e.Id == model.Id);
            if (entity == null)
                return null;
            return ModelFactory.CreateSemesterRecordViewModel(entity);
        }

        public ResultService CreateSemesterRecord(SemesterRecordRecordBindingModel model)
        {
            var entity = new SemesterRecord
            {
                Id = model.Id,
                Week = model.Week,
                Day = model.Day,
                Lesson = model.Lesson,
                LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType),
                ClassroomId = model.ClassroomId,
                LessonDiscipline = model.LessonDiscipline,
                LessonGroupName = model.LessonGroupName,
                LessonTeacher = model.LessonTeacher,
                StudentGroupId = model.StudentGroupId
            };
            try
            {
                _context.SemesterRecords.Add(entity);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
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
                        return ResultService.Error("entity", "not_found", 404);
                    }
                    if (model.ApplyToAnalogRecords)
                    {
                        var entries = _context.SemesterRecords.Where(e =>
                                        e.LessonDiscipline == entity.LessonDiscipline &&
                                        e.LessonGroupName == entity.LessonGroupName &&
                                        e.LessonTeacher == entity.LessonTeacher &&
                                        e.LessonType == entity.LessonType);
                        foreach (var entr in entries)
                        {
                            entr.LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType);
                            entr.ClassroomId = model.ClassroomId;
                            entr.LessonDiscipline = model.LessonDiscipline;
                            entr.LessonGroupName = model.LessonGroupName;
                            entr.LessonTeacher = model.LessonTeacher;
                            entr.StudentGroupId = model.StudentGroupId;
                            _context.Entry(entr).State = System.Data.Entity.EntityState.Modified;
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        entity.Lesson = model.Lesson;
                        entity.LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType);
                        entity.ClassroomId = model.ClassroomId;
                        entity.LessonDiscipline = model.LessonDiscipline;
                        entity.LessonGroupName = model.LessonGroupName;
                        entity.LessonTeacher = model.LessonTeacher;
                        entity.StudentGroupId = model.StudentGroupId;
                        _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        _context.SaveChanges();
                    }
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

        public ResultService DeleteSemesterRecord(SemesterRecordGetBindingModel model)
        {
            try
            {
                var entity = _context.SemesterRecords
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("entity", "not_found", 404);
                }

                _context.SemesterRecords.Remove(entity);
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
