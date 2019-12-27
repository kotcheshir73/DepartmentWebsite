using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace ScheduleImplementations.Services
{
    public class ConsultationRecordService : IConsultationRecordService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

        private readonly string _entity = "Расписание";

        public ResultService<List<ConsultationRecordShortViewModel>> GetConsultationSchedule(ScheduleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.ConsultationRecords.AsQueryable();

                    if (model.DateBegin.HasValue && model.DateEnd.HasValue)
                    {
                        selectedRecords = selectedRecords.Where(x => x.ScheduleDate >= model.DateBegin.Value.Date &&
                                                                    x.ScheduleDate <= model.DateEnd.Value.Date.AddDays(1));
                    }

                    if (model.ClassroomId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.ClassroomId == model.ClassroomId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View, "Расписание дисциплины");
                        selectedRecords = selectedRecords.Where(x => x.DisciplineId == model.DisciplineId.Value);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View, "Расписание преподавателей");
                        selectedRecords = selectedRecords.Where(x => x.LecturerId == model.LecturerId.Value);
                    }
                    if (model.StudentGroupId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.StudentGroupId == model.StudentGroupId.Value);
                    }

                    if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.LessonClassroom == model.ClassroomNumber);
                    }
                    if (!string.IsNullOrEmpty(model.DisciplineName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.LessonDiscipline == model.DisciplineName);
                    }
                    if (!string.IsNullOrEmpty(model.LecturerName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.LessonLecturer == model.LecturerName);
                    }
                    if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.LessonStudentGroup == model.StudentGroupName);
                    }

                    selectedRecords = selectedRecords
                                            .Include(x => x.Classroom)
                                            .Include(x => x.Discipline)
                                            .Include(x => x.Lecturer)
                                            .Include(x => x.StudentGroup);

                    selectedRecords = selectedRecords.OrderBy(s => s.ScheduleDate);

                    return ResultService<List<ConsultationRecordShortViewModel>>.Success(selectedRecords.Select(x => x.CreateRecordShortViewModel()).ToList());
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<ConsultationRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService<ConsultationRecordViewModel> GetConsultationRecord(ScheduleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ConsultationRecords
                                .Where(x => x.Id == model.Id)
                                .Include(x => x.Classroom)
                                .Include(x => x.Discipline)
                                .Include(x => x.Lecturer)
                                .Include(x => x.StudentGroup)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ConsultationRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    return ResultService<ConsultationRecordViewModel>.Success(entity.CreateRecordViewModel());
                }
            }
            catch (Exception ex)
            {
                return ResultService<ConsultationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateConsultationRecord(ConsultationRecordSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = model.CreateRecord();

                    context.ConsultationRecords.Add(entity);
                    context.SaveChanges();

                    return ResultService.Success(entity.Id);
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService UpdateConsultationRecord(ConsultationRecordSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ConsultationRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    entity = model.CreateRecord(entity);
                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService DeleteConsultationRecord(ScheduleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ConsultationRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    context.ConsultationRecords.Remove(entity);
                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

        public ResultService ClearConsultationRecords(ScheduleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.ConsultationRecords.Where(x => 
                    x.ScheduleDate >= model.DateBegin.Value.Date &&
                                           x.ScheduleDate <= model.DateEnd.Value.Date.AddDays(1));

                    if (model.ClassroomId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.ClassroomId == model.ClassroomId.Value);
                    }
                    if (model.DisciplineId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_дисциплины, AccessType.View, "Расписание дисциплины");
                        selectedRecords = selectedRecords.Where(x => x.DisciplineId == model.DisciplineId.Value);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_преподаватели, AccessType.View, "Расписание преподавателей");
                        selectedRecords = selectedRecords.Where(x => x.LecturerId == model.LecturerId.Value);
                    }
                    if (model.StudentGroupId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.StudentGroupId == model.StudentGroupId.Value);
                    }

                    if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.LessonClassroom == model.ClassroomNumber);
                    }
                    if (!string.IsNullOrEmpty(model.DisciplineName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.LessonDiscipline == model.DisciplineName);
                    }
                    if (!string.IsNullOrEmpty(model.LecturerName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.LessonLecturer == model.LecturerName);
                    }
                    if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.LessonStudentGroup == model.StudentGroupName);
                    }

                    context.ConsultationRecords.RemoveRange(selectedRecords);
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