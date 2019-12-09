using DatabaseContext;
using Enums;
using Models;
using Models.AcademicYearData;
using Models.Schedule;
using ScheduleInterfaces.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

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
                if (dateBeginSemester < model.DateConsultation && dateEndSemester > model.DateConsultation)
                {
                    // по дате консультации определяем неделю, день и пару
                    int day = ((int)(model.DateConsultation - dateBeginSemester).TotalDays % 14);
                    int week = day < 7 ? 0 : 1;
                    day = day % 7;
                    int lesson = 7;

                    // получаем время пар
                    var times = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("пара")).OrderBy(x => x.DateBeginLesson).ToList();
                    if (times == null || times.Count == 0)
                    {
                        throw new Exception("LessonTime not found");
                    }

                    //формиурем даты с временем, исходя из времени начала пар
                    lessons = new DateTime[times.Count];
                    for (int i = 0; i < times.Count; ++i)
                    {
                        lessons[i] = new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day,
                            times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
                    }

                    // ищем на какую пару выпадает консультация
                    for (int i = 0; i < lessons.Length - 1; ++i)
                    {
                        if (lessons[i] >= model.DateConsultation && lessons[i + 1] >= model.DateConsultation)
                        {
                            lesson = i;
                            break;
                        }
                    }

                    // проверяем, что пара свободна
                    var entries = context.SemesterRecords.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.SeasonDatesId == seasonDate.Id && x.LessonType != LessonTypes.удл);
                    if(model.ClassroomId.HasValue)
                    {
                        entries = entries.Where(x => x.ClassroomId == model.ClassroomId);
                    }
                    if (model.LecturerId.HasValue)
                    {
                        entries = entries.Where(x => x.LecturerId == model.LecturerId);
                    }
                    if (model.StudentGroupId.HasValue)
                    {
                        entries = entries.Where(x => x.StudentGroupId == model.StudentGroupId);
                    }

                    if (entries != null && entries.Count() > 0)
                    {
                        throw new Exception("Exsist SemesterRecord");
                    }

                    //model.Week = week;
                    //model.Day = day;
                    //model.Lesson = lesson;
                }
                #endregion

                #region консультация ставится на зачетной неделе
                var dateBeginOffset = Convert.ToDateTime(seasonDate.DateBeginOffset);
                var dateEndOffset = Convert.ToDateTime(seasonDate.DateEndOffset);
                if (dateBeginOffset < model.DateConsultation && dateEndOffset > model.DateConsultation)
                {
                    // по дате консультации определяем неделю, день и пару
                    int day = ((int)(model.DateConsultation - dateBeginOffset).TotalDays % 14);
                    int week = day < 8 ? 0 : 1;
                    day = day % 7;
                    int lesson = 7;

                    // получаем время пар
                    var times = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("пара")).ToList();
                    if (times == null || times.Count == 0)
                    {
                        throw new Exception("LessonTime not found");
                    }

                    //формиурем даты с временем, исходя из времени начала пар
                    lessons = new DateTime[times.Count];
                    for (int i = 0; i < times.Count; ++i)
                    {
                        lessons[i] = new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day,
                            times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
                    }

                    // ищем на какую пару выпадает консультация
                    for (int i = 0; i < lessons.Length - 1; ++i)
                    {
                        if (lessons[i] >= model.DateConsultation && lessons[i + 1] >= model.DateConsultation)
                        {
                            lesson = i;
                            break;
                        }
                    }

                    // проверяем, что пара свободна
                    var entry = context.OffsetRecords.FirstOrDefault(sr => sr.Week == week && sr.Day == day && sr.Lesson == lesson &&
                                                                               sr.ClassroomId == model.ClassroomId);
                    if (entry != null)
                    {
                        throw new Exception("Exsist OffsetRecord");
                    }

                    //model.Week = week;
                    //model.Day = day;
                    //model.Lesson = lesson;
                }
                #endregion

                #region консультация назначается в сессию
                var dateBeginExamination = Convert.ToDateTime(seasonDate.DateBeginExamination);
                var dateEndExamination = Convert.ToDateTime(seasonDate.DateEndExamination);
                if (dateBeginExamination < model.DateConsultation && dateEndExamination > model.DateConsultation)
                {
                    // по дате консультации определяем день
                    int day = ((int)(model.DateConsultation - dateBeginExamination).TotalDays);
                    int lesson = 2;
                    // получаем время экзаменов
                    var times = context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("экзамен")).ToList();
                    if (times == null || times.Count == 0)
                    {
                        throw new Exception("LessonTime not found");
                    }
                    // получаем время консультаций
                    times.AddRange(context.ScheduleLessonTimes.Where(slt => slt.Title.Contains("консультация")).ToList());
                    if (times == null || times.Count == 0)
                    {
                        throw new Exception("LessonTime not found");
                    }

                    //формиурем даты с временем, исходя из времени начала экзаменов и консультаций
                    lessons = new DateTime[times.Count];
                    for (int i = 0; i < times.Count; ++i)
                    {
                        lessons[i] = new DateTime(model.DateConsultation.Year, model.DateConsultation.Month, model.DateConsultation.Day,
                            times[i].DateBeginLesson.Hour, times[i].DateBeginLesson.Minute, 0);
                    }

                    // ищем на какую пару выпадает консультация
                    for (int i = 0; i < lessons.Length - 1; ++i)
                    {
                        if (lessons[i] <= model.DateConsultation && lessons[i + 1] > model.DateConsultation)
                        {
                            lesson = i;
                            break;
                        }
                    }

                    // проверяем, что пара свободна
                    var entry = context.ExaminationRecords.FirstOrDefault(sr =>
                                         ((sr.DateExamination.Year == model.DateConsultation.Year && sr.DateExamination.Month == model.DateConsultation.Month &&
                                         sr.DateExamination.Day == model.DateConsultation.Day &&
                                         (sr.DateExamination.Hour >= model.DateConsultation.Hour && sr.DateExamination.Hour + 3 < model.DateConsultation.Hour))
                                         //попадает на момент проведения экзамена (3 часа на экзамен)
                                         ||
                                         (sr.DateConsultation.Year == model.DateConsultation.Year && sr.DateConsultation.Month == model.DateConsultation.Month &&
                                         sr.DateConsultation.Day == model.DateConsultation.Day &&
                                         (sr.DateConsultation.Hour >= model.DateConsultation.Hour && sr.DateConsultation.Hour + 1 < model.DateConsultation.Hour)))
                                         //попадает на момент проведения консультации (1  на консультацию)
                                         && sr.ClassroomId == model.ClassroomId);
                    if (entry != null)
                    {
                        throw new Exception("Exsist ExaminationRecord");
                    }

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
                    switch(strs.Length)
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
            return entity.StudentGroupId.HasValue ? entity.StudentGroup.GroupName : entity.LessonGroup;
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
            if(string.IsNullOrEmpty(fullDiscipliineName))
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
    }
}