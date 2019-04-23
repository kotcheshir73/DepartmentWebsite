using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Enums;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace ScheduleServiceImplementations.Helpers
{
    class ExportScheduleToExcel
    {
        public static ResultService ExportSemesterRecordExcel(List<ScheduleLessonTimeViewModel> times, List<SemesterRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(model.FileName, SpreadsheetDocumentType.Workbook))
                {
                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    // Добавляем в документ набор стилей
                    WorkbookStylesPart workbookStylesPart = document.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    workbookStylesPart.Stylesheet = GenerateStyleSheet();
                    workbookStylesPart.Stylesheet.Save();

                    var sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());

                    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                    var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

                    for (int r = 1; r <= model.Classrooms.Count; r++)
                    {
                        WorksheetPart worksheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new Worksheet();

                        SheetProperties sheetProperties = new SheetProperties(new PageSetupProperties());
                        worksheetPart.Worksheet.SheetProperties = sheetProperties;
                        worksheetPart.Worksheet.SheetProperties.PageSetupProperties.FitToPage = BooleanValue.FromBoolean(true);
                        // Задаем колонки и их ширину
                        Columns listColumns = new Columns();
                        listColumns.Append(new Column() { Min = 1, Max = 1, Width = 9.90, CustomWidth = true });
                        listColumns.Append(new Column() { Min = 2, Max = 9, Width = 15.90, CustomWidth = true });
                        worksheetPart.Worksheet.Append(listColumns);

                        SheetData sheetData = new SheetData();
                        List<Row> rows = new List<Row>();

                        #region шапка
                        for (uint i = 0; i < days.Length * 2 + 4; ++i)
                        {
                            Row row = new Row()
                            {
                                RowIndex = i + 1,
                                Height = (i == 0 /*шапка*/ ? 25 : i == days.Length + 2 /*между таблицами*/ ? 15 : i == 1 || i == days.Length + 3 /*шапки таблиц*/ ? 30 : 40),
                                CustomHeight = true
                            };
                            rows.Add(row);
                            sheetData.Append(row);
                        }

                        InsertCell(rows, 0, 1, model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0], CellValues.String, 2);
                        InsertCell(rows, 1, 1, "I неделя", CellValues.String, 1);
                        InsertCell(rows, 3 + days.Length, 1, "II неделя", CellValues.String, 1);
                        for (int i = 0; i < days.Length; i++)
                        {
                            InsertCell(rows, 2 + i, 1, days[i], CellValues.String, 1);
                            InsertCell(rows, 4 + days.Length + i, 1, days[i], CellValues.String, 1);
                        }
                        for (int j = 0; j < times.Count; j++)
                        {
                            InsertCell(rows, 1, j + 1, times[j].Text, CellValues.String, 1);
                            InsertCell(rows, 3 + days.Length, j + 1, times[j].Text, CellValues.String, 1);
                        }
                        #endregion

                        #region тело
                        var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[r - 1]).ToList();
                        for (int week = 0; week < 2; week++)
                        {
                            for (int day = 0; day < 6; day++)
                            {
                                for (int lesson = 0; lesson < 8; lesson++)
                                {
                                    var elems = list.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson && x.LessonType != LessonTypes.удл.ToString()).OrderBy(x => x.LessonGroup);
                                    if (elems != null && elems.Count() > 0)
                                    {
                                        // одна пара
                                        if (elems.Count() == 1)
                                        {
                                            string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", elems.First().LessonType, elems.First().LessonDiscipline, elems.First().LessonClassroom,
                                                Environment.NewLine, elems.First().LessonLecturer, elems.First().LessonGroup);
                                            InsertCell(rows, day + 2 + week * 8, lesson + 2, text, CellValues.String, 1);
                                        }
                                        else
                                        {
                                            // подгруппы
                                            if (elems.Select(x => x.LessonGroup).Distinct().Count() == 1)
                                            {
                                                string text = string.Format("{0} {1} {2} {3}{2}  {4}", elems.First().LessonType,
                                                    string.Join("/", elems.Select(x => string.Format("{0} {1}", x.LessonDiscipline, x.LessonClassroom))),
                                                    Environment.NewLine, string.Join("/", elems.Select(x => x.LessonLecturer)), elems.First().LessonGroup);
                                                InsertCell(rows, day + 2 + week * 8, lesson + 2, text, CellValues.String, 1);
                                            }
                                            // поток
                                            else
                                            {
                                                string groups = string.Join(",", elems.Select(x => x.LessonGroup));
                                                string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", elems.First().LessonType, elems.First().LessonDiscipline, elems.First().LessonClassroom,
                                                    Environment.NewLine, elems.First().LessonLecturer, groups);
                                                InsertCell(rows, day + 2 + week * 8, lesson + 2, text, CellValues.String, 1);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        InsertCell(rows, day + 2 + week * 8, lesson + 2, "", CellValues.String, 1);
                                    }
                                }
                            }
                        }
                        #endregion

                        // Add a WorksheetPart to the WorkbookPart.
                        worksheetPart.Worksheet.AppendChild(sheetData);

                        #region аудитория
                        MergeCells mergeCells = new MergeCells();
                        MergeCell mergeCell = new MergeCell() { Reference = new StringValue("A1" + ":" + simbols[times.Count] + "1") };
                        mergeCells.Append(mergeCell);
                        worksheetPart.Worksheet.AppendChild(mergeCells);
                        #endregion

                        // Add Sheets to the Workbook.
                        Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = (uint)r, Name = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0] };
                        sheets.Append(sheet);

                        PageSetup pageSetup = new PageSetup
                        {
                            Orientation = OrientationValues.Landscape
                        };
                        worksheetPart.Worksheet.AppendChild(pageSetup);
                    }

                    workbookPart.Workbook.Save();
                    document.Close();
                }
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService ExportOffsetRecordExcel(List<ScheduleLessonTimeViewModel> times, List<OffsetRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
        {
            try
            {
                //var excel = new Application();
                //if (File.Exists(model.FileName))
                //{
                //    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //        Type.Missing, Type.Missing);
                //}
                //else
                //{
                //    excel.SheetsInNewWorkbook = model.Classrooms.Count;
                //    excel.Workbooks.Add(Type.Missing);
                //    excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false,
                //        XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //}
                //Sheets excelsheets = excel.Workbooks[1].Worksheets;
                //for (int r = 1; r <= model.Classrooms.Count; r++)
                //{
                //    var excelworksheet = (Worksheet)excelsheets.get_Item(r);//Получаем ссылку на лист
                //    excelworksheet.Cells.Clear();
                //    excelworksheet.Name = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
                //    excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                //    excelworksheet.PageSetup.RightMargin = 0;
                //    excelworksheet.PageSetup.LeftMargin = 0;
                //    excelworksheet.PageSetup.TopMargin = 0;
                //    excelworksheet.PageSetup.BottomMargin = 0;
                //    excelworksheet.PageSetup.CenterHorizontally = true;
                //    excelworksheet.PageSetup.CenterVertically = true;
                //    #region шапка
                //    var days = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ" };
                //    var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

                //    Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days.Length));
                //    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                //    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                //    excelcells.HorizontalAlignment = Constants.xlCenter;
                //    excelcells.VerticalAlignment = Constants.xlCenter;
                //    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                //                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                //    excelcells.Font.Name = "Times New Roman";
                //    excelcells.Font.Size = 8;

                //    excelcells = excelworksheet.get_Range("A" + (4 + days.Length), simbols[times.Count] + (4 + days.Length * 2));
                //    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                //    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                //    excelcells.HorizontalAlignment = Constants.xlCenter;
                //    excelcells.VerticalAlignment = Constants.xlCenter;
                //    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                //                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                //    excelcells.Font.Name = "Times New Roman";
                //    excelcells.Font.Size = 8;

                //    excelcells = excelworksheet.get_Range("A2", "A2");
                //    excelcells.Value2 = "I неделя";
                //    excelcells.ColumnWidth = 9;
                //    excelcells.RowHeight = 30;
                //    excelcells = excelworksheet.get_Range("A" + (4 + days.Length), "A" + (4 + days.Length));
                //    excelcells.Value2 = "II неделя";
                //    excelcells.RowHeight = 30;
                //    for (int i = 0; i < days.Length; i++)
                //    {
                //        excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
                //        excelcells.RowHeight = 40;
                //        excelcells.Value2 = days[i];
                //        excelcells = excelworksheet.get_Range("A" + (5 + days.Length + i), "A" + (5 + days.Length + i));
                //        excelcells.RowHeight = 40;
                //        excelcells.Value2 = days[i];
                //    }
                //    for (int j = 0; j < times.Count; j++)
                //    {
                //        excelcells = excelworksheet.get_Range(simbols[j + 1] + 2, simbols[j + 1] + 2);
                //        excelcells.ColumnWidth = 15;
                //        excelcells.Value2 = times[j].Text;
                //        excelcells = excelworksheet.get_Range(simbols[j + 1] + (4 + days.Length), simbols[j + 1] + (4 + days.Length));
                //        excelcells.Value2 = times[j].Text;
                //    }
                //    #endregion
                //    #region тело
                //    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[r - 1]).ToList();
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        excelcells = excelworksheet.get_Range(simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8),
                //            simbols[list[i].Lesson + 1] + (list[i].Day + 3 + list[i].Week * 8));
                //        excelcells.Value2 = list[i].Text;
                //    }
                //    #endregion
                //    #region аудитория
                //    excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
                //    excelcells.Merge(Type.Missing);
                //    excelcells.Font.Bold = true;
                //    excelcells.Value2 = model.Classrooms[r - 1];
                //    excelcells.RowHeight = 25;
                //    excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //    excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                //    excelcells.Font.Name = "Times New Roman";
                //    excelcells.Font.Size = 14;
                //    #endregion
                //}

                //excel.Workbooks[1].Save();
                //excel.Quit();
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
                //var excel = new Application();

                //var currentDates = ScheduleHelper.GetCurrentDates();

                //var currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
                //var days = (Convert.ToDateTime(currentDates.DateEndExamination) - currentdate).Days;

                //if (File.Exists(model.FileName))
                //{
                //    excel.Workbooks.Open(model.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                //        Type.Missing);
                //}
                //else
                //{
                //    excel.SheetsInNewWorkbook = model.Classrooms.Count;
                //    excel.Workbooks.Add(Type.Missing);
                //    excel.Workbooks[1].SaveAs(model.FileName, XlFileFormat.xlExcel8, Type.Missing, Type.Missing, false, false,
                //        XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //}
                //Sheets excelsheets = excel.Workbooks[1].Worksheets;
                //for (int r = 1; r <= model.Classrooms.Count; ++r)
                //{
                //    var excelworksheet = (Worksheet)excelsheets.get_Item(r);//Получаем ссылку на лист
                //    excelworksheet.Cells.Clear();
                //    excelworksheet.Name = model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0];
                //    excelworksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                //    excelworksheet.PageSetup.RightMargin = 0;
                //    excelworksheet.PageSetup.LeftMargin = 0;
                //    excelworksheet.PageSetup.TopMargin = 0;
                //    excelworksheet.PageSetup.BottomMargin = 0;
                //    excelworksheet.PageSetup.CenterHorizontally = true;
                //    excelworksheet.PageSetup.CenterVertically = true;
                //    #region шапка
                //    var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

                //    Range excelcells = excelworksheet.get_Range("A2", simbols[times.Count] + (2 + days));
                //    excelcells.Borders.LineStyle = XlLineStyle.xlContinuous;
                //    excelcells.Borders.Weight = XlBorderWeight.xlThin;
                //    excelcells.HorizontalAlignment = Constants.xlCenter;
                //    excelcells.VerticalAlignment = Constants.xlCenter;
                //    excelcells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium,
                //                            XlColorIndex.xlColorIndexAutomatic, 1);//обводим границы дня
                //    excelcells.Font.Name = "Times New Roman";
                //    excelcells.Font.Size = 8;

                //    for (int i = 0; i < days; i++)
                //    {
                //        excelcells = excelworksheet.get_Range("A" + (3 + i), "A" + (3 + i));
                //        excelcells.ColumnWidth = 8;
                //        excelcells.RowHeight = 40;
                //        excelcells.Formula = "DD.MM.YYYY";
                //        excelcells.Value2 = currentdate.ToShortDateString();
                //        currentdate = currentdate.AddDays(1);
                //    }
                //    for (int i = 0; i < times.Count; i++)
                //    {
                //        excelcells = excelworksheet.get_Range(simbols[i + 1] + (2), simbols[i + 1] + (2));
                //        excelcells.ColumnWidth = 20;
                //        excelcells.RowHeight = 30;
                //        excelcells.Value2 = times[i].Text;
                //    }
                //    #endregion
                //    #region тело
                //    currentdate = Convert.ToDateTime(currentDates.DateBeginExamination);
                //    var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[r - 1]).ToList();

                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        if ((list[i].DateConsultation - currentdate).Days > -1 && (list[i].DateConsultation - currentdate).Days <= days)
                //        {
                //            for (int t = 0; t < times.Count; ++t)
                //            {
                //                if (list[i].DateConsultation.Hour == times[t].DateBeginLesson.Hour)
                //                {
                //                    excelcells = excelworksheet.get_Range(simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3),
                //                        simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3));
                //                    excelcells.Value2 = list[i].Text;
                //                    break;
                //                }
                //            }
                //        }
                //        if ((list[i].DateExamination - currentdate).Days > -1 && (list[i].DateExamination - currentdate).Days <= days)
                //        {
                //            for (int t = 0; t < times.Count; ++t)
                //            {
                //                if (list[i].DateExamination.Hour == times[t].DateBeginLesson.Hour)
                //                {
                //                    excelcells = excelworksheet.get_Range(simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3),
                //                        simbols[t + 1] + ((list[i].DateConsultation - currentdate).Days + 3));
                //                    excelcells.Value2 = list[i].Text;
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //    #endregion
                //    #region аудитория
                //    excelcells = excelworksheet.get_Range("A1", simbols[times.Count] + "1");
                //    excelcells.Merge(Type.Missing);
                //    excelcells.Font.Bold = true;
                //    excelcells.Value2 = model.Classrooms[r - 1];
                //    excelcells.RowHeight = 25;
                //    excelcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //    excelcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                //    excelcells.Font.Name = "Times New Roman";
                //    excelcells.Font.Size = 14;
                //    #endregion
                //}

                //excel.Workbooks[1].Save();
                //excel.Quit();
                return ResultService.Success();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        /// <summary>
        /// Добавление Ячейки в строку (На вход подаем: строку, номер колонки, тип значения, стиль)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cell_num"></param>
        /// <param name="val"></param>
        /// <param name="type"></param>
        /// <param name="styleIndex"></param>
        private static void InsertCell(List<Row> rows, int row_number, int cell_num, string val, CellValues type, uint styleIndex)
        {
            Cell refCell = null;
            Cell newCell = new Cell() { CellReference = string.Format("{0}:{1}", cell_num, row_number + 2), StyleIndex = styleIndex };
            rows[row_number].InsertBefore(newCell, refCell);
            // Устанавливает тип значения.
            newCell.CellValue = new CellValue(val);
            newCell.DataType = new EnumValue<CellValues>(type);

        }

        /// <summary>
        /// Метод генерирует стили для ячеек
        /// </summary>
        /// <returns></returns>
        private static Stylesheet GenerateStyleSheet()
        {
            return new Stylesheet(
                new Fonts(
                    new Font(                                                               // Стиль под номером 0 - Шрифт для расписания.
                        new FontSize() { Val = 8 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 1 - Шрифт для заголовка.
                        new Bold(),
                        new FontSize() { Val = 14 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" })
                ),
                new Fills(
                    new Fill(                                                           // Стиль под номером 0 - Заполнение ячейки по умолчанию.
                        new PatternFill() { PatternType = PatternValues.None })
                )
                ,
                new Borders(
                    new Border(                                                         // Стиль под номером 0 - Без границ.
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),
                    new Border(                                                         // Стиль под номером 2 - Границы.
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new RightBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new BottomBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                ),
                new CellFormats(
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true, ApplyFont = true, ApplyBorder = true },

                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyFont = true, ApplyBorder = true },

                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 1, FillId = 0, BorderId = 0, ApplyAlignment = true, ApplyFont = true }
                )
            ); // Выход
        }
    }
}