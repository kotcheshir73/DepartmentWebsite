﻿using DatabaseContext;
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
    public class ExaminationRecordService : IExaminationRecordService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

        private readonly string _entity = "Расписание";

        public ResultService<List<ExaminationRecordShortViewModel>> GetExaminationSchedule(ScheduleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.ExaminationRecords.AsQueryable();

                    if (model.DateBegin.HasValue && model.DateEnd.HasValue)
                    {
                        selectedRecords = selectedRecords.Where(x => 
                                (x.ScheduleDate >= model.DateBegin.Value.Date && x.ScheduleDate <= model.DateEnd.Value.Date.AddDays(1)) ||
                                (x.DateConsultation >= model.DateBegin.Value.Date && x.DateConsultation <= model.DateEnd.Value.Date.AddDays(1)));
                    }

                    if (model.ClassroomId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.ClassroomId == model.ClassroomId.Value || x.ConsultationClassroomId == model.ClassroomId.Value);
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
                        selectedRecords = selectedRecords.Where(x => x.LessonClassroom == model.ClassroomNumber || x.LessonConsultationClassroom == model.ClassroomNumber);
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
                                            .Include(x => x.ConsultationClassroom)
                                            .Include(x => x.Discipline)
                                            .Include(x => x.Lecturer)
                                            .Include(x => x.StudentGroup);

                    selectedRecords = selectedRecords.OrderBy(s => s.ScheduleDate);

                    return ResultService<List<ExaminationRecordShortViewModel>>.Success(selectedRecords.Select(x => x.CreateRecordShortViewModel()).ToList());
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<ExaminationRecordShortViewModel>>.Error(ex, ResultServiceStatusCode.Error);
            }
		}
		
		public ResultService<ExaminationRecordViewModel> GetExaminationRecord(ScheduleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationRecords
                                .Where(x => x.Id == model.Id)
                                .Include(x => x.Classroom)
                                .Include(x => x.ConsultationClassroom)
                                .Include(x => x.Discipline)
                                .Include(x => x.Lecturer)
                                .Include(x => x.StudentGroup)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    return ResultService<ExaminationRecordViewModel>.Success(entity.CreateRecordViewModel());
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateExaminationRecord(ExaminationRecordSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = model.CreateRecord();

                    context.ExaminationRecords.Add(entity);
                    context.SaveChanges();

                    return ResultService.Success(entity.Id);
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService UpdateExaminationRecord(ExaminationRecordSetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationRecords.FirstOrDefault(x => x.Id == model.Id);
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

		public ResultService DeleteExaminationRecord(ScheduleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationRecords.FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }

                    context.ExaminationRecords.Remove(entity);
                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
		}

        public ResultService ClearExaminationRecords(ScheduleGetBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Delete, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.ExaminationRecords.Where(x =>
                                (x.ScheduleDate >= model.DateBegin.Value.Date && x.ScheduleDate <= model.DateEnd.Value.Date.AddDays(1)) ||
                                (x.DateConsultation >= model.DateBegin.Value.Date && x.DateConsultation <= model.DateEnd.Value.Date.AddDays(1)));

                    if (model.ClassroomId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.ClassroomId == model.ClassroomId.Value || x.ConsultationClassroomId == model.ClassroomId.Value);
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
                        selectedRecords = selectedRecords.Where(x => x.LessonClassroom == model.ClassroomNumber || x.LessonConsultationClassroom == model.ClassroomNumber);
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

                    context.ExaminationRecords.RemoveRange(selectedRecords);
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