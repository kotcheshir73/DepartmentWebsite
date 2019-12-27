using DatabaseContext;
using Models;
using Models.AcademicYearData;
using Models.Schedule;
using ScheduleInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleImplementations.Helpers
{
    // TODO разбить на несколько классов по логике
    public class ScheduleHelper
    {
        public static bool CheckCreateConsultation(ConsultationRecordSetBindingModel model, SeasonDates seasonDate)
        {
            DateTime[] lessons;
            using (var context = DepartmentUserManager.GetContext)
            {
                #region консультация назначается в семестре, определяем неделю, день и пару
                var dateBeginSemester = Convert.ToDateTime(seasonDate.DateBeginFirstHalfSemester);
                var dateEndSemester = Convert.ToDateTime(seasonDate.DateEndSecondHalfSemester);
                if (dateBeginSemester < model.ScheduleDate && dateEndSemester > model.ScheduleDate)
                {
                    // по дате консультации определяем неделю, день и пару
                    int day = ((int)(model.ScheduleDate - dateBeginSemester).TotalDays % 14);
                    int week = day < 7 ? 0 : 1;
                    day = day % 7;
                    int lesson = 7;

                    // получаем время пар
                    //var times = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("пара")).OrderBy(x => x.DateBeginLesson).ToList();
                    //if (times == null || times.Count == 0)
                    //{
                    //    throw new Exception("LessonTime not found");
                    //}

                    ////формиурем даты с временем, исходя из времени начала пар
                    //lessons = new DateTime[times.Count];
                    //for (int i = 0; i < times.Count; ++i)
                    //{
                    //    lessons[i] = new DateTime(model.DateBeginConsultation.Year, model.DateBeginConsultation.Month, model.DateBeginConsultation.Day,
                    //        times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
                    //}

                    //// ищем на какую пару выпадает консультация
                    //for (int i = 0; i < lessons.Length - 1; ++i)
                    //{
                    //    if (lessons[i] >= model.DateBeginConsultation && lessons[i + 1] >= model.DateBeginConsultation)
                    //    {
                    //        lesson = i;
                    //        break;
                    //    }
                    //}

                    // проверяем, что пара свободна
                    //var entries = context.SemesterRecords.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.SeasonDatesId == seasonDate.Id && x.LessonType != LessonTypes.удл);
                    //if(model.ClassroomId.HasValue)
                    //{
                    //    entries = entries.Where(x => x.ClassroomId == model.ClassroomId);
                    //}
                    //if (model.LecturerId.HasValue)
                    //{
                    //    entries = entries.Where(x => x.LecturerId == model.LecturerId);
                    //}
                    //if (model.StudentGroupId.HasValue)
                    //{
                    //    entries = entries.Where(x => x.StudentGroupId == model.StudentGroupId);
                    //}

                    //if (entries != null && entries.Count() > 0)
                    //{
                    //    throw new Exception("Exsist SemesterRecord");
                    //}

                    //model.Week = week;
                    //model.Day = day;
                    //model.Lesson = lesson;
                }
                #endregion

                #region консультация ставится на зачетной неделе
                var dateBeginOffset = Convert.ToDateTime(seasonDate.DateBeginOffset);
                var dateEndOffset = Convert.ToDateTime(seasonDate.DateEndOffset);
                if (dateBeginOffset < model.ScheduleDate && dateEndOffset > model.ScheduleDate)
                {
                    // по дате консультации определяем неделю, день и пару
                    int day = ((int)(model.ScheduleDate - dateBeginOffset).TotalDays % 14);
                    int week = day < 8 ? 0 : 1;
                    day = day % 7;
                    int lesson = 7;

                    // получаем время пар
                    //var times = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("пара")).ToList();
                    //if (times == null || times.Count == 0)
                    //{
                    //    throw new Exception("LessonTime not found");
                    //}

                    ////формиурем даты с временем, исходя из времени начала пар
                    //lessons = new DateTime[times.Count];
                    //for (int i = 0; i < times.Count; ++i)
                    //{
                    //    lessons[i] = new DateTime(model.DateBeginConsultation.Year, model.DateBeginConsultation.Month, model.DateBeginConsultation.Day,
                    //        times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
                    //}

                    // ищем на какую пару выпадает консультация
                    //for (int i = 0; i < lessons.Length - 1; ++i)
                    //{
                    //    if (lessons[i] >= model.DateBeginConsultation && lessons[i + 1] >= model.DateBeginConsultation)
                    //    {
                    //        lesson = i;
                    //        break;
                    //    }
                    //}

                    // проверяем, что пара свободна
                    //var entry = context.OffsetRecords.FirstOrDefault(sr => sr.Week == week && sr.Day == day && sr.Lesson == lesson &&
                    //                                                           sr.ClassroomId == model.ClassroomId);
                    //if (entry != null)
                    //{
                    //    throw new Exception("Exsist OffsetRecord");
                    //}

                    //model.Week = week;
                    //model.Day = day;
                    //model.Lesson = lesson;
                }
                #endregion

                #region консультация назначается в сессию
                var dateBeginExamination = Convert.ToDateTime(seasonDate.DateBeginExamination);
                var dateEndExamination = Convert.ToDateTime(seasonDate.DateEndExamination);
                if (dateBeginExamination < model.ScheduleDate && dateEndExamination > model.ScheduleDate)
                {
                    // по дате консультации определяем день
                    int day = ((int)(model.ScheduleDate - dateBeginExamination).TotalDays);
                    int lesson = 2;
                    // получаем время экзаменов
                    //var times = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("экзамен")).ToList();
                    //if (times == null || times.Count == 0)
                    //{
                    //    throw new Exception("LessonTime not found");
                    //}
                    //// получаем время консультаций
                    //times.AddRange(context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("консультация")).ToList());
                    //if (times == null || times.Count == 0)
                    //{
                    //    throw new Exception("LessonTime not found");
                    //}

                    ////формиурем даты с временем, исходя из времени начала экзаменов и консультаций
                    //lessons = new DateTime[times.Count];
                    //for (int i = 0; i < times.Count; ++i)
                    //{
                    //    lessons[i] = new DateTime(model.DateBeginConsultation.Year, model.DateBeginConsultation.Month, model.DateBeginConsultation.Day,
                    //        times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
                    //}

                    //// ищем на какую пару выпадает консультация
                    //for (int i = 0; i < lessons.Length - 1; ++i)
                    //{
                    //    if (lessons[i] <= model.DateBeginConsultation && lessons[i + 1] > model.DateBeginConsultation)
                    //    {
                    //        lesson = i;
                    //        break;
                    //    }
                    //}

                    // проверяем, что пара свободна
                    //var entry = context.ExaminationRecords.FirstOrDefault(sr =>
                    //                     ((sr.DateExamination.Year == model.DateBeginConsultation.Year && sr.DateExamination.Month == model.DateBeginConsultation.Month &&
                    //                     sr.DateExamination.Day == model.DateBeginConsultation.Day &&
                    //                     (sr.DateExamination.Hour >= model.DateBeginConsultation.Hour && sr.DateExamination.Hour + 3 < model.DateBeginConsultation.Hour))
                    //                     //попадает на момент проведения экзамена (3 часа на экзамен)
                    //                     ||
                    //                     (sr.DateConsultation.Year == model.DateBeginConsultation.Year && sr.DateConsultation.Month == model.DateBeginConsultation.Month &&
                    //                     sr.DateConsultation.Day == model.DateBeginConsultation.Day &&
                    //                     (sr.DateConsultation.Hour >= model.DateBeginConsultation.Hour && sr.DateConsultation.Hour + 1 < model.DateBeginConsultation.Hour)))
                    //                     //попадает на момент проведения консультации (1  на консультацию)
                    //                     && sr.ClassroomId == model.ClassroomId);
                    //if (entry != null)
                    //{
                    //    throw new Exception("Exsist ExaminationRecord");
                    //}

                    //model.Week = 0;
                    //model.Day = day;
                    //model.Lesson = lesson;
                }
                #endregion
                return true;
            }
        }

        public static string GetLessonLecturer(ScheduleRecord entity)
        {
            string str = entity.LecturerId.HasValue ? entity.Lecturer.ToString() : entity.LessonLecturer;
            if (!entity.LecturerId.HasValue)
            {
                if (str == null)
                {
                    str = string.Empty;
                }
                else
                {
                    var strs = str.Split(' ');
                    switch (strs.Length)
                    {
                        case 1:
                            str = string.Format("{0}{1}", strs[0][0], strs[0].Substring(1).ToLower());
                            break;
                        case 2:
                            str = string.Format("{0}{1} {2}", strs[0][0], strs[0].Substring(1).ToLower(), strs[1]);
                            break;
                        case 3:
                            str = string.Format("{0}{1} {2} {3}", strs[0][0], strs[0].Substring(1).ToLower(), strs[1], strs[2]);
                            break;
                    }
                }
            }
            return str;
        }

        public static string GetLessonDiscipline(ScheduleRecord entity)
        {
            string str = entity.DisciplineId.HasValue ? entity.Discipline.DisciplineShortName : CalcShortDisciplineName(entity.LessonDiscipline);

            if (string.IsNullOrEmpty(str) && entity.DisciplineId.HasValue)
            {
                str = CalcShortDisciplineName(entity.LessonDiscipline);
            }

            return str;
        }

        public static string GetLessonGroup(ScheduleRecord entity)
        {
            return entity.StudentGroupId.HasValue ? entity.StudentGroup.GroupName : entity.LessonStudentGroup;
        }

        public static string GetLessonClassroom(ScheduleRecord entity)
        {
            return entity.ClassroomId.HasValue ? entity.Classroom.Number : entity.LessonClassroom;
        }

        public static string GetLessonConsultationClassroom(ExaminationRecord entity)
        {
            return entity.ConsultationClassroomId.HasValue ? entity.ConsultationClassroom.Number : entity.LessonConsultationClassroom;
        }

        public static string CalcShortDisciplineName(string fullDiscipliineName)
        {
            if (string.IsNullOrEmpty(fullDiscipliineName))
            {
                return string.Empty;
            }
            // TODO избавиться от '-ия' и т.п.
            StringBuilder sb = new StringBuilder();
            var glas = new List<char> { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            var predlogs = new List<string> { "в", "без", "до", "и", "из", "к", "на", "по", "о", "от", "перед", "при", "через", "с", "у", "за", "над", "об", "под", "для" };
            var strsSpice = fullDiscipliineName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // если одно слово, и оно не содержит деифсов и точек, то просто берем первые 8 символов
            if (strsSpice.Length == 1 && !strsSpice[0].Contains("-") && !strsSpice[0].Contains("."))
            {
                int length = strsSpice[0].Length > 8 ? 8 : strsSpice[0].Length;
                sb.Append(string.Format("{0}.", strsSpice[0].Substring(0, length)));
            }
            else
            {
                foreach (var strSpice in strsSpice)
                {
                    // если слово содержит дефис и оно одно, то каждое слово сокращаем до 3-4 знаков
                    if (strSpice.Contains("-") && strsSpice.Length == 1)
                    {
                        var substrs = strSpice.Split(new char[] { '-', '.' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var subS in substrs)
                        {
                            for (int t = 0; t < subS.Length; ++t)
                            {
                                if (t < 3)
                                {
                                    sb.Append(subS[t]);
                                }
                                else if (!glas.Contains(subS[t]))
                                {
                                    sb.Append(subS[t]);
                                }
                                else
                                {
                                    sb.Append('.');
                                    break;
                                }
                            }
                            sb.Append('-');
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                    }
                    // иначе
                    else
                    {
                        // разбиваем строку на подстроки по точке и дефису
                        var substrs = strSpice.Split(new char[] { '.', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var subS in substrs)
                        {
                            // если строка - предлог, то берем тока первый симвов а нижнем регистре
                            if (predlogs.Contains(subS.ToLower()))
                            {
                                sb.Append(subS.ToLower()[0]);
                            }
                            // слово может быть целиком уже аббревиатурой
                            else if (subS.ToUpper() == subS)
                            {
                                sb.Append(subS);
                            }
                            // либо берем первую букву в верхнем регистре
                            else
                            {
                                sb.Append(subS[0].ToString().ToUpper());
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }

        public static List<DateTime> ScheduleLessonTimes()
        {
            return new List<DateTime>
            {
                DateTime.Now.Date.AddHours(8).AddMinutes(0),
                DateTime.Now.Date.AddHours(9).AddMinutes(40),
                DateTime.Now.Date.AddHours(11).AddMinutes(30),
                DateTime.Now.Date.AddHours(13).AddMinutes(10),
                DateTime.Now.Date.AddHours(14).AddMinutes(50),
                DateTime.Now.Date.AddHours(16).AddMinutes(30),
                DateTime.Now.Date.AddHours(18).AddMinutes(10),
                DateTime.Now.Date.AddHours(19).AddMinutes(50)
            };
        }

        public static List<DateTime> GetSemesterDates()
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                //вытаскиваем учебный год
                var currentSetting = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Учебный год");
                if (currentSetting == null)
                {
                    var ay = context.AcademicYears.Last();
                    if (ay != null)
                    {
                        currentSetting = new CurrentSettings { Key = "Учебный год", Value = ay.Title };
                        context.CurrentSettings.Add(currentSetting);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("CurrentSetting not found");
                    }
                }

                var academicYear = context.AcademicYears.FirstOrDefault(x => x.Title == currentSetting.Value);
                if (academicYear == null)
                {
                    throw new Exception("AcademicYears not found");
                }

                //вытаскиваем все сезонные даты для выбранного года (для 4 курса есть различия)
                var dates = context.SeasonDates.Where(x => x.AcademicYearId == academicYear.Id && !x.IsDeleted).ToList();
                List<DateTime> dateTimes = new List<DateTime>();
                foreach (var date in dates)
                {
                    dateTimes.Add(date.DateBeginFirstHalfSemester);
                    dateTimes.Add(date.DateBeginSecondHalfSemester);
                }

                return dateTimes.Distinct().OrderBy(x => x.Date).ToList();
            }
        }

        public static DateTime GetDateWithTime(DateTime date, int lesson)
        {
            return date.Date.AddHours(ScheduleLessonTimes()[lesson].Hour).AddMinutes(ScheduleLessonTimes()[lesson].Minute);
        }

        public static DateTime GetDateWithTime(DateTime date, int week, int day, int lesson)
        {
            return date.Date.AddDays(week * 7 + day).AddHours(ScheduleLessonTimes()[lesson].Hour).AddMinutes(ScheduleLessonTimes()[lesson].Minute);
        }

        public static int GetWeek(DateTime date)
        {
            var semesterDates = GetSemesterDates();
            foreach(var semdate in semesterDates)
            {
                var days = (int)(date - semdate).TotalDays;
                if (days < 14)
                {
                    return days / 7;
                }
            }

            return -1;
        }

        public static int GetDay(DateTime date)
        {
            var semesterDates = GetSemesterDates();
            foreach (var semdate in semesterDates)
            {
                var days = (int)(date - semdate).TotalDays;
                if (days < 7)
                {
                    return days;
                }
                else if (days < 14)
                {
                    return days - 7;
                }
            }

            return -1;
        }

        public static int GetLesson(DateTime date)
        {
            for (int i = 0; i < ScheduleLessonTimes().Count; i++)
            {
                if (ScheduleLessonTimes()[i].Hour == date.Hour && ScheduleLessonTimes()[i].Minute == date.Minute)
                {
                    return i;
                }
            }

            return -1;
        }

        public static void GetClassroom(DepartmentDatabaseContext context, ScheduleSetBindingModel record)
        {
            if (!string.IsNullOrEmpty(record.LessonClassroom))
            {
                var classroom = context.Classrooms.FirstOrDefault(c => record.LessonClassroom.Contains(c.Number) && !c.IsDeleted);
                if (classroom != null)
                {
                    record.ClassroomId = classroom.Id;
                }
            }
            else
            {
                record.LessonClassroom = "нет данных";
            }
        }

        public static void GetConsultationClassroom(DepartmentDatabaseContext context, ExaminationRecordSetBindingModel record)
        {
            if (!string.IsNullOrEmpty(record.LessonConsultationClassroom))
            {
                var classroom = context.Classrooms.FirstOrDefault(c => record.LessonConsultationClassroom.Contains(c.Number) && !c.IsDeleted);
                if (classroom != null)
                {
                    record.ConsultationClassroomId = classroom.Id;
                }
            }
            else
            {
                record.LessonConsultationClassroom = "нет данных";
            }
        }

        public static void GetDiscipline(DepartmentDatabaseContext context, ScheduleSetBindingModel record)
        {
            if (!string.IsNullOrEmpty(record.LessonDiscipline))
            {
                var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineName == record.LessonDiscipline);
                if (discipline != null)
                {
                    record.DisciplineId = discipline.Id;
                    if (!string.IsNullOrEmpty(discipline.DisciplineShortName))
                    {
                        record.LessonDiscipline = discipline.DisciplineShortName;
                    }
                }
            }
            else
            {
                record.LessonDiscipline = "нет данных";
            }
        }

        public static void GetLecturer(DepartmentDatabaseContext context, ScheduleSetBindingModel record)
        {
            if (!string.IsNullOrEmpty(record.LessonLecturer))
            {
                var spliters = record.LessonLecturer.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string lastName = spliters[0][0] + spliters[0].Substring(1).ToLower();
                string firstName = spliters.Length > 1 ? spliters[1] : string.Empty;
                string patronumic = spliters.Length > 2 ? spliters[2] : string.Empty;
                var lecturer = context.Lecturers.FirstOrDefault(l => l.LastName == lastName &&
                                        ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                                        ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                if (lecturer != null)
                {
                    record.LecturerId = lecturer.Id;
                }
            }
            else
            {
                record.LessonLecturer = "нет данных";
            }
        }

        public static void GetStudentGroup(DepartmentDatabaseContext context, ScheduleSetBindingModel record)
        {
            if (!string.IsNullOrEmpty(record.LessonStudentGroup))
            {
                var group = context.StudentGroups.FirstOrDefault(x => x.GroupName == record.LessonStudentGroup && !x.IsDeleted);
                if (group != null)
                {
                    record.StudentGroupId = group.Id;
                }
            }
            else
            {
                record.LessonStudentGroup = "нет данных";
            }
        }
    }
}