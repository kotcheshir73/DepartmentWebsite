using DepartmentDAL.Context;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DepartmentService.ViewModels;
using DepartmentService.BindingModels;
using DepartmentDAL;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using DepartmentDAL.Models;
using DepartmentDAL.Enums;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace DepartmentService.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly DepartmentDbContext _context;

        private readonly IClassroomService _serviceC;

        private readonly ISeasonDatesService _serviceSD;

        private readonly IStreamingLessonService _serviceSL;

        private readonly ISemesterRecordService _serviceSR;

        public ScheduleService(DepartmentDbContext context, IClassroomService serviceC, ISeasonDatesService serviceSD,
            IStreamingLessonService serviceSL, ISemesterRecordService serviceSR)
        {
            _context = context;
            _serviceC = serviceC;
            _serviceSD = serviceSD;
            _serviceSL = serviceSL;
            _serviceSR = serviceSR;
        }

        public List<ClassroomViewModel> GetClassrooms()
        {
            return _serviceC.GetClassrooms();
        }

        public List<SeasonDatesViewModel> GetSeasonDaties()
        {
            return _serviceSD.GetSeasonDaties();
        }

        public SeasonDatesViewModel GetCurrentDates()
        {
            try
            {
                var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
                if (currentSetting == null)
                {
                    return null;
                }
                return _serviceSD.GetSeasonDates(new SeasonDatesGetBindingModel { Title = currentSetting.Value });
            }
            catch (Exception)
            {
                return null;
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
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public List<SemesterRecordShortViewModel> GetScheduleSemester(ScheduleSemesterBindingModel model)
        {
            var currentDates = GetCurrentDates();
            if (currentDates == null)
            {
                throw new Exception("Выставьте учебный период");
            }
            var records = _context.SemesterRecords.Include(sr => sr.Lecturer).Include(sr => sr.Classroom).Include(sr => sr.StudentGroup).
                Where(sr => sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == currentDates.Id).ToList();
            List<SemesterRecordShortViewModel> result = new List<SemesterRecordShortViewModel>();
            for (int i = 0; i < records.Count; ++i)
            {
                string groups = GetLessonGroup(records[i]);
                if (records[i].IsStreaming)
                {//если потоковая пара
                    var recs = records.Where(rec => rec.Week == records[i].Week && rec.Day == records[i].Day && rec.Lesson == records[i].Lesson &&
                                            rec.LessonClassroom == records[i].LessonClassroom && rec.IsStreaming).ToList();
                    StringBuilder sb = new StringBuilder();
                    foreach (var rec in recs)
                    {
                        sb.Append(rec.LessonGroup + ";");
                        if (records[i] != rec)
                        {
                            records.Remove(rec);
                        }
                    }
                    groups = sb.Remove(sb.Length - 1, 1).ToString();
                    //пытаемся найти запись о потоковом занятии
                    var streamingLesson = _context.StreamingLessons.FirstOrDefault(sl => sl.IncomingGroups == groups);
                    if (streamingLesson != null)
                    {
                        groups = streamingLesson.StreamName;
                    }
                    else
                    {
                        _serviceSL.CreateStreamingLesson(new StreamingLessonRecordBindingModel
                        {
                            IncomingGroups = groups,
                            StreamName = ""
                        });
                    }
                }
                if (records[i].LessonType == LessonTypes.удл)
                {//не выводим занятие, если оно удаленное и в эту пару поставили пару
                    var recordExists = records.Exists(r => r.Week == records[i].Week && r.Day == records[i].Day && r.Lesson == records[i].Lesson &&
                                                    r.LessonType != LessonTypes.удл);
                    if (recordExists)
                    {
                        records.Remove(records[i--]);
                        continue;
                    }
                }
                result.Add(new SemesterRecordShortViewModel
                {
                    Id = records[i].Id,
                    Week = records[i].Week,
                    Day = records[i].Day,
                    Lesson = records[i].Lesson,
                    LessonType = records[i].LessonType.ToString(),
                    IsStreaming = records[i].IsStreaming,
                    LessonLecturer = GetLessonLecturer(records[i]),
                    LessonDiscipline = GetLessonDiscipline(records[i]),
                    LessonGroup = groups,
                    LessonClassroom = GetLessonClassroom(records[i])
                });
            }

            return result.OrderBy(e => e.Id).ToList();
        }

        public List<ConsultationRecordShortViewModel> GetScheduleConsultation(ScheduleConsultationBindingModel model)
        {
            var currentSetting = _context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
            if (currentSetting == null)
            {
                throw new Exception("Выставьте учебный период");
            }
            var seasonDate = _context.SeasonDates.FirstOrDefault(sd => sd.Title == currentSetting.Value);
            if (seasonDate == null)
            {
                throw new Exception("Выставьте учебный период");
            }
            var records = _context.ConsultationRecords.Include(sr => sr.Lecturer).Include(sr => sr.Classroom).Include(sr => sr.StudentGroup).
                Where(sr => sr.ClassroomId == model.ClassroomId && sr.SeasonDatesId == seasonDate.Id).ToList();
            List<ConsultationRecordShortViewModel> result = new List<ConsultationRecordShortViewModel>();
            for (int i = 0; i < records.Count; ++i)
            {
                int day = -1;
                int week = -1;
                int lesson = -1;
                string groups = GetLessonGroup(records[i]);
                if (seasonDate.DateBeginSemester < records[i].DateConsultation && seasonDate.DateEndSemester > records[i].DateConsultation)
                {//консультация назначается в семестре, определяем неделю, день и пару
                    day = ((int)(records[i].DateConsultation - seasonDate.DateBeginSemester).TotalDays % 14);
                    week = day < 8 ? 0 : 1;
                    day = day % 7;
                    lesson = 7;
                    DateTime[] lessons = new DateTime[]
                    {
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 8, 0, 0),
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 9, 40, 0),
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 11, 30, 0),
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 13, 10, 0),
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 14, 50, 0),
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 16, 30, 0),
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 18, 10, 0),
                    new DateTime(records[i].DateConsultation.Day, records[i].DateConsultation.Month, records[i].DateConsultation.Year, 19, 50, 0)
                    };
                    for (int j = 0; j < lessons.Length - j; ++i)
                    {
                        if (lessons[j] > records[i].DateConsultation && lessons[j + 1] < records[i].DateConsultation)
                        {
                            lesson = j;
                            break;
                        }
                    }
                }
                result.Add(new ConsultationRecordShortViewModel
                {
                    Id = records[i].Id,
                    Week = week,
                    Day = day,
                    Lesson = lesson,
                    DateConsultation = string.Format("{0} {1}", records[i].DateConsultation.ToShortDateString(), records[i].DateConsultation.ToShortTimeString()),
                    LessonLecturer = GetLessonLecturer(records[i]),
                    LessonDiscipline = GetLessonDiscipline(records[i]),
                    LessonGroup = groups,
                    LessonClassroom = GetLessonClassroom(records[i])
                });
            }

            return result.OrderBy(e => e.Id).ToList();
        }

        public ResultService ClearSemesterRecords(ClassroomGetBindingModel model)
        {
            try
            {
                var records = _context.SemesterRecords.Where(sr => sr.ClassroomId == model.Id);
                _context.SemesterRecords.RemoveRange(records);
                _context.SaveChanges();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService LoadScheduleHTMLForClassrooms(LoadHTMLForClassroomsBindingModel model)
        {
            var currentDates = GetCurrentDates();
            if (currentDates == null)
            {
                return ResultService.Error("currentDates_not_found", "Выставьте учебный период", 404);
            }
            WebClient web = new WebClient();
            web.Encoding = UTF8Encoding.Default;

            string strHTML = web.DownloadString(model.ScheduleUrl + "raspisan.htm");

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(strHTML);

            var nodes = document.DocumentNode.SelectNodes("//table/tr/td");
            StringBuilder error = new StringBuilder();

            var stopWords = _context.ScheduleStopWords.ToList();
            foreach (var node in nodes)
            {
                if (node.InnerText != "\r\n")
                {
                    var elem = node.ChildNodes.FirstOrDefault(e => e.Name.ToLower() == "a");
                    if (elem != null)
                    {
                        try
                        {
                            var res = ParsingPage(model.ScheduleUrl + elem.Attributes.First().Value, model.Classrooms,
                                                        (node.InnerText.Replace("\r\n", "").Replace(" ", "")), stopWords, currentDates);
                            if (!string.IsNullOrEmpty(res))
                            {
                                error.Append(res);
                            }
                            Thread.Sleep(100);
                        }
                        catch (Exception ex)
                        {
                            error.Append(node.InnerText.Replace("\r\n", "").Replace(" ", ""));
                            error.Append(": ");
                            error.Append(ex.Message);
                            error.Append("\r\n");
                        }
                    }
                }
            }
            if (error.Length > 0)
            {
                return ResultService.Error("error", error.ToString(), 400);
            }

            return ResultService.Success();
        }

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
                                                    (sr.LessonGroup == record.LessonGroup || sr.StudentGroupId == record.StudentGroupId) &&
                                                    (sr.LessonLecturer == record.LessonLecturer || sr.LecturerId == record.LecturerId) &&
                                                    (sr.LessonClassroom == record.LessonClassroom || sr.ClassroomId == record.ClassroomId) &&
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
                                                       (sr.LessonGroup == record.LessonGroup || sr.StudentGroupId == record.StudentGroupId) &&
                                                       (sr.LessonLecturer == record.LessonLecturer || sr.LecturerId == record.LecturerId) &&
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
                    StringBuilder error = new StringBuilder();
                    error.Append(ex.Message);
                    var err = ex;
                    while (err.InnerException != null)
                    {
                        error.Append("\r\nInnerException: ");
                        error.Append(err.InnerException.Message);
                        err = err.InnerException;
                    }
                    transaction.Rollback();
                    return ResultService.Error("error", error.ToString(), 400);
                }
            }
        }

        public ResultService ExportExcel(ExportToExcelClassroomsBindingModel model)
        {
            try
            {
                var excel = new Application();
                if (File.Exists(Path.GetDirectoryName(model.FileName)))
                {
                    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = 5;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false,
                        XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                for (int r = 1; r <= model.Classrooms.Count; r++)
                {
                    var excelworksheet = (Worksheet)excelsheets.get_Item(r);//Получаем ссылку на лист
                    excelworksheet.Cells.Clear();
                    excelworksheet.Name = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
                    excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    excelworksheet.PageSetup.RightMargin = 0;
                    excelworksheet.PageSetup.LeftMargin = 0;
                    excelworksheet.PageSetup.TopMargin = 0;
                    excelworksheet.PageSetup.BottomMargin = 0;
                    excelworksheet.PageSetup.CenterHorizontally = true;
                    excelworksheet.PageSetup.CenterVertically = true;
                    #region шапка
                    Range excelcells = excelworksheet.get_Range("A2", "I8");
                    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Constants.xlCenter;
                    excelcells.VerticalAlignment = Constants.xlCenter;
                    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 8;
                    excelcells = excelworksheet.get_Range("A10", "I16");
                    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Constants.xlCenter;
                    excelcells.VerticalAlignment = Constants.xlCenter;
                    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 8;
                    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                    var simbols = new[] { "B", "C", "D", "E", "F", "G", "H", "I" };
                    var times = new[] { "08:00-09:30", "09:40-11:10", "11:30-13:00", "13:10-14:40", "14:50-16:20",
                        "16:30-18:00", "18:10-19:40", "19:50-21:20" };
                    excelcells = excelworksheet.get_Range("A2", "A2");
                    excelcells.Value2 = "I неделя";
                    excelcells.ColumnWidth = 9;
                    excelcells.RowHeight = 30;
                    excelcells = excelworksheet.get_Range("A10", "A10");
                    excelcells.Value2 = "II неделя";
                    excelcells.RowHeight = 30;
                    for (int i = 0; i < 6; i++)
                    {
                        excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
                        excelcells.RowHeight = 40;
                        excelcells.Value2 = days[i];
                        excelcells = excelworksheet.get_Range("A" + (11 + i), "A" + (11 + i));
                        excelcells.RowHeight = 40;
                        excelcells.Value2 = days[i];
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        excelcells = excelworksheet.get_Range(simbols[j] + 2, simbols[j] + 2);
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = j + 1 + " пара\r\n" + times[j];
                        excelcells = excelworksheet.get_Range(simbols[j] + 10, simbols[j] + 10);
                        excelcells.Value2 = j + 1 + " пара\r\n" + times[j];
                    }
                    #endregion
                    #region тело

                    var list = GetScheduleSemester(new ScheduleSemesterBindingModel
                    {
                        ClassroomId = model.Classrooms[r - 1]
                    });
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Week == 0)
                        {
                            excelcells = excelworksheet.get_Range(simbols[list[i].Lesson] + (list[i].Day + 3),
                                simbols[list[i].Lesson] + (list[i].Day + 3));
                        }
                        if (list[i].Week == 1)
                        {
                            excelcells = excelworksheet.get_Range(simbols[list[i].Lesson] + (list[i].Day + 11),
                                simbols[list[i].Lesson] + (list[i].Day + 11));
                        }
                        excelcells.Value2 = list[i].LessonType + " " + list[i].LessonDiscipline + Environment.NewLine +
                                list[i].LessonLecturer + Environment.NewLine + list[i].LessonGroup;
                    }
                    #endregion
                    #region аудитория
                    excelcells = excelworksheet.get_Range("A1", "I1");
                    excelcells.Merge(Type.Missing);
                    excelcells.Font.Bold = true;
                    excelcells.Value2 = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
                    excelcells.RowHeight = 25;
                    excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 14;
                    #endregion
                }

                excel.Workbooks[1].Save();
                excel.Quit();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        public ResultService ExportHTML(ExportToHTMLClassroomsBindingModel model)
        {
            try
            {
                for (int i = 0; i < model.Classrooms.Count; ++i)
                {
                    if (File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" +
                        model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt"))
                    {
                        File.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" +
                            model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt");
                    }
                    var writer = new StreamWriter(new FileStream(model.FilePath + "\\" + model.Classrooms[i].Split(new char[] { '\\', '/' })[0] + "_sem.txt",
                        FileMode.Create));
                    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                    var list = GetScheduleSemester(new ScheduleSemesterBindingModel
                    {
                        ClassroomId = model.Classrooms[i]
                    }
                    );
                    #region тело
                    for (int j = 0; j < 2; j++)
                    {
                        writer.WriteLine(string.Format("<p class=\"rteright\">Дата обновления: {0} </ p >", DateTime.Now.ToShortDateString()));
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
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>1 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>08:00-09:30</span></span></td>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>2 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>09:40-11:10</span></span></td>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>3 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>11:30-13:00</span></span></td>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>4 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>13:10-14:40</span></span></td>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>5 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>14:50-16:20</span></span></td>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>6 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>16:30-18:00</span></span></td>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>7 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>18:10-19:40</span></span></td>");
                        writer.WriteLine("\t\t\t<td class='rtecenter' style='width: 70px; background-color: rgb(0, 153, 51)'>");
                        writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>8 пара<br />");
                        writer.WriteLine("\t\t\t\t<span style='font-size:10px;'>19:50-21:20</span></span></td>\r\n\t\t</tr>");
                        for (int k = 0; k < 6; k++)
                        {
                            writer.WriteLine("\t\t<tr style='height: 40px'>");
                            writer.WriteLine("\t\t\t<td class='rtecenter' style='background-color: rgb(153, 0, 0)'>");
                            writer.WriteLine("\t\t\t\t<span style='color:#ffffff;'>" + days[k] + "</span></td>");
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
                                    writer.WriteLine("\t\t\t\t<span style='font-size:8px;'>" + record.LessonType + " " +
                                        record.LessonDiscipline + "<br />");
                                    writer.WriteLine("\t\t\t\t" + record.LessonLecturer + "<br />");
                                    writer.WriteLine("\t\t\t\t" + record.LessonGroup + "</span></td>");
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
                return ResultService.Error("error", ex.Message, 400);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulrUrl"></param>
        /// <param name="classrooms"></param>
        private string ParsingPage(string schedulrUrl, List<string> classrooms, string groupName, List<ScheduleStopWord> stopWords, SeasonDatesViewModel currentDates)
        {
            string[] days = new string[] { "Пнд", "Втр", "Срд", "Чтв", "Птн", "Сбт" };
            WebClient web = new WebClient();
            web.Encoding = UTF8Encoding.Default;
            string pageHTML = web.DownloadString(schedulrUrl);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageHTML);
            var pageNodes = document.DocumentNode.SelectNodes("//table/tr/td");
            int week = -1;
            int day = -1;
            int para = -1;
            StringBuilder error = new StringBuilder();
            try
            {
                foreach (var pageNode in pageNodes)
                {
                    string text = pageNode.InnerText.Replace("\r\n", "").Replace(" ", "");
                    if (days.Contains(text))
                    {
                        if (days[0].Contains(text))
                        {
                            week++;
                            day = -1;
                        }
                        day++;
                        para = -1;
                    }
                    if (week > -1)
                    {
                        var elem = pageNode.ChildNodes.First().NextSibling;
                        if (elem.Name.ToLower() == "font")
                        {
                            para++;
                            var lesson = pageNode.InnerText.Replace("\r\n", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (lesson[0] == "_")
                            {
                                // пустая пара, переходим к следующей
                                continue;
                            }
                            var entityFirst = new SemesterRecordRecordBindingModel();
                            var entitySecond = new SemesterRecordRecordBindingModel();

                            entityFirst.Week = week;
                            entityFirst.Day = day;
                            entityFirst.Lesson = para;
                            //entityFirst.SeasonDatesId = currentDates.Id;

                            entitySecond.Week = week;
                            entitySecond.Day = day;
                            entitySecond.Lesson = para;
                            // entitySecond.SeasonDatesId = currentDates.Id;

                            AnalisString(pageNode.InnerText, classrooms, groupName, stopWords, entityFirst, entitySecond);

                            CheckNewSemesterRecordForConflictAndSave(entityFirst, error);
                            CheckNewSemesterRecordForConflictAndSave(entitySecond, error);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return error.ToString();
        }

        private void AnalisString(string text, List<string> classrooms, string groupName, List<ScheduleStopWord> stopWords,
            SemesterRecordRecordBindingModel recordFirst, SemesterRecordRecordBindingModel recordSecond)
        {
            text = Regex.Replace(text, @"(\-?)(\s?)\d(\s?)п/г", "");
            var lesson = text.Replace("\r\n", "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            recordFirst.LessonGroup = groupName;
            recordFirst.LessonDiscipline += lesson[0];
            if (!string.IsNullOrEmpty(recordFirst.LessonGroup))
            {
                var group = _context.StudentGroups.FirstOrDefault(sg => sg.GroupName == recordFirst.LessonGroup);
                if (group != null)
                {
                    recordFirst.StudentGroupId = group.Id;
                }
            }
            int i = 1;
            for (; i < lesson.Length - 1; ++i)
            {
                if (i < lesson.Length - 3 && lesson[i].ToUpper() == lesson[i] &&
                    lesson[i + 1].Length == 1 && lesson[i + 1].ToUpper() == lesson[i + 1] &&
                    lesson[i + 2].Length == 1 && lesson[i + 2].ToUpper() == lesson[i + 2])
                {//Шаблон для преподавателя "ФАМИЛИЯ И О"
                    break;
                }
                if (stopWords.FirstOrDefault(sw => sw.StopWord.Contains(lesson[i].ToUpper()) && sw.StopWordType == ScheduleStopWordTypes.Преподаватель) != null)
                {
                    break;
                }
                recordFirst.LessonDiscipline += lesson[i] + " ";
            }

            if (i < lesson.Length - 3)
            {
                if (stopWords.FirstOrDefault(sw => sw.StopWord.Contains(lesson[i].ToUpper()) && sw.StopWordType == ScheduleStopWordTypes.Преподаватель) != null)
                {
                    recordFirst.LessonLecturer = lesson[i++];
                }
                else
                {
                    recordFirst.LessonLecturer = lesson[i++] + " " + lesson[i++] + "." + lesson[i++] + ".";
                }
            }
            else if (stopWords.FirstOrDefault(sw => sw.StopWord.Contains(lesson[i].ToUpper()) && sw.StopWordType == ScheduleStopWordTypes.Преподаватель) != null)
            {
                recordFirst.LessonLecturer = lesson[i++];
            }
            if (!string.IsNullOrEmpty(recordFirst.LessonLecturer))
            {
                string searchName = recordFirst.LessonLecturer[0] + recordFirst.LessonLecturer.Split(' ')[0].Substring(1).ToLower();
                if (recordFirst.LessonLecturer.Split(' ').Length > 1)
                {
                    string firstName = recordFirst.LessonLecturer.Split(' ')[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string patronumic = recordFirst.LessonLecturer.Split(' ')[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == searchName &&
                                            ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                                            ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                    if (lecturer != null)
                    {
                        recordFirst.LecturerId = lecturer.Id;
                    }
                }
                else
                {
                    var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == searchName);
                    if (lecturer != null)
                    {
                        recordFirst.LecturerId = lecturer.Id;
                    }
                }
            }

            if (i < lesson.Length)
            {
                if (lesson[i] == "-")
                {
                    i++;
                }
                recordFirst.LessonClassroom = lesson[i++];

                if (classrooms.Any(c => recordFirst.LessonClassroom.Contains(c)))
                {
                    recordFirst.ClassroomId = classrooms.FirstOrDefault(c => recordFirst.LessonClassroom.Contains(c));
                }
            }

            recordFirst.LessonType = LessonTypes.нд.ToString();
            if (recordFirst.LessonDiscipline.StartsWith("лек."))
            {
                recordFirst.LessonType = LessonTypes.лек.ToString();
                recordFirst.LessonDiscipline = recordFirst.LessonDiscipline.Remove(0, 4);
            }
            if (recordFirst.LessonDiscipline.StartsWith("пр."))
            {
                recordFirst.LessonType = LessonTypes.пр.ToString();
                recordFirst.LessonDiscipline = recordFirst.LessonDiscipline.Remove(0, 3);
            }
            if (recordFirst.LessonDiscipline.StartsWith("лаб."))
            {
                recordFirst.LessonType = LessonTypes.лаб.ToString();
                recordFirst.LessonDiscipline = recordFirst.LessonDiscipline.Remove(0, 4);
            }


            if (i < lesson.Length)
            {

                recordSecond.LessonDiscipline = "";
                recordSecond.LessonGroup = recordFirst.LessonGroup;
                if (recordFirst.StudentGroupId != null)
                {
                    recordSecond.StudentGroupId = recordFirst.StudentGroupId;
                }

                if (i < lesson.Length - 3)
                {
                    if (stopWords.FirstOrDefault(sw => sw.StopWord.Contains(lesson[i].ToUpper()) && sw.StopWordType == ScheduleStopWordTypes.Преподаватель) != null)
                    {
                        recordSecond.LessonLecturer = lesson[i++];
                    }
                    else
                    {
                        recordSecond.LessonLecturer = lesson[i++] + " " + lesson[i++] + "." + lesson[i++] + ".";
                    }
                }
                else if (stopWords.FirstOrDefault(sw => sw.StopWord.Contains(lesson[i].ToUpper()) && sw.StopWordType == ScheduleStopWordTypes.Преподаватель) != null)
                {
                    recordSecond.LessonLecturer = lesson[i++];
                }
                if (!string.IsNullOrEmpty(recordSecond.LessonLecturer))
                {
                    string searchName = recordSecond.LessonLecturer[0] + recordSecond.LessonLecturer.Split(' ')[0].Substring(1).ToLower();
                    if (recordSecond.LessonLecturer.Split(' ').Length > 1)
                    {
                        string firstName = recordSecond.LessonLecturer.Split(' ')[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0];
                        string patronumic = recordSecond.LessonLecturer.Split(' ')[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[1];
                        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == searchName &&
                                                ((l.FirstName.Length > 0 && l.FirstName.Contains(firstName)) || l.FirstName.Length == 0) &&
                                                ((l.Patronymic.Length > 0 && l.Patronymic.Contains(patronumic)) || l.Patronymic.Length == 0));
                        if (lecturer != null)
                        {
                            recordSecond.LecturerId = lecturer.Id;
                        }
                    }
                    else
                    {
                        var lecturer = _context.Lecturers.FirstOrDefault(l => l.LastName == searchName);
                        if (lecturer != null)
                        {
                            recordSecond.LecturerId = lecturer.Id;
                        }
                    }
                }

                if (i < lesson.Length)
                {
                    if (lesson[i] == "-")
                    {
                        i++;
                    }
                    recordSecond.LessonClassroom = lesson[i++];
                    if (classrooms.Any(c => recordSecond.LessonClassroom.Contains(c)))
                    {
                        recordSecond.ClassroomId = classrooms.FirstOrDefault(c => recordSecond.LessonClassroom.Contains(c));
                    }

                    recordSecond.LessonType = LessonTypes.нд.ToString();
                    if (recordFirst.LessonDiscipline.StartsWith("лек."))
                    {
                        recordSecond.LessonType = LessonTypes.лек.ToString();
                    }
                    if (recordFirst.LessonDiscipline.StartsWith("пр."))
                    {
                        recordSecond.LessonType = LessonTypes.пр.ToString();
                    }
                    if (recordFirst.LessonDiscipline.StartsWith("лаб."))
                    {
                        recordSecond.LessonType = LessonTypes.лаб.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Проверяем добавляемую пару на конфликты
        /// </summary>
        /// <param name="record"></param>
        /// <param name="error"></param>
        private ResultService CheckNewSemesterRecordForConflictAndSave(SemesterRecordRecordBindingModel record, StringBuilder error)
        {
            if (record.ClassroomId != null)
            {
                var exsistRecord = _context.SemesterRecords.FirstOrDefault(r => r.Week == record.Week &&
                                        r.Day == record.Day && r.Lesson == record.Lesson &&
                                        r.ClassroomId == record.ClassroomId);
                if (exsistRecord != null)
                {//если на этой неделе в этот день этой парой в этой аудитории уже есть занятие
                    if (exsistRecord.LessonDiscipline == record.LessonDiscipline &&
                        exsistRecord.LessonLecturer == record.LessonLecturer &&
                        exsistRecord.LessonType.ToString() == record.LessonType)
                    {//если совпадает дисицпилна, преподаватель и тип занятия, то это потоковое занятие
                        record.IsStreaming = true;
                        exsistRecord.IsStreaming = true;

                        return _serviceSR.CreateSemesterRecord(record);
                    }
                    else if (exsistRecord.LessonType == LessonTypes.удл)
                    {//занятие было помечено как удаленное, т.е. по факту пара не проводилась, так что просто удаляем ее
                        var result = _serviceSR.DeleteSemesterRecord(new SemesterRecordGetBindingModel { Id = exsistRecord.Id });
                        if (!result.Succeeded)
                        {
                            return result;
                        }
                        return _serviceSR.CreateSemesterRecord(record);
                    }
                    else
                    {
                        error.Append(string.Format("Конфликт: {0} {1} {2}: {3} - {4}\r\n", record.Week, record.Day, record.Lesson,
                            exsistRecord.LessonGroup, record.LessonGroup));
                    }
                }
                else
                {
                    return _serviceSR.CreateSemesterRecord(record);
                }
            }
            return ResultService.Error("error", "classroom not found", 404);
        }

        private string GetLessonLecturer(SemesterRecord entity)
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

        private string GetLessonLecturer(ConsultationRecord entity)
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

        private string GetLessonDiscipline(SemesterRecord entity)
        {
            string str = entity.LessonDiscipline;

            if (str.Length > 10)
            {
                var strs = str.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder();
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
                            if (strs[i][j] == '-')
                            {
                                continue;
                            }
                            if (strs[i][j].ToString().ToUpper() == strs[i][j].ToString())
                            {
                                sb.Append(strs[i][j].ToString().ToUpper());
                            }
                        }
                    }
                }
                str = sb.ToString();
            }
            return str;
        }

        private string GetLessonDiscipline(ConsultationRecord entity)
        {
            string str = entity.LessonDiscipline;

            if (str.Length > 10)
            {
                var strs = str.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder();
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
                            if (strs[i][j] == '-')
                            {
                                continue;
                            }
                            if (strs[i][j].ToString().ToUpper() == strs[i][j].ToString())
                            {
                                sb.Append(strs[i][j].ToString().ToUpper());
                            }
                        }
                    }
                }
                str = sb.ToString();
            }
            return str;
        }

        private string GetLessonGroup(SemesterRecord entity)
        {
            return entity.StudentGroupId.HasValue ? entity.StudentGroup.GroupName : entity.LessonGroup;
        }

        private string GetLessonGroup(ConsultationRecord entity)
        {
            return entity.StudentGroupId.HasValue ? entity.StudentGroup.GroupName : entity.LessonGroup;
        }

        private string GetLessonClassroom(SemesterRecord entity)
        {
            return string.IsNullOrEmpty(entity.ClassroomId) ? entity.LessonClassroom : entity.ClassroomId;
        }

        private string GetLessonClassroom(ConsultationRecord entity)
        {
            return string.IsNullOrEmpty(entity.ClassroomId) ? entity.LessonClassroom : entity.ClassroomId;
        }
    }
}
