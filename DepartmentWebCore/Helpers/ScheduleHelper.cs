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
using System.Threading.Tasks;

namespace DepartmentWebCore.Helpers
{
    public static class ScheduleHelper
    {
        private readonly static int step = 10;

        private readonly static Color ConsultColor = Color.LightGreen;

        private readonly static Color ExamColor = Color.DarkCyan;

        private readonly static Color OffsetColor = Color.SandyBrown;

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

            return new HtmlString($"<table cellspacing=\"5\" cellpadding=\"10\" border=\"1\" class=\"schedule-table\">{sb.ToString()}</table>");
        }

        /// <summary>
        /// Загрузка строки с информацией по парам
        /// </summary>
        private static string AddTimeRow()
        {
            StringBuilder sb = new StringBuilder("<tr class=\"schedule-row-time\"><td class=\"schedule-col-header\" />");

            List<DateTime> times = ScheduleImplementations.Helpers.ScheduleHelper.ScheduleLessonTimes();
            for (int i = 0; i < times.Count; ++i)
            {
                int colspan = 9;

                if (i > 0)
                {
                    sb.Append($"<td colspan=\"{(times[i - 1].AddMinutes(90) - times[i]).TotalMinutes / step}\"></td>");
                }

                sb.Append($"<td colspan=\"{colspan}\"><span>{i + 1} пара<br />{times[i].ToString("HH:mm")}-{times[i].AddMinutes(90).ToString("HH:mm")}</span></td>");
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
                if (groups[i].Count() > 1)
                {// потоки
                    //var records = groups[i].ToList();
                    //var control = new Panel
                    //{
                    //    Location = new Point(0, 0),
                    //    Dock = DockStyle.Fill,
                    //    Margin = new Padding(0),
                    //};
                    //// группируем все занятия этой пары по типу занятий и кидаем все на панель (скорее всего, будет 1 кнопка для потокового занятия, 
                    //// но может стоять пара и консультация, так как пары по факту нету)
                    //var localgroup = records.GroupBy(x => new { x.ScheduleRecordType, x.LessonType });
                    //int count = 0;
                    //foreach (var local in localgroup)
                    //{
                    //    count++;
                    //    var button = MakeButton(local.ToList());
                    //    button.Dock = count == localgroup.Count() ? DockStyle.Fill : DockStyle.Top;
                    //    control.Controls.Add(button);
                    //}

                    //var counter = (records[0].ScheduleDate - records[0].ScheduleDate.Date.AddHours(8)).TotalMinutes / step + 1;
                    //int colspan = records[0].TimeSpanMinutes / step;

                    //tableLayoutPanel.Controls.Add(control, (int)counter, row);
                    //if (colspan > 1)
                    //{
                    //    tableLayoutPanel.SetColumnSpan(control, colspan);
                    //}
                }
                else
                {
                    sb.Append(SetRecord(groups[i].First()));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Установка контрола для записи
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
                record.ScheduleRecordType == ScheduleRecordType.Offset ? OffsetColor : Color.Gray;

            return $"<td{colSp} style=\"background-color:{color.Name}\"><span>{record.ToString().Replace("\r\n", "<br />")}</span></td>";
        }
    }
}