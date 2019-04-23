using Enums;
using Microsoft.EntityFrameworkCore;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace ScheduleImplementations.Services
{
    public class SemesterRecordService : ISemesterRecordService
    {
        private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

        private readonly string _entity = "Расписание";

        public ResultService<List<SemesterRecordShortViewModel>> GetSemesterSchedule(ScheduleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                var currentDates = model.SeasonDateId ?? ScheduleHelper.GetCurrentDates().Id;

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.SemesterRecords.Where(x => x.SeasonDatesId == currentDates);

                    if (model.IsFirstHalfSemester.HasValue)
                    {
                        selectedRecords = selectedRecords.Where(x => x.IsFirstHalfSemester == model.IsFirstHalfSemester.Value);
                    }
                    if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.LessonClassroom == model.ClassroomNumber);
                    }
                    if (!string.IsNullOrEmpty(model.DisciplineName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View, "Расписание дисциплины");
                        selectedRecords = selectedRecords.Where(x => x.LessonDiscipline == model.DisciplineName);
                    }
                    if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.LessonGroup == model.StudentGroupName);
                    }
                    if (model.ClassroomId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.ClassroomId == model.ClassroomId.Value);
                    }
                    if (model.StudentGroupId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.StudentGroupId == model.StudentGroupId.Value);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View, "Расписание преподавателей");
                        selectedRecords = selectedRecords.Where(x => x.LecturerId == model.LecturerId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View, "Расписание дисциплины");
                        selectedRecords = selectedRecords.Where(x => x.DisciplineId == model.DisciplineId.Value);
                    }

                    selectedRecords = selectedRecords
                                            .Include(x => x.Classroom)
                                            .Include(x => x.Discipline)
                                            .Include(x => x.Lecturer)
                                            .Include(x => x.StudentGroup);

                    var records = selectedRecords.OrderBy(s => s.Week).ThenBy(s => s.Day).ThenBy(s => s.Lesson).ToList();

                    List<SemesterRecordShortViewModel> result = new List<SemesterRecordShortViewModel>();
                    for (int i = 0; i < records.Count; ++i)
                    {
                        if (records[i].LessonType == LessonTypes.удл)
                        {//не выводим занятие, если оно удаленное и в эту пару поставили пару
                            var recordExists = records.Exists(r => r.Week == records[i].Week && r.Day == records[i].Day && r.Lesson == records[i].Lesson &&
                                                            r.LessonType != LessonTypes.удл);
                            if (recordExists)
                            {
                                continue;
                            }
                        }

                        result.Add(ScheduleModelFactoryToViewModel.CreateSemesterRecordShortViewModel(records[i]));
                    }

                    return ResultService<List<SemesterRecordShortViewModel>>.Success(result.OrderBy(x => x.Id).ToList());
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SemesterRecords
                                .Where(x => x.Id == model.Id)
                                .Include(x => x.Classroom).Include(x => x.Discipline).Include(x => x.Lecturer).Include(x => x.StudentGroup)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<SemesterRecordViewModel>.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<SemesterRecordViewModel>.Success(ScheduleModelFactoryToViewModel.CreateSemesterRecordViewModel(entity));
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                var seasonDate = ScheduleHelper.GetCurrentDates();

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entry = context.SemesterRecords.FirstOrDefault(x => x.Week == model.Week && x.Day == model.Day && x.Lesson == model.Lesson &&
                                                                                (x.ClassroomId == model.ClassroomId && model.ClassroomId != null) &&
                                                                                (x.StudentGroupId == model.StudentGroupId && model.StudentGroupId != null) &&
                                                                                x.LessonType != LessonTypes.удл &&
                                                                                x.SeasonDatesId == seasonDate.Id);

                    if (entry != null)
                    {
                        return ResultService.Error("Error:", "Exsist SemesterRecord", ResultServiceStatusCode.ExsistItem);
                    }

                    var entity = ScheduleModelFacotryFromBindingModel.CreateSemesterRecord(model, seasonDate: seasonDate);

                    context.SemesterRecords.Add(entity);
                    context.SaveChanges();

                    return ResultService.Success(entity.Id);
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SemesterRecords
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }

                    entity = ScheduleModelFacotryFromBindingModel.CreateSemesterRecord(model, entity);
                    context.SaveChanges();

                    return ResultService.Success();
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.SemesterRecords
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Entity not found", ResultServiceStatusCode.NotFound);
                    }
                    entity.LessonType = LessonTypes.удл;
                    context.SaveChanges();

                    return ResultService.Success();
                }
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
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                var currentDates = model.SeasonDateId ?? ScheduleHelper.GetCurrentDates().Id;

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.SemesterRecords.Where(x => x.SeasonDatesId == currentDates);

                    if (model.IsFirstHalfSemester.HasValue)
                    {
                        selectedRecords = selectedRecords.Where(x => x.IsFirstHalfSemester == model.IsFirstHalfSemester.Value);
                    }

                    if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.Delete, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.LessonClassroom == model.ClassroomNumber);
                    }
                    if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.Delete, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.LessonGroup == model.StudentGroupName);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.Delete, "Расписание преподавателей");
                        selectedRecords = selectedRecords.Where(x => x.LecturerId == model.LecturerId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.Delete, "Расписание дисциплины");
                        selectedRecords = selectedRecords.Where(x => x.DisciplineId == model.DisciplineId.Value);
                    }

                    context.SemesterRecords.RemoveRange(selectedRecords);
                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}