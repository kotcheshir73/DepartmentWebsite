using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DepartmentService.Helpers
{
    class ExportScheduleToExcel
    {
        public static ResultService ExportSemesterRecordExcel(List<ScheduleLessonTimeViewModel> times, List<SemesterRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
        {
            var excel = new Application();
            try
            {
                if (File.Exists(model.FileName))
                {
                    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = model.Classrooms.Count;
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
                    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                    var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

                    Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days.Length));
                    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Constants.xlCenter;
                    excelcells.VerticalAlignment = Constants.xlCenter;
                    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 8;

                    excelcells = excelworksheet.get_Range("A" + (4 + days.Length), simbols[times.Count] + (4 + days.Length * 2));
                    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Constants.xlCenter;
                    excelcells.VerticalAlignment = Constants.xlCenter;
                    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 8;

                    excelcells = excelworksheet.get_Range("A2", "A2");
                    excelcells.Value2 = "I неделя";
                    excelcells.ColumnWidth = 9;
                    excelcells.RowHeight = 30;
                    excelcells = excelworksheet.get_Range("A" + (4 + days.Length), "A" + (4 + days.Length));
                    excelcells.Value2 = "II неделя";
                    excelcells.RowHeight = 30;
                    for (int i = 0; i < days.Length; i++)
                    {
                        excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
                        excelcells.RowHeight = 40;
                        excelcells.Value2 = days[i];
                        excelcells = excelworksheet.get_Range("A" + (5 + days.Length + i), "A" + (5 + days.Length + i));
                        excelcells.RowHeight = 40;
                        excelcells.Value2 = days[i];
                    }
                    for (int j = 0; j < times.Count; j++)
                    {
                        excelcells = excelworksheet.get_Range(simbols[j + 1] + 2, simbols[j + 1] + 2);
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = times[j].Text;
                        excelcells = excelworksheet.get_Range(simbols[j + 1] + (4 + days.Length), simbols[j + 1] + (4 + days.Length));
                        excelcells.Value2 = times[j].Text;
                    }
                    #endregion
                    #region тело
                    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[r - 1]).ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        excelcells = excelworksheet.get_Range(simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8),
                            simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8));
                        excelcells.Value2 = list[i].Text;
                    }
                    #endregion
                    #region аудитория
                    excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
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
                excel.Quit();
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService ExportOffsetRecordExcel(List<ScheduleLessonTimeViewModel> times, List<OffsetRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
        {
            try
            {
                var excel = new Application();
                if (File.Exists(model.FileName))
                {
                    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = model.Classrooms.Count;
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
                    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                    var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

                    Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days.Length));
                    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Constants.xlCenter;
                    excelcells.VerticalAlignment = Constants.xlCenter;
                    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 8;

                    excelcells = excelworksheet.get_Range("A" + (4 + days.Length), simbols[times.Count] + (4 + days.Length * 2));
                    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Constants.xlCenter;
                    excelcells.VerticalAlignment = Constants.xlCenter;
                    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 8;

                    excelcells = excelworksheet.get_Range("A2", "A2");
                    excelcells.Value2 = "I неделя";
                    excelcells.ColumnWidth = 9;
                    excelcells.RowHeight = 30;
                    excelcells = excelworksheet.get_Range("A" + (4 + days.Length), "A" + (4 + days.Length));
                    excelcells.Value2 = "II неделя";
                    excelcells.RowHeight = 30;
                    for (int i = 0; i < days.Length; i++)
                    {
                        excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
                        excelcells.RowHeight = 40;
                        excelcells.Value2 = days[i];
                        excelcells = excelworksheet.get_Range("A" + (5 + days.Length + i), "A" + (5 + days.Length + i));
                        excelcells.RowHeight = 40;
                        excelcells.Value2 = days[i];
                    }
                    for (int j = 0; j < times.Count; j++)
                    {
                        excelcells = excelworksheet.get_Range(simbols[j + 1] + 2, simbols[j + 1] + 2);
                        excelcells.ColumnWidth = 15;
                        excelcells.Value2 = times[j].Text;
                        excelcells = excelworksheet.get_Range(simbols[j + 1] + (4 + days.Length), simbols[j + 1] + (4 + days.Length));
                        excelcells.Value2 = times[j].Text;
                    }
                    #endregion
                    #region тело
                    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[r - 1]).ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        excelcells = excelworksheet.get_Range(simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8),
                            simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8));
                        excelcells.Value2 = list[i].Text;
                    }
                    #endregion
                    #region аудитория
                    excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
                    excelcells.Merge(Type.Missing);
                    excelcells.Font.Bold = true;
                    excelcells.Value2 = model.Classrooms[r - 1];
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
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService ExportExaminationRecordExcel(List<ScheduleLessonTimeViewModel> times, List<ExaminationRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
        {
            try
            {
                var excel = new Application();
                
                var currentDates = ScheduleHelper.GetCurrentDates();

                var currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
                var days = (Convert.ToDateTime(currentDates.DateEndExamination) - currentdate).Days;

                if (File.Exists(model.FileName))
                {
                    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing);
                }
                else
                {
                    excel.SheetsInNewWorkbook = model.Classrooms.Count;
                    excel.Workbooks.Add(Type.Missing);
                    excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false,
                        XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                Sheets excelsheets = excel.Workbooks[1].Worksheets;
                for (int r = 1; r <= model.Classrooms.Count; ++r)
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
                    var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

                    Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days));
                    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Constants.xlCenter;
                    excelcells.VerticalAlignment = Constants.xlCenter;
                    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                    excelcells.Font.Name = "Times New Roman";
                    excelcells.Font.Size = 8;

                    for (int i = 0; i < days; i++)
                    {
                        excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
                        excelcells.ColumnWidth = 8;
                        excelcells.RowHeight = 40;
                        excelcells.Formula = "DD.MM.YYYY";
                        excelcells.Value2 = currentdate.ToShortDateString();
                        currentdate = currentdate.AddDays(1);
                    }
                    for (int i = 0; i < times.Count; i++)
                    {
                        excelcells = excelworksheet.get_Range(simbols[i + 1] + (2), simbols[i + 1] + (2));
                        excelcells.ColumnWidth = 20;
                        excelcells.RowHeight = 30;
                        excelcells.Value2 = times[i].Text;
                    }
                    #endregion
                    #region тело
                    currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
                    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[r - 1]).ToList();

                    for (int i = 0; i < list.Count; i++)
                    {
                        if ((list[i].DateConsultation - currentdate).Days > -1 && (list[i].DateConsultation - currentdate).Days <= days)
                        {
                            for (int t = 0; t < times.Count; ++t)
                            {
                                if (list[i].DateConsultation.Hour == times[t].DateBeginLesson.Hour)
                                {
                                    excelcells = excelworksheet.get_Range(simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3),
                                        simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3));
                                    excelcells.Value2 = list[i].Text;
                                    break;
                                }
                            }
                        }
                        if ((list[i].DateExamination - currentdate).Days > -1 && (list[i].DateExamination - currentdate).Days <= days)
                        {
                            for (int t = 0; t < times.Count; ++t)
                            {
                                if (list[i].DateExamination.Hour == times[t].DateBeginLesson.Hour)
                                {
                                    excelcells = excelworksheet.get_Range(simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3),
                                        simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3));
                                    excelcells.Value2 = list[i].Text;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                    #region аудитория
                    excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
                    excelcells.Merge(Type.Missing);
                    excelcells.Font.Bold = true;
                    excelcells.Value2 = model.Classrooms[r - 1];
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
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}
