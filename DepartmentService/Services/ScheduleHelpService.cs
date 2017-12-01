using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DepartmentService.Services
{
    class ScheduleHelpService
    {
        public static SeasonDates GetCurrentDates(DepartmentDbContext context)
        {
            var currentSetting = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
            if (currentSetting == null)
            {
                throw new Exception("CurrentSetting not found");
            }

            var currentDates = context.SeasonDates.Where(sd => sd.Title == currentSetting.Value).FirstOrDefault();
            if (currentDates == null)
            {
                throw new Exception("CurrentDates not found");
            }
            return currentDates;
        }

        public static bool CheckCreateConsultation(DepartmentDbContext context,
            ConsultationRecordRecordBindingModel model, SeasonDates seasonDate)
        {
            DateTime[] lessons;

            #region консультация назначается в семестре, определяем неделю, день и пару
            var dateBeginSemester = Convert.ToDateTime(seasonDate.DateBeginSemester);
            var dateEndSemester = Convert.ToDateTime(seasonDate.DateEndSemester);
            if (dateBeginSemester < model.DateConsultation && dateEndSemester > model.DateConsultation)
            {
                // по дате консультации определяем неделю, день и пару
                int day = ((int)(model.DateConsultation - dateBeginSemester).TotalDays % 14);
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
                var entry = context.SemesterRecords.FirstOrDefault(sr => sr.Week == week && sr.Day == day && sr.Lesson == lesson &&
                                                                           sr.ClassroomId == model.ClassroomId && sr.LessonType != LessonTypes.удл);
                if (entry != null)
                {
                    throw new Exception("Exsist SemesterRecord");
                }

                model.Week = week;
                model.Day = day;
                model.Lesson = lesson;
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

                model.Week = week;
                model.Day = day;
                model.Lesson = lesson;
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
                    if (lessons[i] >= model.DateConsultation && lessons[i + 1] >= model.DateConsultation)
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

                model.Week = 0;
                model.Day = day;
                model.Lesson = lesson;
            }
            #endregion
            return true;
        }

        public static string GetLessonLecturer(ScheduleRecord entity)
        {
            string str = entity.LecturerId.HasValue ? entity.Lecturer.ToString() : entity.LessonLecturer;
            if (!entity.LecturerId.HasValue)
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = "";
                }
                else
                {
                    var strs = str.Split(' ');
                    if (strs.Length == 1)
                    {
                        str = string.Format("{0}{1}", strs[0][0], strs[0].Substring(1).ToLower());
                    }
                    else if (strs.Length == 2)
                    {
                        str = string.Format("{0}{1} {2}", strs[0][0], strs[0].Substring(1).ToLower(), strs[1]);
                    }
                    else if (strs.Length == 3)
                    {
                        str = string.Format("{0}{1} {2} {3}", strs[0][0], strs[0].Substring(1).ToLower(), strs[1], strs[2]);
                    }
                }
            }
            return str;
        }

        public static string GetLessonDiscipline(ScheduleRecord entity)
        {
            string str = entity.DisciplineId.HasValue ? entity.Discipline.DisciplineShortName : entity.LessonDiscipline;

            if (string.IsNullOrEmpty(str) && entity.DisciplineId.HasValue)
            {
                str = entity.LessonDiscipline;
            }

            if (str.Length > 10)
            {
                StringBuilder sb = new StringBuilder();
                if (str.Contains("-"))
                {
                    var substrs = str.Split(new char[] { '-', '.', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var glas = new List<char> { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
                    for (int j = 0; j < substrs.Length; ++j)
                    {
                        for (int t = 0; t < substrs[j].Length; ++t)
                        {
                            if (t < 4)
                            {
                                sb.Append(substrs[j][t]);
                            }
                            else if (!glas.Contains(substrs[j][t]))
                            {
                                sb.Append(substrs[j][t]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        sb.Append('-');
                    }
                }
                else
                {
                    var strs = str.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strs.Length; ++i)
                    {
                        if (strs.Length == 1)
                        {
                            sb.Append(string.Format("{0}.", strs[0].Substring(0, 8)));
                        }
                        else if (strs[i].Length == 1)
                        {
                            sb.Append(strs[i]);
                        }
                        else if (strs[i].ToUpper() == strs[i])
                        {
                            sb.Append(strs[i].ToUpper());
                        }
                        else
                        {
                            sb.Append(strs[i][0].ToString().ToUpper());
                            for (int j = 1; j < strs[i].Length; ++j)
                            {
                                if (strs[i][j].ToString().ToUpper() == strs[i][j].ToString())
                                {
                                    sb.Append(strs[i][j].ToString().ToUpper());
                                }
                            }
                        }
                    }
                    str = sb.ToString();
                }
            }
            else
            {
                str = str.Replace(" ", "");
            }
            return str;
        }

        public static string GetLessonGroup(ScheduleRecord entity)
        {
            return entity.StudentGroupId.HasValue ? entity.StudentGroup.GroupName : entity.LessonGroup;
        }

        public static string GetLessonClassroom(ScheduleRecord entity)
        {
            return string.IsNullOrEmpty(entity.ClassroomId) ? entity.LessonClassroom : entity.ClassroomId;
        }
    }
}
