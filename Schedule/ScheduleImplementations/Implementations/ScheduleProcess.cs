using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Enums;
using Microsoft.EntityFrameworkCore;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using ScheduleServiceImplementations.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace ScheduleImplementations.Services
{
    public class ScheduleProcess : IScheduleProcess
    {
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

        public ScheduleProcess(IClassroomService serviceC, IDisciplineService serviceD, ILecturerService serviceL, IStudentGroupService serviceG,
            ISeasonDatesService serviceSD, IScheduleLessonTimeService serviceSLT, IStreamingLessonService serviceSL,
            ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER,
            IConsultationRecordService serviceCR)
        {
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
                using (var context = DepartmentUserManager.GetContext)
                {
                    var currentSetting = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
                    if (currentSetting == null)
                    {
                        var seasonDates = _serviceSD.GetSeasonDaties(new SeasonDatesGetBindingModel());
                        if (seasonDates.Succeeded)
                        {
                            currentSetting = new Models.CurrentSettings { Key = "Даты семестра", Value = seasonDates.Result.List[0].Title };
                            context.CurrentSettings.Add(currentSetting);
                            context.SaveChanges();
                        }
                        else
                        {
                            return ResultService<SeasonDatesViewModel>.Error("Error:", "CurrentSetting not found", ResultServiceStatusCode.NotFound);
                        }
                    }
                    return _serviceSD.GetSeasonDates(new SeasonDatesGetBindingModel { Title = currentSetting.Value });
                }
            }
            catch (Exception ex)
            {
                return ResultService<SeasonDatesViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService UpdateCurrentDates(SeasonDatesGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var currentSetting = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
                    if (currentSetting == null)
                    {
                        return null;
                    }

                    currentSetting.Value = model.Title;
                    context.SaveChanges();

                    return ResultService.Success();
                }
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
            using (var context = DepartmentUserManager.GetContext)
            {
                var records = context.SemesterRecords.Include(sr => sr.Classroom).Where(sr =>
                                    (string.IsNullOrEmpty(sr.LessonDiscipline)) ||
                                    (sr.LessonType == LessonTypes.нд)).ToList();
                bool flag;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            flag = false;
                            if (string.IsNullOrEmpty(record.LessonDiscipline))
                            {//если нет названия дисциплины
                                var searchMatches = context.SemesterRecords.FirstOrDefault(sr =>
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
                                    searchMatches = context.SemesterRecords.FirstOrDefault(sr =>
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
                                var searchMatches = context.SemesterRecords.FirstOrDefault(sr =>
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
                                        searchMatches = context.SemesterRecords.FirstOrDefault(sr =>
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
                                context.SaveChanges();
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
        }

        #region Export
        public ResultService ExportSemesterRecordExcel(ExportToExcelClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            model.Times = result.Result.List;

            var resultSemester = _serviceSR.GetSemesterSchedule(new ScheduleGetBindingModel());
            if (!resultSemester.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleSemester not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultSemester.Result;

            List<SemesterRecordShortViewModel> records = new List<SemesterRecordShortViewModel>();
            for (int classroom = 0; classroom < model.Classrooms.Count; ++classroom)
            {
                for (int week = 0; week < 2; week++)
                {
                    for (int day = 0; day < 6; day++)
                    {
                        for (int lesson = 0; lesson < 8; lesson++)
                        {
                            var elems = list.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonType != LessonTypes.удл.ToString() &&
                                                        x.LessonClassroom == model.Classrooms[classroom]).OrderBy(x => x.LessonGroup);
                            if (elems != null && elems.Count() > 0)
                            {
                                if (elems.Count() == 1)
                                {
                                    var exs = records.FirstOrDefault(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonClassroom == elems.First().LessonClassroom);
                                    if (exs == null)
                                    {
                                        records.Add(new SemesterRecordShortViewModel
                                        {
                                            Week = week,
                                            Day = day,
                                            Lesson = lesson,
                                            IsStreaming = false,
                                            IsSubgroup = false,
                                            LessonType = elems.First().LessonType,
                                            LessonClassroom = elems.First().LessonClassroom,
                                            LessonDiscipline = elems.First().LessonDiscipline,
                                            LessonGroup = elems.First().LessonGroup,
                                            LessonLecturer = elems.First().LessonLecturer
                                        });
                                    }
                                    else
                                    {
                                        throw new Exception("Накладка");
                                    }
                                }
                                else
                                {
                                    // подгруппы
                                    if (elems.Select(x => x.LessonGroup).Distinct().Count() == 1)
                                    {
                                        var exs = records.FirstOrDefault(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonClassroom == elems.First().LessonClassroom);
                                        if (exs == null)
                                        {
                                            foreach (var elem in elems)
                                            {
                                                records.Add(new SemesterRecordShortViewModel
                                                {
                                                    Week = week,
                                                    Day = day,
                                                    Lesson = lesson,
                                                    IsStreaming = false,
                                                    IsSubgroup = true,
                                                    LessonType = elem.LessonType,
                                                    LessonClassroom = elem.LessonClassroom,
                                                    LessonDiscipline = elem.LessonDiscipline,
                                                    LessonGroup = elem.LessonGroup,
                                                    LessonLecturer = elem.LessonLecturer
                                                });
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Накладка");
                                        }
                                    }
                                    // поток
                                    else
                                    {
                                        string groups = string.Join(",", elems.Select(x => x.LessonGroup));
                                        var stream = _serviceSL.GetStreamingLesson(new StreamingLessonGetBindingModel
                                        {
                                            IncomingGroups = groups
                                        });
                                        if (stream.Succeeded)
                                        {
                                            groups = stream.Result.StreamName;
                                        }
                                        var exs = records.FirstOrDefault(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonClassroom == elems.First().LessonClassroom);
                                        if (exs == null)
                                        {
                                            records.Add(new SemesterRecordShortViewModel
                                            {
                                                Week = week,
                                                Day = day,
                                                Lesson = lesson,
                                                IsStreaming = true,
                                                IsSubgroup = false,
                                                LessonType = elems.First().LessonType,
                                                LessonClassroom = elems.First().LessonClassroom,
                                                LessonDiscipline = elems.First().LessonDiscipline,
                                                LessonGroup = groups,
                                                LessonLecturer = elems.First().LessonLecturer
                                            });
                                        }
                                        else
                                        {
                                            throw new Exception("Накладка");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ExportScheduleToExcel.ExportSemesterRecordExcel(records, model);
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

            var resultDates = GetCurrentDates();
            if (!resultDates.Succeeded)
            {
                return ResultService.Error("Error:", "CurrentDates not found", ResultServiceStatusCode.NotFound);
            }
            model.Times = times;
            model.Dates = resultDates.Result;

            return ExportScheduleToExcel.ExportOffsetRecordExcel(list, model);
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

            var resultDates = GetCurrentDates();
            if (!resultDates.Succeeded)
            {
                return ResultService.Error("Error:", "CurrentDates not found", ResultServiceStatusCode.NotFound);
            }
            model.Times = times;
            model.Dates = resultDates.Result;

            return ExportScheduleToExcel.ExportExaminationRecordExcel(list, model);
        }

        public ResultService ExportSemesterRecordHTML(ExportToHTMLClassroomsBindingModel model)
        {
            var result = _serviceSLT.GetScheduleLessonTimes(new ScheduleLessonTimeGetBindingModel { Title = "пара" });
            if (!result.Succeeded)
            {
                return ResultService.Error("Error:", "LessonTime not found", ResultServiceStatusCode.NotFound);
            }
            model.Times = result.Result.List;
            // TODO определить период
            var resultSemester = _serviceSR.GetSemesterSchedule(new ScheduleGetBindingModel { IsFirstHalfSemester = false });
            if (!resultSemester.Succeeded)
            {
                return ResultService.Error("Error:", "ScheduleSemester not found", ResultServiceStatusCode.NotFound);
            }
            var list = resultSemester.Result;
            List<SemesterRecordShortViewModel> records = new List<SemesterRecordShortViewModel>();
            for (int classroom = 0; classroom < model.Classrooms.Count; ++classroom)
            {
                for (int week = 0; week < 2; week++)
                {
                    for (int day = 0; day < 6; day++)
                    {
                        for (int lesson = 0; lesson < 8; lesson++)
                        {
                            var elems = list.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonType != LessonTypes.удл.ToString() &&
                                                        x.LessonClassroom == model.Classrooms[classroom]).OrderBy(x => x.LessonGroup);
                            if (elems != null && elems.Count() > 0)
                            {
                                if (elems.Count() == 1)
                                {
                                    var exs = records.FirstOrDefault(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonClassroom == elems.First().LessonClassroom);
                                    if (exs == null)
                                    {
                                        records.Add(new SemesterRecordShortViewModel
                                        {
                                            Week = week,
                                            Day = day,
                                            Lesson = lesson,
                                            IsStreaming = false,
                                            IsSubgroup = false,
                                            LessonType = elems.First().LessonType,
                                            LessonClassroom = elems.First().LessonClassroom,
                                            LessonDiscipline = elems.First().LessonDiscipline,
                                            LessonGroup = elems.First().LessonGroup,
                                            LessonLecturer = elems.First().LessonLecturer
                                        });
                                    }
                                    else
                                    {
                                        throw new Exception("Накладка");
                                    }
                                }
                                else
                                {
                                    // подгруппы
                                    if (elems.Select(x => x.LessonGroup).Distinct().Count() == 1)
                                    {
                                        var exs = records.FirstOrDefault(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonClassroom == elems.First().LessonClassroom);
                                        if (exs == null)
                                        {
                                            foreach (var elem in elems)
                                            {
                                                records.Add(new SemesterRecordShortViewModel
                                                {
                                                    Week = week,
                                                    Day = day,
                                                    Lesson = lesson,
                                                    IsStreaming = false,
                                                    IsSubgroup = true,
                                                    LessonType = elem.LessonType,
                                                    LessonClassroom = elem.LessonClassroom,
                                                    LessonDiscipline = elem.LessonDiscipline,
                                                    LessonGroup = elem.LessonGroup,
                                                    LessonLecturer = elem.LessonLecturer
                                                });
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Накладка");
                                        }
                                    }
                                    // поток
                                    else
                                    {
                                        string groups = string.Join(",", elems.Select(x => x.LessonGroup));
                                        var stream = _serviceSL.GetStreamingLesson(new StreamingLessonGetBindingModel
                                        {
                                            IncomingGroups = groups
                                        });
                                        if (stream.Succeeded)
                                        {
                                            groups = stream.Result.StreamName;
                                        }
                                        var exs = records.FirstOrDefault(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonClassroom == elems.First().LessonClassroom);
                                        if (exs == null)
                                        {
                                            records.Add(new SemesterRecordShortViewModel
                                            {
                                                Week = week,
                                                Day = day,
                                                Lesson = lesson,
                                                IsStreaming = true,
                                                IsSubgroup = false,
                                                LessonType = elems.First().LessonType,
                                                LessonClassroom = elems.First().LessonClassroom,
                                                LessonDiscipline = elems.First().LessonDiscipline,
                                                LessonGroup = groups,
                                                LessonLecturer = elems.First().LessonLecturer
                                            });
                                        }
                                        else
                                        {
                                            throw new Exception("Накладка");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ExportScheduleToHTML.ExportSemesterRecordHTML(records, model);
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
            return ParsingSite4SemesterSchedule.ImportHtml(model);
        }

        public ResultService ImportExcel(ImportToOffsetFromExcel model)
        {
            return ImportScheduleFromExcel.ImportOffsets(model);
        }

        public ResultService ImportExcel(ImportToExaminationFromExcel model)
        {
            return ImportScheduleFromExcel.ImportExaminations(model);
        }
        #endregion
    }
}