﻿using DepartmentContext;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.Enums;
using DepartmentService.IServices;
using DepartmentService.ViewModels;
using ScheduleServiceImplementations.Helpers;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.Interfaces;
using ScheduleServiceInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace ScheduleServiceImplementations.Services
{
    public class ScheduleProcess : IScheduleProcess
    {
        private readonly DepartmentDbContext _context;

        private readonly IClassroomService _serviceC;

        private readonly IDisciplineService _serviceD;

        private readonly ILecturerService _serviceL;

        private readonly IStudentGroupService _serviceG;

        private readonly ISeasonDatesService _serviceSD;

        private readonly IScheduleLessonTimeService _serviceSLT;

        private readonly IStreamingLessonService _serviceSL;

        private readonly ISemesterRecordService _serviceSR;

        private readonly IOffsetRecordService _serviceOR;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        public ScheduleProcess(DepartmentDbContext context, IClassroomService serviceC, IDisciplineService serviceD, ILecturerService serviceL, IStudentGroupService serviceG,
            ISeasonDatesService serviceSD, IScheduleLessonTimeService serviceSLT, IStreamingLessonService serviceSL,
            ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
            _context = context;
            _serviceC = serviceC;
            _serviceD = serviceD;
            _serviceL = serviceL;
            _serviceG = serviceG;
            _serviceSD = serviceSD;
            _serviceSLT = serviceSLT;
            _serviceSL = serviceSL;
            _serviceSR = serviceSR;
            _serviceOR = serviceOR;
            _serviceER = serviceER;
            _serviceCR = serviceCR;
        }

        public ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model)
        {
            model.NotUseInSchedule = false;
            return _serviceC.GetClassrooms(model);
        }

        public ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model)
        {
            return _serviceD.GetDisciplines(model);
        }

        public ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model)
        {
            return _serviceL.GetLecturers(model);
        }

        public ResultService<StudentGroupPageViewModel> GetStudentGroups(StudentGroupGetBindingModel model)
        {
            return _serviceG.GetStudentGroups(model);
        }

        public ResultService<SeasonDatesPageViewModel> GetSeasonDaties(SeasonDatesGetBindingModel model)
        {
            return _serviceSD.GetSeasonDaties(model);
        }

        public ResultService<ScheduleLessonTimePageViewModel> GetScheduleLessonTimes(ScheduleLessonTimeGetBindingModel model)
        {
            return _serviceSLT.GetScheduleLessonTimes(model);
        }

        public ResultService<SeasonDatesViewModel> GetCurrentDates()
        {
            try
            {
                var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
                if (currentSetting == null)
                {
                    var seasonDates = _serviceSD.GetSeasonDaties(new SeasonDatesGetBindingModel());
                    if (seasonDates.Succeeded)
                    {
                        currentSetting = new DepartmentModel.Models.CurrentSettings { Key = "Даты семестра", Value = seasonDates.Result.List[0].Title };
                        _context.CurrentSettings.Add(currentSetting);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return ResultService<SeasonDatesViewModel>.Error("Error:", "CurrentSetting not found", ResultServiceStatusCode.NotFound);
                    }
                }
                return _serviceSD.GetSeasonDates(new SeasonDatesGetBindingModel { Title = currentSetting.Value });
            }
            catch (DbEntityValidationException ex)
            {
                return ResultService<SeasonDatesViewModel>.Error(ex,
                    ResultServiceStatusCode.Error);
            }
            catch (Exception ex)
            {
                return ResultService<SeasonDatesViewModel>.Error(ex,
                    ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateCurrentDates(SeasonDatesGetBindingModel model)
        {
            try
            {
                var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
                if (currentSetting == null)
                {
                    return null;
                }

                currentSetting.Value = model.Title;
                _context.Entry(currentSetting).State = EntityState.Modified;
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

        public ResultService<ScheduleRecordsForDisciplinePageViewModel> GetScheduleRecordsForDiciplinePageViewModel(ScheduleRecordsForDiciplineBindingModel model)
        {
            List<ScheduleRecordsForDisciplineViewModel> list = new List<ScheduleRecordsForDisciplineViewModel>();
            var modelGet = new ScheduleGetBindingModel { DisciplineId = model.DisciplineId, SeasonDateId = model.SeasonDateId };
            var semesters = _serviceSR.GetSemesterSchedule(modelGet);
            var days = new[] { "Пн.", "Вт.", "Ср.", "Чт.", "Пт.", "Сб." };//дни недели
            if (semesters.Succeeded)
            {
                foreach (var rec in semesters.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("{0} нед., {1} {2} пара", rec.Week + 1, days[rec.Day], rec.Lesson + 1),
                        LessonType = rec.LessonType,
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var offsets = _serviceOR.GetOffsetSchedule(modelGet);
            if (offsets.Succeeded)
            {
                foreach (var rec in offsets.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("{0} нед., {1} {2} пара", rec.Week + 1, days[rec.Day], rec.Lesson + 1),
                        LessonType = "зачет",
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var examinations = _serviceER.GetExaminationSchedule(modelGet);
            if (examinations.Succeeded)
            {
                foreach (var rec in examinations.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("Конс:{0}, Экз:{1}", rec.DateConsultation.ToShortDateString(), rec.DateExamination.ToShortDateString()),
                        LessonType = "экзамен",
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var consultations = _serviceCR.GetConsultationSchedule(modelGet);
            if (consultations.Succeeded)
            {
                foreach (var rec in consultations.Result)
                {
                    list.Add(new ScheduleRecordsForDisciplineViewModel
                    {
                        Id = rec.Id,
                        Type = ScheduleRecordTypeForDiscipline.Semester,
                        Date = string.Format("Дата:{0}, Время:{1}", rec.DateConsultation.ToShortDateString(), rec.DateConsultation.ToShortTimeString()),
                        LessonType = "консультация",
                        LessonClassroom = rec.LessonClassroom,
                        LessonDiscipline = rec.LessonDiscipline,
                        LessonLecturer = rec.LessonLecturer,
                        LessonGroup = rec.LessonGroup,
                        NotParseRecord = rec.NotParseRecord
                    });
                }
            }

            var result = new ScheduleRecordsForDisciplinePageViewModel
            {
                MaxCount = 0,
                List = list
            };

            return ResultService<ScheduleRecordsForDisciplinePageViewModel>.Success(result);
        }

        #region ClearRecords
        public ResultService ClearSemesterRecords(ScheduleGetBindingModel model)
        {
            return _serviceSR.ClearSemesterRecords(model);
        }

        public ResultService ClearOffsetRecords(ScheduleGetBindingModel model)
        {
            return _serviceOR.ClearOffsetRecords(model);
        }

        public ResultService ClearExaminationRecords(ScheduleGetBindingModel model)
        {
            return _serviceER.ClearExaminationRecords(model);
        }

        public ResultService ClearConsultationRecords(ScheduleGetBindingModel model)
        {
            return _serviceCR.ClearConsultationRecords(model);
        }
        #endregion

        public ResultService CheckSemesterRecordsIfNotComplite()
        {
            var records = _context.SemesterRecords.Include(sr => sr.Classroom).Where(sr =>
                                    (string.IsNullOrEmpty(sr.LessonDiscipline)) ||
                                    (sr.LessonType == LessonTypes.нд)).ToList();
            bool flag;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var record in records)
                    {
                        flag = false;
                        if (string.IsNullOrEmpty(record.LessonDiscipline))
                        {//если нет названия дисциплины
                            var searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
        (sr.LessonGroup == record.LessonGroup || (sr.StudentGroupId == record.StudentGroupId && sr.StudentGroupId != null)) &&
        (sr.LessonLecturer == record.LessonLecturer || (sr.LecturerId == record.LecturerId && sr.LecturerId != null)) &&
        (sr.LessonClassroom == record.LessonClassroom || (sr.ClassroomId == record.ClassroomId && sr.ClassroomId.HasValue)) &&
        !sr.IsStreaming && sr.Id != record.Id);
                            if (searchMatches != null)
                            {
                                record.LessonDiscipline = searchMatches.LessonDiscipline;
                                if (record.LessonType == LessonTypes.нд)
                                {//если по мимо названия дисциплины нет и типа занятия
                                    record.LessonType = searchMatches.LessonType;
                                }
                                flag = true;
                            }
                            else
                            {//если в этой аудитории нет такой пары, то ищем по другим аудиториям
                                searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
        (sr.LessonGroup == record.LessonGroup || (sr.StudentGroupId == record.StudentGroupId && sr.StudentGroupId != null)) &&
        (sr.LessonLecturer == record.LessonLecturer || (sr.LecturerId == record.LecturerId && sr.LecturerId != null)) &&
        !sr.IsStreaming && sr.Id != record.Id);
                                if (searchMatches != null)
                                {
                                    record.LessonDiscipline = searchMatches.LessonDiscipline;
                                    if (record.LessonType == LessonTypes.нд)
                                    {//если по мимо названия дисциплины нет и типа занятия
                                        record.LessonType = searchMatches.LessonType;
                                    }
                                    flag = true;
                                }
                            }
                        }
                        if (record.LessonType == LessonTypes.нд)
                        {//если нет типа занятия
                            var searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
                                                    (sr.LessonGroup == record.LessonGroup || sr.StudentGroupId == record.StudentGroupId) &&
                                                    (sr.LessonLecturer == record.LessonLecturer || sr.LecturerId == record.LecturerId) &&
                                                    (sr.LessonClassroom == record.LessonClassroom || sr.ClassroomId == record.ClassroomId) &&
                                                    (sr.LessonDiscipline == record.LessonDiscipline) &&
                                                    (sr.LessonType != record.LessonType) &&
                                                    !sr.IsStreaming && sr.Id != record.Id);
                            if (searchMatches != null)
                            {
                                record.LessonType = searchMatches.LessonType;
                                flag = true;
                            }
                            else
                            {
                                if (record.Classroom != null)
                                {
                                    searchMatches = _context.SemesterRecords.FirstOrDefault(sr =>
                                                    (sr.LessonGroup == record.LessonGroup || sr.StudentGroupId == record.StudentGroupId) &&
                                                    (sr.LessonLecturer == record.LessonLecturer || sr.LecturerId == record.LecturerId) &&
                                                    (sr.ClassroomId == record.ClassroomId && sr.Classroom.ClassroomType == record.Classroom.ClassroomType) &&
                                                    (sr.LessonDiscipline == record.LessonDiscipline) &&
                                                    (sr.LessonType != record.LessonType) &&
                                                    !sr.IsStreaming && sr.Id != record.Id);
                                    if (searchMatches != null)
                                    {
                                        record.LessonType = searchMatches.LessonType;
                                    }
                                    else
                                    {
                                        switch (record.Classroom.ClassroomType)
                                        {
                                            case ClassroomTypes.Дисплейный:
                                                record.LessonType = LessonTypes.лаб;
                                                break;
                                            case ClassroomTypes.Проекторный:
                                                record.LessonType = LessonTypes.лек;
                                                break;
                                            case ClassroomTypes.Обычный:
                                                record.LessonType = LessonTypes.пр;
                                                break;
                                        }
                                    }
                                    flag = true;
                                }
                            }
                        }
                        if (flag)
                        {
                            _context.Entry(record).State = EntityState.Modified;
                            _context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                    return ResultService.Success();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ResultService.Error(ex, ResultServiceStatusCode.Error);
                }
            }
        }

        #region Export
        public ResultService ExportSemesterRecordExcel(ExportToExcelClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            var times = result.Result.List;

            var resultSemester = _serviceSR.GetSemesterSchedule(new ScheduleGetBindingModel());
            if (!resultSemester.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleSemester not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultSemester.Result;

            return ExportScheduleToExcel.ExportSemesterRecordExcel(times, list, model);
        }

        public ResultService ExportOffsetRecordExcel(ExportToExcelClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            var times = result.Result.List;

            var resultOffset = _serviceOR.GetOffsetSchedule(new ScheduleGetBindingModel());
            if (!resultOffset.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleOffset not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultOffset.Result;

            return ExportScheduleToExcel.ExportOffsetRecordExcel(times, list, model);
        }

        public ResultService ExportExaminationRecordExcel(ExportToExcelClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            var times = result.Result.List;
            result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            times.AddRange(result.Result.List);

            var resultExamination = _serviceER.GetExaminationSchedule(new ScheduleGetBindingModel());
            if (!resultExamination.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleExamination not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultExamination.Result;

            return ExportScheduleToExcel.ExportExaminationRecordExcel(times, list, model);
        }

        public ResultService ExportSemesterRecordHTML(ExportToHTMLClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            var times = result.Result.List;
            // TODO определить период
            var resultSemester = _serviceSR.GetSemesterSchedule(new ScheduleGetBindingModel { IsFirstHalfSemester = false });
            if (!resultSemester.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleSemester not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultSemester.Result;

            return ExportScheduleToHTML.ExportSemesterRecordHTML(times, list, model);
        }

        public ResultService ExportOffsetRecordHTML(ExportToHTMLClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            var times = result.Result.List;

            var resultOffset = _serviceOR.GetOffsetSchedule(new ScheduleGetBindingModel());
            if (!resultOffset.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleOffset not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultOffset.Result;

            return ExportScheduleToHTML.ExportOffsetRecordHTML(times, list, model);
        }

        public ResultService ExportExaminationRecordHTML(ExportToHTMLClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "экзамен" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            var times = result.Result.List;
            result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "консультация" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            times.AddRange(result.Result.List);
            
            var resultExamination = _serviceER.GetExaminationSchedule(new ScheduleGetBindingModel());
            if (!resultExamination.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleExamination not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultExamination.Result;

            return ExportScheduleToHTML.ExportExaminationRecordHTML(times, list, model);
        }
        #endregion

        #region Import
        public ResultService ImportHtml(ImportToSemesterFromHTMLBindingModel model)
        {
            return ParsingSite4SemesterSchedule.ImportHtml(_context, model);
        }

        public ResultService ImportExcel(ImportToOffsetFromExcel model)
        {
            return ImportScheduleFromExcel.ImportOffsets(_context, model);
        }

        public ResultService ImportExcel(ImportToExaminationFromExcel model)
        {
            return ImportScheduleFromExcel.ImportExaminations(_context, model);
        }
        #endregion
    }
}