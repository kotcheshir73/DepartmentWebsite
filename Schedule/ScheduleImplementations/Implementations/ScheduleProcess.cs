using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DatabaseContext;
using Enums;
using Microsoft.EntityFrameworkCore;
using Models.Schedule;
using ScheduleImplementations.Helpers;
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

        private readonly ISemesterRecordService _serviceSR;

        private readonly IOffsetRecordService _serviceOR;

        private readonly IExaminationRecordService _serviceER;

        private readonly IConsultationRecordService _serviceCR;

        public ScheduleProcess(IClassroomService serviceC, IDisciplineService serviceD, ILecturerService serviceL, IStudentGroupService serviceG,
            ISemesterRecordService serviceSR, IOffsetRecordService serviceOR, IExaminationRecordService serviceER, IConsultationRecordService serviceCR)
        {
            _serviceC = serviceC;
            _serviceD = serviceD;
            _serviceL = serviceL;
            _serviceG = serviceG;
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

        public ResultService<List<DateTime>> GetScheduleLessonTimes()
        {
            return ResultService<List<DateTime>>.Success(ScheduleHelper.ScheduleLessonTimes());
        }

        public ISemesterRecordService GetSemesterRecordService()
        {
            return _serviceSR;
        }

        public IExaminationRecordService GetExaminationRecordService()
        {
            return _serviceER;
        }

        public IOffsetRecordService GetOffsetRecordService()
        {
            return _serviceOR;
        }

        public IConsultationRecordService GetConsultationRecordService()
        {
            return _serviceCR;
        }

        public ResultService<List<ScheduleRecordViewModel>> LoadSchedule(LoadScheduleBindingModel model)
        {
            // здесь будем хранить все найденные занятия
            List<ScheduleRecordViewModel> records = new List<ScheduleRecordViewModel>();

            #region вытаскиваем записи
            using (var context = DepartmentUserManager.GetContext)
            {
                // время пар
                List<DateTime> times = GetScheduleLessonTimes().Result;

                //вытаскиваем учебный год
                var currentSetting = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Учебный год");
                if (currentSetting == null)
                {
                    var ay = context.AcademicYears.Last();
                    if (ay != null)
                    {
                        currentSetting = new Models.CurrentSettings { Key = "Учебный год", Value = ay.Title };
                        context.CurrentSettings.Add(currentSetting);
                        context.SaveChanges();
                    }
                    else
                    {
                        return ResultService<List<ScheduleRecordViewModel>>.Error("Error:", "CurrentSetting not found", ResultServiceStatusCode.NotFound);
                    }
                }

                var academicYear = context.AcademicYears.FirstOrDefault(x => x.Title == currentSetting.Value);
                if (academicYear == null)
                {
                    return ResultService<List<ScheduleRecordViewModel>>.Error("Error:", "CurrentSetting not found", ResultServiceStatusCode.NotFound);
                }

                //вытаскиваем все сезонные даты для выбранного года (для 4 курса есть различия)
                var dates = context.SeasonDates.Where(x => x.AcademicYearId == academicYear.Id && !x.IsDeleted).ToList();

                if (dates == null || dates.Count == 0)
                {
                    return ResultService<List<ScheduleRecordViewModel>>.Error("Error:", "SeasonDates not found", ResultServiceStatusCode.NotFound);
                }

                foreach (var date in dates)
                {
                    // Смотрим первую половину семестра
                    // Если дата начала входит в нее или дата конца входит, то получаем записи
                    // Для семестра дата занятия по первым двум неделям начала периода!!!
                    if ((date.DateBeginFirstHalfSemester <= model.BeginDate && date.DateEndFirstHalfSemester >= model.BeginDate) ||
                        (date.DateBeginFirstHalfSemester <= model.EndDate && date.DateEndFirstHalfSemester >= model.EndDate))
                    {
                        var semRecords = GetSemesterRecords(new ScheduleGetBindingModel
                        {
                            ClassroomId = model.ClassroomId,
                            ClassroomNumber = model.ClassroomNumber,
                            DisciplineId = model.DisciplineId,
                            DisciplineName = model.DisciplineName,
                            LecturerId = model.LecturerId,
                            LecturerName = model.LecturerName,
                            StudentGroupId = model.StudentGroupId,
                            StudentGroupName = model.StudentGroupName,
                            DateBegin = date.DateBeginFirstHalfSemester.Date,
                            DateEnd = date.DateBeginFirstHalfSemester.Date.AddDays(13)
                        });

                        // week == 0 - первая неделя
                        var week = date.DateBeginFirstHalfSemester < model.BeginDate ? (model.BeginDate - date.DateBeginFirstHalfSemester).TotalDays / 7 % 2 : 0;

                        var startDay = date.DateBeginFirstHalfSemester < model.BeginDate ? (model.BeginDate - date.DateBeginFirstHalfSemester).TotalDays % 7 : 0;

                        var startDate = date.DateBeginFirstHalfSemester < model.BeginDate ? model.BeginDate : date.DateBeginFirstHalfSemester;

                        while (startDate.Date <= model.EndDate.Date && startDate.Date <= date.DateEndFirstHalfSemester.Date)
                        {
                            for (int day = (int)startDay; day < 7 && startDate.Date <= model.EndDate.Date && startDate.Date <= date.DateEndFirstHalfSemester.Date; day++)
                            {
                                for (int lesson = 0; lesson < 8; lesson++)
                                {
                                    var search = semRecords.Where(x => x.ScheduleDate == ScheduleHelper.GetDateWithTime(date.DateBeginFirstHalfSemester, (int)week, day, lesson));
                                    foreach (var find in search)
                                    {
                                        records.Add(new ScheduleRecordViewModel
                                        {
                                            Id = find.Id,
                                            ClassroomId = find.ClassroomId,
                                            Classroom = find.Classroom?.ToString(),
                                            DisciplineId = find.DisciplineId,
                                            Discipline = find.Discipline?.ToString(),
                                            LecturerId = find.LecturerId,
                                            Lecturer = find.Lecturer?.ToString(),
                                            StudentGroupId = find.StudentGroupId,
                                            StudentGroup = find.StudentGroup?.ToString(),
                                            LessonClassroom = find.LessonClassroom,
                                            LessonDiscipline = find.LessonDiscipline,
                                            LessonLecturer = find.LessonLecturer,
                                            LessonStudentGroup = find.LessonStudentGroup,
                                            LessonType = find.LessonType,
                                            ScheduleRecordType = ScheduleRecordType.Semester,
                                            ScheduleDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, find.ScheduleDate.Hour, find.ScheduleDate.Minute, 0),
                                            TimeSpanMinutes = 90
                                        });
                                    }
                                }

                                startDate = startDate.AddDays(1);
                            }

                            week = ++week % 2;
                        }
                    }

                    // Смотрим вторую половину семестра
                    // Если дата начала входит в нее или дата конца входит, то получаем записи
                    // Для семестра дата занятия по первым двум неделям начала периода!!!
                    if ((date.DateBeginSecondHalfSemester <= model.BeginDate && date.DateEndSecondHalfSemester >= model.BeginDate) ||
                        (date.DateBeginSecondHalfSemester <= model.EndDate && date.DateEndSecondHalfSemester >= model.EndDate))
                    {
                        var semRecords = GetSemesterRecords(new ScheduleGetBindingModel
                        {
                            ClassroomId = model.ClassroomId,
                            ClassroomNumber = model.ClassroomNumber,
                            DisciplineId = model.DisciplineId,
                            DisciplineName = model.DisciplineName,
                            LecturerId = model.LecturerId,
                            LecturerName = model.LecturerName,
                            StudentGroupId = model.StudentGroupId,
                            StudentGroupName = model.StudentGroupName,
                            DateBegin = date.DateBeginSecondHalfSemester.Date,
                            DateEnd = date.DateBeginSecondHalfSemester.Date.AddDays(13)
                        });

                        // week == 0 - первая неделя
                        var week = date.DateBeginSecondHalfSemester < model.BeginDate ? (model.BeginDate - date.DateBeginSecondHalfSemester).TotalDays / 7 % 2 : 0;

                        var startDay = date.DateBeginFirstHalfSemester < model.BeginDate ? (model.BeginDate - date.DateBeginFirstHalfSemester).TotalDays % 7 : 0;

                        var startDate = date.DateBeginSecondHalfSemester < model.BeginDate ? model.BeginDate : date.DateBeginSecondHalfSemester;

                        while (startDate.Date <= model.EndDate.Date && startDate.Date <= date.DateEndSecondHalfSemester.Date)
                        {
                            for (int day = (int)startDay; day < 7 && startDate.Date <= model.EndDate.Date && startDate.Date <= date.DateEndSecondHalfSemester.Date; day++)
                            {
                                for (int lesson = 0; lesson < 8; lesson++)
                                {
                                    var search = semRecords.Where(x => x.ScheduleDate == ScheduleHelper.GetDateWithTime(date.DateBeginSecondHalfSemester, (int)week, day, lesson));
                                    foreach (var find in search)
                                    {
                                        records.Add(new ScheduleRecordViewModel
                                        {
                                            Id = find.Id,
                                            ClassroomId = find.ClassroomId,
                                            Classroom = find.Classroom?.ToString(),
                                            DisciplineId = find.DisciplineId,
                                            Discipline = find.Discipline?.ToString(),
                                            LecturerId = find.LecturerId,
                                            Lecturer = find.Lecturer?.ToString(),
                                            StudentGroupId = find.StudentGroupId,
                                            StudentGroup = find.StudentGroup?.ToString(),
                                            LessonClassroom = find.LessonClassroom,
                                            LessonDiscipline = find.LessonDiscipline,
                                            LessonLecturer = find.LessonLecturer,
                                            LessonStudentGroup = find.LessonStudentGroup,
                                            LessonType = find.LessonType,
                                            ScheduleRecordType = ScheduleRecordType.Semester,
                                            ScheduleDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, find.ScheduleDate.Hour, find.ScheduleDate.Minute, 0),
                                            TimeSpanMinutes = 90
                                        });
                                    }
                                }

                                startDate = startDate.AddDays(1);
                            }

                            week = ++week % 2;
                        }
                    }
                }

                // Смотрим зачеты
                var offRecords = GetOffsetRecords(new ScheduleGetBindingModel
                {
                    ClassroomId = model.ClassroomId,
                    ClassroomNumber = model.ClassroomNumber,
                    DisciplineId = model.DisciplineId,
                    DisciplineName = model.DisciplineName,
                    LecturerId = model.LecturerId,
                    LecturerName = model.LecturerName,
                    StudentGroupId = model.StudentGroupId,
                    StudentGroupName = model.StudentGroupName,
                    DateBegin = model.BeginDate.Date,
                    DateEnd = model.EndDate.Date.AddDays(1)
                });
                foreach (var find in offRecords)
                {
                    records.Add(new ScheduleRecordViewModel
                    {
                        Id = find.Id,
                        ClassroomId = find.ClassroomId,
                        Classroom = find.Classroom?.ToString(),
                        DisciplineId = find.DisciplineId,
                        Discipline = find.Discipline?.ToString(),
                        LecturerId = find.LecturerId,
                        Lecturer = find.Lecturer?.ToString(),
                        StudentGroupId = find.StudentGroupId,
                        StudentGroup = find.StudentGroup?.ToString(),
                        LessonClassroom = find.LessonClassroom,
                        LessonDiscipline = find.LessonDiscipline,
                        LessonLecturer = find.LessonLecturer,
                        LessonStudentGroup = find.LessonStudentGroup,
                        LessonType = LessonTypes.зачет,
                        ScheduleRecordType = ScheduleRecordType.Offset,
                        ScheduleDate = find.ScheduleDate,
                        TimeSpanMinutes = 90
                    });
                }

                // Смотрим экзамены
                var examRecords = GetExaminationRecords(new ScheduleGetBindingModel
                {
                    ClassroomId = model.ClassroomId,
                    ClassroomNumber = model.ClassroomNumber,
                    DisciplineId = model.DisciplineId,
                    DisciplineName = model.DisciplineName,
                    LecturerId = model.LecturerId,
                    LecturerName = model.LecturerName,
                    StudentGroupId = model.StudentGroupId,
                    StudentGroupName = model.StudentGroupName,
                    DateBegin = model.BeginDate,
                    DateEnd = model.EndDate
                });
                foreach (var find in examRecords)
                {
                    if (find.DateConsultation.Date >= model.BeginDate && find.DateConsultation <= model.EndDate)
                    {
                        records.Add(new ScheduleRecordViewModel
                        {
                            Id = find.Id,
                            ClassroomId = find.ClassroomId,
                            Classroom = find.Classroom?.ToString(),
                            DisciplineId = find.DisciplineId,
                            Discipline = find.Discipline?.ToString(),
                            LecturerId = find.LecturerId,
                            Lecturer = find.Lecturer?.ToString(),
                            StudentGroupId = find.StudentGroupId,
                            StudentGroup = find.StudentGroup?.ToString(),
                            LessonClassroom = find.LessonClassroom,
                            LessonDiscipline = find.LessonDiscipline,
                            LessonLecturer = find.LessonLecturer,
                            LessonStudentGroup = find.LessonStudentGroup,
                            LessonType = LessonTypes.экзконс,
                            ScheduleRecordType = ScheduleRecordType.Examination,
                            ScheduleDate = find.DateConsultation,
                            TimeSpanMinutes = 90
                        });
                    }
                    if (find.ScheduleDate.Date >= model.BeginDate && find.ScheduleDate <= model.EndDate)
                    {
                        records.Add(new ScheduleRecordViewModel
                        {
                            Id = find.Id,
                            ClassroomId = find.ClassroomId,
                            Classroom = find.Classroom?.ToString(),
                            DisciplineId = find.DisciplineId,
                            Discipline = find.Discipline?.ToString(),
                            LecturerId = find.LecturerId,
                            Lecturer = find.Lecturer?.ToString(),
                            StudentGroupId = find.StudentGroupId,
                            StudentGroup = find.StudentGroup?.ToString(),
                            LessonClassroom = find.LessonClassroom,
                            LessonDiscipline = find.LessonDiscipline,
                            LessonLecturer = find.LessonLecturer,
                            LessonStudentGroup = find.LessonStudentGroup,
                            LessonType = LessonTypes.экзамен,
                            ScheduleRecordType = ScheduleRecordType.Examination,
                            ScheduleDate = find.ScheduleDate,
                            TimeSpanMinutes = 180
                        });
                    }
                }

                // Смотрим консультации
                var consRecords = GetConsultationRecords(new ScheduleGetBindingModel
                {
                    ClassroomId = model.ClassroomId,
                    ClassroomNumber = model.ClassroomNumber,
                    DisciplineId = model.DisciplineId,
                    DisciplineName = model.DisciplineName,
                    LecturerId = model.LecturerId,
                    LecturerName = model.LecturerName,
                    StudentGroupId = model.StudentGroupId,
                    StudentGroupName = model.StudentGroupName,
                    DateBegin = model.BeginDate,
                    DateEnd = model.EndDate
                });
                foreach (var find in consRecords)
                {
                    records.Add(new ScheduleRecordViewModel
                    {
                        Id = find.Id,
                        ClassroomId = find.ClassroomId,
                        Classroom = find.Classroom?.ToString(),
                        DisciplineId = find.DisciplineId,
                        Discipline = find.Discipline?.ToString(),
                        LecturerId = find.LecturerId,
                        Lecturer = find.Lecturer?.ToString(),
                        StudentGroupId = find.StudentGroupId,
                        StudentGroup = find.StudentGroup?.ToString(),
                        LessonClassroom = find.LessonClassroom,
                        LessonDiscipline = find.LessonDiscipline,
                        LessonLecturer = find.LessonLecturer,
                        LessonStudentGroup = find.LessonStudentGroup,
                        LessonType = LessonTypes.конс,
                        ScheduleRecordType = ScheduleRecordType.Consultation,
                        ScheduleDate = find.ScheduleDate,
                        TimeSpanMinutes = find.ConsultationTime
                    });
                }
            }
            #endregion

            // сортируем все найденные записи
            records = records.OrderBy(x => x.ScheduleDate).ToList();

            return ResultService<List<ScheduleRecordViewModel>>.Success(records);
        }

        private List<SemesterRecord> GetSemesterRecords(ScheduleGetBindingModel model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var selectedRecords = context.SemesterRecords.Where(x => x.ScheduleDate >= model.DateBegin.Value && x.ScheduleDate <= model.DateEnd.Value);

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
                    selectedRecords = selectedRecords.Where(x => x.LessonStudentGroup == model.StudentGroupName);
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

                selectedRecords = selectedRecords.OrderBy(s => s.ScheduleDate);

                return selectedRecords.ToList();
            }
        }

        private List<OffsetRecord> GetOffsetRecords(ScheduleGetBindingModel model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var selectedRecords = context.OffsetRecords.Where(x => x.ScheduleDate >= model.DateBegin.Value &&
                                                                    x.ScheduleDate <= model.DateEnd.Value);

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
                    selectedRecords = selectedRecords.Where(x => x.LessonStudentGroup == model.StudentGroupName);
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

                selectedRecords = selectedRecords.OrderBy(s => s.ScheduleDate).ThenBy(s => s.ScheduleDate);

                return selectedRecords.ToList();
            }
        }

        private List<ExaminationRecord> GetExaminationRecords(ScheduleGetBindingModel model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var selectedRecords = context.ExaminationRecords.Where(x =>
                                (x.ScheduleDate >= model.DateBegin.Value && x.ScheduleDate <= model.DateEnd.Value) ||
                                (x.DateConsultation >= model.DateBegin.Value && x.DateConsultation <= model.DateEnd.Value));

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
                    selectedRecords = selectedRecords.Where(x => x.LessonStudentGroup == model.StudentGroupName);
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

                selectedRecords = selectedRecords.OrderBy(s => s.ScheduleDate);

                return selectedRecords.ToList();
            }
        }

        private List<ConsultationRecord> GetConsultationRecords(ScheduleGetBindingModel model)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var selectedRecords = context.ConsultationRecords.Where(x => x.ScheduleDate >= model.DateBegin.Value &&
                                                                    x.ScheduleDate <= model.DateEnd.Value);

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
                    selectedRecords = selectedRecords.Where(x => x.LessonStudentGroup == model.StudentGroupName);
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

                selectedRecords = selectedRecords.OrderBy(s => s.ScheduleDate);

                return selectedRecords.ToList();
            }
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

        #region Import
        public ResultService Import(ImportToSemesterRecordsBindingModel model)
        {
            return ImportScheduleFromSite.ImportHtml(model);
        }

        public ResultService Import(ImportToOffsetFromExcel model)
        {
            return ImportScheduleFromExcel.ImportOffsets(model);
        }

        public ResultService Import(ImportToExaminationFromExcel model)
        {
            return ImportScheduleFromExcel.ImportExaminations(model);
        }
        #endregion
    }
}