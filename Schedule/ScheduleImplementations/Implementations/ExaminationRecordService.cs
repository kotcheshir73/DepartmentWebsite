using DatabaseContext;
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
    public class ExaminationRecordService : IExaminationRecordService
	{
		private readonly AccessOperation _serviceOperation = AccessOperation.Расписание;

        private readonly string _entity = "Расписание";

        public ResultService<List<ExaminationRecordShortViewModel>> GetExaminationSchedule(ScheduleGetBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.View, _entity);

                var currentDates = model.SeasonDateId ?? DepartmentUserManager.GetCurrentDates().Id;

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.ExaminationRecords.Where(x => x.SeasonDatesId == currentDates);

                    if (!string.IsNullOrEmpty(model.ClassroomNumber))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.LessonClassroom == model.ClassroomNumber || x.LessonConsultationClassroom == model.ClassroomNumber);
                    }
                    if (model.ClassroomId.HasValue)
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_аудитории, AccessType.View, "Расписание аудитории");
                        selectedRecords = selectedRecords.Where(x => x.ClassroomId == model.ClassroomId.Value || x.ConsultationClassroomId == model.ClassroomId.Value);
                    }
                    if (!string.IsNullOrEmpty(model.StudentGroupName))
                    {
                        DepartmentUserManager.CheckAccess(AccessOperation.Расписание_группы, AccessType.View, "Расписание групп");
                        selectedRecords = selectedRecords.Where(x => x.LessonGroup == model.StudentGroupName);
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
                                            .Include(x => x.ConsultationClassroom)
                                            .Include(x => x.Discipline)
                                            .Include(x => x.Lecturer)
                                            .Include(x => x.StudentGroup);

                    var records = selectedRecords.ToList();
                    List<ExaminationRecordShortViewModel> result = new List<ExaminationRecordShortViewModel>();
                    for (int i = 0; i < records.Count; ++i)
                    {
                        result.Add(ScheduleModelFactoryToViewModel.CreateExaminationRecordShortViewModel(records[i]));
                    }

                    return ResultService<List<ExaminationRecordShortViewModel>>.Success(result.OrderBy(x => x.Id).ToList());
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
                                .Include(x => x.Classroom).Include(x => x.ConsultationClassroom).Include(x => x.Discipline).Include(x => x.Lecturer).Include(x => x.StudentGroup)
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService<ExaminationRecordViewModel>.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    return ResultService<ExaminationRecordViewModel>.Success(ScheduleModelFactoryToViewModel.CreateExaminationRecordViewModel(entity));
                }
            }
            catch (Exception ex)
            {
                return ResultService<ExaminationRecordViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
		}

		public ResultService CreateExaminationRecord(ExaminationRecordRecordBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                var seasonDate = DepartmentUserManager.GetCurrentDates();

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entry = context.ExaminationRecords.FirstOrDefault(x => x.DateConsultation == model.DateConsultation && x.DateExamination == model.DateExamination &&
                                                                                x.ClassroomId == model.ClassroomId && x.SeasonDatesId == seasonDate.Id);

                    if (entry != null)
                    {
                        return ResultService.Error("Error:", "Запись уже существует", ResultServiceStatusCode.ExsistItem);
                    }
                    var entity = ScheduleModelFacotryFromBindingModel.CreateExaminationRecord(model, seasonDate: seasonDate);

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

		public ResultService UpdateExaminationRecord(ExaminationRecordRecordBindingModel model)
		{
            try
            {
                DepartmentUserManager.CheckAccess(_serviceOperation, AccessType.Change, _entity);

                using (var context = DepartmentUserManager.GetContext)
                {
                    var entity = context.ExaminationRecords
                                .FirstOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return ResultService.Error("Error:", "Элемент не найден", ResultServiceStatusCode.NotFound);
                    }
                    entity = ScheduleModelFacotryFromBindingModel.CreateExaminationRecord(model, entity);
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
                    var entity = context.ExaminationRecords
                                .FirstOrDefault(x => x.Id == model.Id);
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

                var currentDates = model.SeasonDateId ?? DepartmentUserManager.GetCurrentDates().Id;

                using (var context = DepartmentUserManager.GetContext)
                {
                    var selectedRecords = context.ExaminationRecords.Where(x => x.SeasonDatesId == currentDates);

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