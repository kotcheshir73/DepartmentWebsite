using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DepartmentService.Helpers
{
    class ExportScheduleToHTML
    {
        public static ResultService ExportSemesterRecordHTML(List<ScheduleLessonTimeViewModel> times, List<SemesterRecordShortViewModel> records, ExportToHTMLClassroomsBindingModel model)
        {
            try
            {
                for (int i = 0; i < model.Classrooms.Count; ++i)
                {
                    if (File.Exists(model.FilePath + "\\" +
                        model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt"))
                    {
                        File.Delete(model.FilePath + "\\" +
                            model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt");
                    }
                    var writer = new StreamWriter(new FileStream(model.FilePath + "\\" + model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt",
                        FileMode.Create));
                    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[i]).ToList();

                    #region тело
                    writer.WriteLine(string.Format("<p class=\"rteright\">Дата обновления: {0} </ p >", DateTime.Now.ToShortDateString()));
                    for (int j = 0; j < 2; j++)
                    {
                        writer.WriteLine("<table align='center' border='1' cellpadding='1' cellspacing='1'>\r\n\t<tbody>");
                        writer.WriteLine("\t\t<tr>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 40px; background-color: rgb(0, 153, 51)'>");
                        if (j == 0)
                        {
                            writer.WriteLine("\t\t\t<span style='color:#ffffff;'>I</span><span style='color:#ffffff;'> неделя</span></td>");
                        }
                        else
                        {
                            writer.WriteLine("\t\t\t<span style='color:#ffffff;'>II</span><span style='color:#ffffff;'> неделя</span></td>");
                        }
                        for (int t = 0; t < times.Count; ++t)
                        {
                            writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                            writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;font-size:10px;'>{0}<br />", times[t].Title));
                            writer.WriteLine(string.Format("\t\t\t\t{0} - {1}</span></td>", times[t].TimeBeginLesson, times[t].TimeEndLesson));
                        }
                        for (int k = 0; k < 6; k++)
                        {
                            writer.WriteLine("\t\t<tr style='height: 40px'>");
                            writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(153, 0, 0)'>");
                            writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;'>{0}</span></td>", days[k]));
                            for (int r = 0; r < 8; r++)
                            {
                                if (r % 2 != 0)
                                {
                                    writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(255, 255, 255)'>");
                                }
                                else
                                {
                                    writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(204, 204, 204)'>");
                                }
                                if (list.Exists(rec => rec.Week == j && rec.Day == k && rec.Lesson == r))
                                {
                                    var record = list.Find(rec => rec.Week == j && rec.Day == k && rec.Lesson == r);
                                    writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0} {1}<br />{2}<br />{3}</span></td>",
                                        record.LessonType, record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
                                }
                                else
                                {
                                    writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
                                }
                            }
                            writer.WriteLine("\t\t</tr>");
                        }
                        writer.WriteLine("\t</tbody>\r\n</table>\r\n<p>&nbsp;</p>");
                    }
                    #endregion
                    writer.Close();
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService ExportOffsetRecordHTML(List<ScheduleLessonTimeViewModel> times, List<OffsetRecordShortViewModel> records, ExportToHTMLClassroomsBindingModel model)
        {
            try
            {
                for (int i = 0; i < model.Classrooms.Count; ++i)
                {
                    if (File.Exists(model.FilePath + "\\" +
                    model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_off.txt"))
                    {
                        File.Delete(model.FilePath + "\\" +
                        model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_off.txt");
                    }
                    var writer = new StreamWriter(new FileStream(model.FilePath + "\\" +
                        model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_off.txt", FileMode.Create));
                    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[i]).ToList();

                    #region тело
                    writer.WriteLine(string.Format("<p class=\"rteright\">Дата обновления: {0} </ p >", DateTime.Now.ToShortDateString()));
                    for (int j = 0; j < 2; j++)
                    {
                        writer.WriteLine("<table align='center' border='1' cellpadding='1' cellspacing='1'>\r\n\t<tbody>");
                        writer.WriteLine("\t\t<tr>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 40px; background-color: rgb(0, 153, 51)'>");
                        if (j == 0)
                        {
                            writer.WriteLine("\t\t\t<span style='color:#ffffff;'>I</span><span style='color:#ffffff;'> неделя</span></td>");
                        }
                        else
                        {
                            writer.WriteLine("\t\t\t<span style='color:#ffffff;'>II</span><span style='color:#ffffff;'> неделя</span></td>");
                        }
                        for (int t = 0; t < times.Count; ++t)
                        {
                            writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                            writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;font-size:10px;'>{0}<br />", times[t].Title));
                            writer.WriteLine(string.Format("\t\t\t\t{0} - {1}</span></td>", times[t].TimeBeginLesson, times[t].TimeEndLesson));
                        }
                        for (int k = 0; k < 6; k++)
                        {
                            writer.WriteLine("\t\t<tr style='height: 40px'>");
                            writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(153, 0, 0)'>");
                            writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;'>{0}</span></td>", days[k]));
                            for (int r = 0; r < 8; r++)
                            {
                                if (r % 2 != 0)
                                {
                                    writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(255, 255, 255)'>");
                                }
                                else
                                {
                                    writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(204, 204, 204)'>");
                                }
                                if (list.Exists(rec => rec.Week == j && rec.Day == k && rec.Lesson == r))
                                {
                                    var record = list.Find(rec => rec.Week == j && rec.Day == k && rec.Lesson == r);
                                    writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>зач. {0}<br />{1}<br />{2}</span></td>",
                                        record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
                                }
                                else
                                {
                                    writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
                                }
                            }
                            writer.WriteLine("\t\t</tr>");
                        }
                        writer.WriteLine("\t</tbody>\r\n</table>\r\n<p>&nbsp;</p>");
                    }
                    #endregion
                    writer.Close();
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService ExportExaminationRecordHTML(List<ScheduleLessonTimeViewModel> times, List<ExaminationRecordShortViewModel> records, ExportToHTMLClassroomsBindingModel model)
        {
            try
            {
                var currentDates = ScheduleHelper.GetCurrentDates();

                for (int i = 0; i < model.Classrooms.Count; ++i)
                {
                    if (File.Exists(model.FilePath + "\\" +
                    model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_exa.txt"))
                    {
                        File.Delete(model.FilePath + "\\" +
                    model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_exa.txt");
                    }
                    var writer = new StreamWriter(new FileStream(model.FilePath + "\\" +
                    model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_exa.txt", FileMode.Create));

                    #region тело
                    writer.WriteLine("<table align='center' border='1' cellpadding='1' cellspacing='1'>\r\n\t<tbody>");
                    writer.WriteLine("\t\t<tr>");
                    writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 40px; background-color: rgb(0, 153, 51)'>");
                    writer.WriteLine("\t\t\t<span style='color:#ffffff;'>Сессия</span></td>");
                    for (int t = 0; t < times.Count; ++t)
                    {
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine(string.Format("\t\t\t\t<span style='color:#ffffff;font-size:10px;'>{0}<br />", times[t].Title));
                        writer.WriteLine(string.Format("\t\t\t\t{0} - {1}</span></td>", times[t].TimeBeginLesson, times[t].TimeEndLesson));
                    }


                    var currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
                    var days = (Convert.ToDateTime(currentDates.DateEndExamination) - currentdate).Days;

                    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[i]).ToList();


                    for (int k = 0; k < days; k++)
                    {
                        writer.WriteLine("\t\t<tr style='height: 40px'>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(153, 0, 0)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>" + currentdate.ToShortDateString() + "</span></td>");
                        for (int r = 0; r < times.Count; r++)
                        {
                            if (r % 2 != 0)
                            {
                                writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(255, 255, 255)'>");
                            }
                            else
                            {
                                writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(204, 204, 204)'>");
                            }
                            switch (r)
                            {
                                case 0:
                                    if (list.Exists(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
                                                                                                        times[0].DateBeginLesson.Hour))
                                    {
                                        var record = list.Find(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
                                                                                                                    times[0].DateBeginLesson.Hour);
                                        writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
                                            record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
                                    }
                                    else
                                    {
                                        writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
                                    }
                                    break;
                                case 1:
                                    if (list.Exists(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
                                                                                                        times[1].DateBeginLesson.Hour))
                                    {
                                        var record = list.Find(rec => rec.DateExamination.Date == currentdate.Date && rec.DateExamination.Hour ==
                                                                                                                    times[1].DateBeginLesson.Hour);
                                        writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
                                            record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
                                    }
                                    else
                                    {
                                        writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
                                    }
                                    break;
                                case 2:
                                    if (list.Exists(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
                                                                                                            times[2].DateBeginLesson.Hour))
                                    {
                                        var record = list.Find(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
                                                                                                                        times[2].DateBeginLesson.Hour);
                                        writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
                                            record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
                                    }
                                    else
                                    {
                                        writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
                                    }
                                    break;
                                case 3:
                                    if (list.Exists(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
                                                                                                            times[3].DateBeginLesson.Hour))
                                    {
                                        var record = list.Find(rec => rec.DateConsultation.Date == currentdate.Date && rec.DateConsultation.Hour ==
                                                                                                                        times[3].DateBeginLesson.Hour);
                                        writer.WriteLine(string.Format("\t\t\t\t<span style='font-size:8px;'>{0}<br />{1}<br />{2}</span></td>",
                                            record.LessonDiscipline, record.LessonLecturer, record.LessonGroup));
                                    }
                                    else
                                    {
                                        writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>-</span></td>");
                                    }
                                    break;
                            }
                        }
                        writer.WriteLine("\t\t</tr>");
                        currentdate = currentdate.AddDays(1);
                    }
                    writer.WriteLine("\t</tbody>\r\n</table>\r\n<p>&nbsp;</p>");
                    #endregion
                    writer.Close();
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}
