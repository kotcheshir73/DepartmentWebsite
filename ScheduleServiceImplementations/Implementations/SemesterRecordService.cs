using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService;
using DepartmentService.BindingModels;
using ScheduleServiceImplementations.Helpers;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.Interfaces;
using ScheduleServiceInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace ScheduleServiceImplementations.Services
{
    public class SemesterRecordService : ISemesterRecordService
    {
        private readonly DepartmentDbContext _context;

        private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

        public SemesterRecordService(DepartmentDbContext context)
        {
            _context = context;

        }

        public ResultService<List<SemesterRecordShortViewModel>> GetSemesterSchedule(ScheduleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по расписанию");
                }

                var currentDates = model.SeasonDateId ?? ScheduleHelper.GetCurrentDates().Id;

                var selectedRecords = _context.SemesterRecords.Where(sr => sr.SeasonDatesId == currentDates);

                if (model.IsFirstHalfSemester.HasValue)
                {
                    selectedRecords = selectedRecords.Where(sr => sr.IsFirstHalfSemester == model.IsFirstHalfSemester.Value);
                }
                if (!string.IsNullOrEmpty(model.ClassroomNumber))
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.LessonClassroom == model.ClassroomNumber);
                }
                if (!string.IsNullOrEmpty(model.DisciplineName))
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию дисциплин");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.LessonDiscipline == model.DisciplineName);
                }
                if (!string.IsNullOrEmpty(model.StudentGroupName))
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию групп");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.LessonGroup == model.StudentGroupName);
                }
                if (model.ClassroomId.HasValue)
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.ClassroomId == model.ClassroomId.Value);
                }
                if (model.StudentGroupId.HasValue)
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию группы");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.StudentGroupId == model.StudentGroupId.Value);
                }
                if (model.LecturerId.HasValue)
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию преподавателей");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.LecturerId == model.LecturerId.Value);
                }
                if (model.DisciplineId.HasValue)
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию дисциплины");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.DisciplineId == model.DisciplineId.Value);
                }

                selectedRecords = selectedRecords
                                        .Include(sr => sr.Classroom)
                                        .Include(sr => sr.Discipline)
                                        .Include(sr => sr.Lecturer)
                                        .Include(sr => sr.StudentGroup);

                var records = selectedRecords.OrderBy(s => s.Week).ThenBy(s => s.Day).ThenBy(s => s.Lesson).ToList();

                List<SemesterRecordShortViewModel> result = new List<SemesterRecordShortViewModel>();
                for (int i = 0; i < records.Count; ++i)
                {
                    string groups = ScheduleHelper.GetLessonGroup(records[i]);
                    if (records[i].IsStreaming && (!string.IsNullOrEmpty(model.ClassroomNumber) || model.LecturerId.HasValue))
                    {//если потоковая пара
                        var recs = records.Where(rec => rec.Week == records[i].Week && rec.Day == records[i].Day && rec.Lesson == records[i].Lesson &&
                                                rec.LessonClassroom == records[i].LessonClassroom && rec.IsStreaming).ToList();
                        StringBuilder sb = new StringBuilder();
                        foreach (var rec in recs)
                        {
                            sb.Append(rec.LessonGroup + ";");
                            if (records[i] != rec)
                            {
                                records.Remove(rec);
                            }
                        }
                        groups = sb.Remove(sb.Length - 1, 1).ToString();
                        //пытаемся найти запись о потоковом занятии
                        var streamingLesson = _context.StreamingLessons.FirstOrDefault(sl => sl.IncomingGroups == groups);
                        if (streamingLesson != null)
                        {
                            groups = streamingLesson.StreamName;
                        }
                        else
                        {
                            var entity = ModelFacotryFromBindingModel.CreateStreamingLesson(new StreamingLessonSetBindingModel
                            {
                                IncomingGroups = groups,
                                StreamName = groups
                            });

                            _context.StreamingLessons.Add(entity);
                            _context.SaveChanges();
                        }
                    }

                    if (records[i].LessonType == LessonTypes.удл)
                    {//не выводим занятие, если оно удаленное и в эту пару поставили пару
                        var recordExists = records.Exists(r => r.Week == records[i].Week && r.Day == records[i].Day && r.Lesson == records[i].Lesson &&
                                                        r.LessonType != LessonTypes.удл);
                        if (recordExists)
                        {
                            records.Remove(records[i--]);
                            continue;
                        }
                    }

                    result.Add(ScheduleModelFactoryToViewModel.CreateSemesterRecordShortViewModel(records[i], groups));
                }

                return ResultService<List<SemesterRecordShortViewModel>>.Success(result.OrderBy(e => e.Id).ToList());
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<List<SemesterRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<List<SemesterRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<SemesterRecordViewModel> GetSemesterRecord(ScheduleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.View))
                {
                    throw new Exception("Нет доступа на чтение данных по расписанию");
                }

                var entity = _context.SemesterRecords
                                .Where(sr => sr.Id == model.Id)
                                .Include(sr => sr.Classroom).Include(sr => sr.Discipline).Include(sr => sr.Lecturer).Include(sr => sr.StudentGroup)
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService<SemesterRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                return ResultService<SemesterRecordViewModel>.Success(ScheduleModelFactoryToViewModel.CreateSemesterRecordViewModel(entity));
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<SemesterRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<SemesterRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService CreateSemesterRecord(SemesterRecordRecordBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по расписанию");
                }

                var seasonDate = ScheduleHelper.GetCurrentDates();

                var entry = _context.SemesterRecords.FirstOrDefault(sr => sr.Week == model.Week && sr.Day == model.Day && sr.Lesson == model.Lesson &&
                                                                                (sr.ClassroomId == model.ClassroomId && model.ClassroomId != null) &&
                                                                                (sr.StudentGroupId == model.StudentGroupId && model.StudentGroupId != null) &&
                                                                                sr.LessonType != LessonTypes.удл &&
                                                                                sr.SeasonDatesId == seasonDate.Id);

                if (entry != null)
                {
                    return ResultService.Error("Error:", "Exsist SemesterRecord", ResultServiceStatusCode.ExsistItem);
                }

                var entity = ScheduleModelFacotryFromBindingModel.CreateSemesterRecord(model, seasonDate: seasonDate);

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
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Change))
                {
                    throw new Exception("Нет доступа на изменение данных по расписанию");
                }

                var entity = _context.SemesterRecords
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }

                entity = ScheduleModelFacotryFromBindingModel.CreateSemesterRecord(model, entity);
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

        public ResultService DeleteSemesterRecord(ScheduleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по расписанию");
                }

                var entity = _context.SemesterRecords
                                .FirstOrDefault(e => e.Id == model.Id);
                if (entity == null)
                {
                    return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                }
                entity.LessonType = LessonTypes.удл;
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

        public ResultService ClearSemesterRecords(ScheduleGetBindingModel model)
        {
            try
            {
                if (!AccessCheckService.CheckAccess(_serviceOperation, AccessType.Delete))
                {
                    throw new Exception("Нет доступа на удаление данных по расписанию");
                }

                var currentDates = model.SeasonDateId ?? ScheduleHelper.GetCurrentDates().Id;

                var selectedRecords = _context.SemesterRecords.Where(sr => sr.SeasonDatesId == currentDates);

                if (model.IsFirstHalfSemester.HasValue)
                {
                    selectedRecords = selectedRecords.Where(sr => sr.IsFirstHalfSemester == model.IsFirstHalfSemester.Value);
                }

                if (!string.IsNullOrEmpty(model.ClassroomNumber))
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию аудиторий");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.LessonClassroom == model.ClassroomNumber);
                }
                if (!string.IsNullOrEmpty(model.StudentGroupName))
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_группы, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию групп");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.LessonGroup == model.StudentGroupName);
                }
                if (model.LecturerId.HasValue)
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию преподавателей");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.LecturerId == model.LecturerId.Value);
                }
                if (model.DisciplineId.HasValue)
                {
                    if (!AccessCheckService.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View))
                    {
                        throw new Exception("Нет доступа на чтение данных по расписанию дисциплины");
                    }
                    selectedRecords = selectedRecords.Where(sr => sr.DisciplineId == model.DisciplineId.Value);
                }

                _context.SemesterRecords.RemoveRange(selectedRecords);
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