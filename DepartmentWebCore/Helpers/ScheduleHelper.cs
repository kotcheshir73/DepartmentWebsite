using DepartmentWebCore.Models;
using Enums;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DepartmentWebCore.Helpers
{
    public static class ScheduleHelper
    {
        private readonly static int step = 10;

        private readonly static Color ConsultColor = Color.LightGreen;

        private readonly static Color ExamColor = Color.DarkCyan;

        private readonly static Color OffsetColor = Color.SandyBrown;

        private static List<DateTime> times;

        private readonly static int colspan = 8;

        private static List<DateTime> Times
        {
            get
            {
                if (times == null)
                {
                    times = ScheduleImplementations.Helpers.ScheduleHelper.ScheduleLessonTimes();
                }
                return times;
            }
        }

        public static HtmlString ClassroomsSchedule(this IHtmlHelper helper, ScheduleClassroomsModel model)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(AddTimeRow());

            for (int i = 0, row = 0; i < model.Classrooms.Count; ++i, row++)
            {
                sb.Append("<tr class=\"schedule-row\">");

                sb.Append($"<td>{model.Classrooms[i].Number}</td>");

                var selectedRecords = model.List.Where(x => x.ClassroomId == model.Classrooms[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    sb.Append(LoadDay(selectedRecords));
                }

                sb.Append("</tr>");
            }

            return CreateTable(sb.ToString());
        }

        public static HtmlString LecturersSchedule(this IHtmlHelper helper, ScheduleLecturersModel model)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(AddTimeRow());

            for (int i = 0, row = 0; i < model.Lecturers.Count; ++i, row++)
            {
                sb.Append("<tr class=\"schedule-row\">");

                sb.Append($"<td>{model.Lecturers[i].FullName}</td>");

                var selectedRecords = model.List.Where(x => x.LecturerId == model.Lecturers[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    sb.Append(LoadDay(selectedRecords));
                }

                sb.Append("</tr>");
            }

            return CreateTable(sb.ToString());
        }

        public static HtmlString StudentGroupsSchedule(this IHtmlHelper helper, ScheduleStudentGroupsModel model)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(AddTimeRow());

            for (int i = 0, row = 0; i < model.StudentGroups.Count; ++i, row++)
            {
                sb.Append("<tr class=\"schedule-row\">");

                sb.Append($"<td>{model.StudentGroups[i].GroupName}</td>");

                var selectedRecords = model.List.Where(x => x.StudentGroupId == model.StudentGroups[i].Id).ToList();

                if (selectedRecords.Count > 0)
                {
                    sb.Append(LoadDay(selectedRecords));
                }

                sb.Append("</tr>");
            }

            return CreateTable(sb.ToString());
        }

        private static HtmlString CreateTable(string body)
        {
            return new HtmlString($"<table cellspacing=\"5\" cellpadding=\"10\" class=\"schedule-table\">{body}</table>");
        }

        /// <summary>
        /// Загрузка строки с информацией по парам
        /// </summary>
        private static string AddTimeRow()
        {
            StringBuilder sb = new StringBuilder("<tr class=\"schedule-row-time\"><td class=\"schedule-col-header\" />");
            
            for (int i = 0; i < Times.Count; ++i)
            {
                if (i > 0)
                {
                    double interval = ((Times[i] - Times[i - 1].AddMinutes(colspan * step)).TotalMinutes / step);
                    if(interval > 1)
                    {
                        sb.Append($"<td colspan=\"{interval}\"></td>");
                    }
                    else
                    {
                        sb.Append($"<td></td>");
                    }
                }

                sb.Append($"<td class=\"schedule-cell-border\" colspan=\"{colspan}\"><span>{i + 1} пара<br />{Times[i].ToString("HH:mm")}-{Times[i].AddMinutes(80).ToString("HH:mm")}</span></td>");
            }

            sb.Append("</tr>");

            return sb.ToString();
        }

        private static string LoadDay(List<ScheduleRecordViewModel> list)
        {
            StringBuilder sb = new StringBuilder();

            var groups = list.GroupBy(x => new { x.ScheduleDate, x.TimeSpanMinutes }).OrderBy(x => x.Key.ScheduleDate).ToList();
            for (int i = 0; i < groups.Count; i++)
            {
                // пропуск начала дня, если не с первой пары занятия
                if(i == 0)
                {
                    var startTime = groups[i].Key.ScheduleDate.Date.AddHours(8).AddMinutes(30);
                    var timespan = (groups[i].Key.ScheduleDate - startTime).TotalMinutes;
                    if (timespan >= step)
                    {
                        sb.Append($"<td colspan=\"{timespan / step}\"</td>");
                    }
                }

                if (groups[i].Count() > 1)
                {
                    sb.Append(SetStreamRecord(groups[i].ToList()));
                }
                else
                {
                    sb.Append(SetRecord(groups[i].First()));
                }

                if (i < groups.Count - 1)
                {
                    var timespan = (groups[i + 1].Key.ScheduleDate - groups[i].Key.ScheduleDate).TotalMinutes - groups[i].Key.TimeSpanMinutes;
                    if (timespan >= step)
                    {
                        sb.Append($"<td colspan=\"{timespan / step}\"</td>");
                    }
                }
                else
                {
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Добавляем простое занятие
        /// </summary>
        /// <param name="record"></param>
        private static string SetRecord(ScheduleRecordViewModel record)
        {
            string colSp = string.Empty;
            int colspan = record.TimeSpanMinutes / step;
            if (colspan > 1)
            {
                colSp = $" colspan=\"{colspan}\"";
            }

            Color color = record.ScheduleRecordType == ScheduleRecordType.Consultation ? ConsultColor :
                record.ScheduleRecordType == ScheduleRecordType.Examination ? ExamColor :
                record.ScheduleRecordType == ScheduleRecordType.Offset ? OffsetColor : Color.White;

            return $"<td class=\"schedule-cell-border\"{colSp} style=\"background-color:{color.Name}\"><span>{record.ToString().Replace("\r\n", "<br />")}</span></td>";
        }

        /// <summary>
        /// Добавляем потоковое занятие
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        private static string SetStreamRecord(List<ScheduleRecordViewModel> records)
        {
            StringBuilder lessons = new StringBuilder();

            // три варианта потокового занятия: 
            
            // поток по дисциплине и преподавателю
            var discandlec = records.GroupBy(x => new { x.LessonType, x.LessonDiscipline, x.LessonLecturer, x.LessonClassroom }).ToList();
            if(discandlec.Count == 1)
            {
                lessons.Append($"<span>{discandlec[0].Key.LessonType} {discandlec[0].Key.LessonDiscipline} {discandlec[0].Key.LessonClassroom}<br />{discandlec[0].Key.LessonLecturer}<br />{string.Join(',', discandlec[0].Select(x => x.LessonStudentGroup))}</span>");
            }
            else // поток по группе (занятие по подгруппам)
            {
                var group = records.GroupBy(x => new { x.LessonType, x.LessonStudentGroup }).ToList();
                if(group.Count == 1)
                {
                    lessons.Append($"<span>{string.Join("<br />", group[0].Select(x => $"{x.LessonType} {x.LessonDiscipline} {x.LessonClassroom}<br />{x.LessonLecturer}"))}{group[0].Key.LessonStudentGroup}</span>");
                }
                else // просто потоки
                {
                    foreach (var rec in records)
                    {
                        lessons.Append($"<span>{rec.ToString().Replace("\r\n", "<br />")}</span>");
                    }
                }
            }

            var counter = (records[0].ScheduleDate - records[0].ScheduleDate.Date.AddHours(8)).TotalMinutes / step + 1;
            int colspan = records[0].TimeSpanMinutes / step;
            string colSp = string.Empty;
            if (colspan > 1)
            {
                colSp = $" colspan=\"{colspan}\"";
            }
            Color color = records[0].ScheduleRecordType == ScheduleRecordType.Consultation ? ConsultColor :
                records[0].ScheduleRecordType == ScheduleRecordType.Examination ? ExamColor :
                records[0].ScheduleRecordType == ScheduleRecordType.Offset ? OffsetColor : Color.Aqua;

            return $"<td class=\"schedule-cell-border\"{colSp} style=\"background-color:{color.Name}\">{lessons.ToString()}</td>";
        }
    }
}