using AcademicYearInterfaces.ViewModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Enums;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tools;

namespace ScheduleServiceImplementations.Helpers
{
    public class ExportScheduleToExcel
    {
        public static ResultService ExportSemesterRecordExcel(List<SemesterRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
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
                        for (int j = 0; j < model.Times.Count; j++)
                        {
                            InsertCell(rows, 1, j + 1, model.Times[j].Text, CellValues.String, 1);
                            InsertCell(rows, 3 + days.Length, j + 1, model.Times[j].Text, CellValues.String, 1);
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

                        // Add a WorksheetPart to the WorkbookPart.
                        worksheetPart.Worksheet.AppendChild(sheetData);

                        MergeCells mergeCells = new MergeCells();
                        MergeCell mergeCell = new MergeCell() { Reference = new StringValue("A1" + ":" + simbols[model.Times.Count] + "1") };
                        mergeCells.Append(mergeCell);
                        worksheetPart.Worksheet.AppendChild(mergeCells);

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

        public static ResultService ExportOffsetRecordExcel(List<OffsetRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
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
                        SheetData sheetData = new SheetData();
                        List<Row> rows = new List<Row>();
                        DateTime currentdate = Convert.ToDateTime(model.Dates.DateBeginOffset);

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
                        InsertCell(rows, 1, 1, "", CellValues.String, 1);
                        InsertCell(rows, 3 + days.Length, 1, "", CellValues.String, 1);
                        for (int i = 0; i < days.Length; i++)
                        {
                            InsertCell(rows, 2 + i, 1, string.Format("{0}\r\n{1}", days[i], currentdate.ToShortDateString()), CellValues.String, 1);
                            InsertCell(rows, 4 + days.Length + i, 1, string.Format("{0}\r\n{1}", days[i], currentdate.AddDays(7).ToShortDateString()), CellValues.String, 1);
                            currentdate = currentdate.AddDays(1);
                        }
                        for (int j = 0; j < model.Times.Count; j++)
                        {
                            InsertCell(rows, 1, j + 1, model.Times[j].Text, CellValues.String, 1);
                            InsertCell(rows, 3 + days.Length, j + 1, model.Times[j].Text, CellValues.String, 1);
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
                                    var elems = list.Where(x => x.Week == week && x.Day == day && x.Lesson == lesson).OrderBy(x => x.LessonGroup);
                                    if (elems != null && elems.Count() > 0)
                                    {
                                        // одна пара
                                        if (elems.Count() == 1)
                                        {
                                            string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", "зач", elems.First().LessonDiscipline, elems.First().LessonClassroom,
                                                Environment.NewLine, elems.First().LessonLecturer, elems.First().LessonGroup);
                                            InsertCell(rows, day + 2 + week * 8, lesson + 2, text, CellValues.String, 1);
                                        }
                                        else
                                        {
                                            // подгруппы
                                            if (elems.Select(x => x.LessonGroup).Distinct().Count() == 1)
                                            {
                                                string text = string.Format("{0} {1} {2} {3}{2}  {4}", "зач",
                                                    string.Join("/", elems.Select(x => string.Format("{0} {1}", x.LessonDiscipline, x.LessonClassroom))),
                                                    Environment.NewLine, string.Join("/", elems.Select(x => x.LessonLecturer)), elems.First().LessonGroup);
                                                InsertCell(rows, day + 2 + week * 8, lesson + 2, text, CellValues.String, 1);
                                            }
                                            // поток
                                            else
                                            {
                                                string groups = string.Join(",", elems.Select(x => x.LessonGroup));
                                                string text = string.Format("{0} {1} {2}{3}{4}{3}{5}", "зач", elems.First().LessonDiscipline, elems.First().LessonClassroom,
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

                        // Add a WorksheetPart to the WorkbookPart.
                        worksheetPart.Worksheet.AppendChild(sheetData);

                        MergeCells mergeCells = new MergeCells();
                        MergeCell mergeCell = new MergeCell() { Reference = new StringValue("A1" + ":" + simbols[model.Times.Count] + "1") };
                        mergeCells.Append(mergeCell);
                        worksheetPart.Worksheet.AppendChild(mergeCells);

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

        public static ResultService ExportExaminationRecordExcel(List<ExaminationRecordShortViewModel> records, ExportToExcelClassroomsBindingModel model)
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

                    var simbols = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };

                    var currentdate = Convert.ToDateTime(model.Dates.DateBeginExamination);
                    var days = (Convert.ToDateTime(model.Dates.DateEndExamination) - currentdate).Days + 1;

                    for (int r = 1; r <= model.Classrooms.Count; r++)
                    {
                        SheetData sheetData = new SheetData();
                        List<Row> rows = new List<Row>();

                        #region шапка
                        for (uint i = 0; i < days + 2; ++i)
                        {
                            Row row = new Row()
                            {
                                RowIndex = i + 1,
                                Height = (i == 0 /*шапка*/ ? 25 : i == 1 /*шапки таблиц*/ ? 30 : 40),
                                CustomHeight = true
                            };
                            rows.Add(row);
                            sheetData.Append(row);
                        }

                        InsertCell(rows, 0, 1, model.Classrooms[r - 1].Split(new char[] { '\\', '/' })[0], CellValues.String, 2);
                        InsertCell(rows, 1, 1, "", CellValues.String, 1);
                        for (int i = 0; i < days; i++)
                        {
                            InsertCell(rows, 2 + i, 1, string.Format("{0}{1}{2}", currentdate.ToShortDateString(), Environment.NewLine,
                               CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(currentdate.DayOfWeek)), CellValues.String, 1);
                            currentdate = currentdate.AddDays(1);
                        }
                        for (int j = 0; j < model.Times.Count; j++)
                        {
                            InsertCell(rows, 1, j + 1, model.Times[j].Text, CellValues.String, 1);
                        }
                        #endregion

                        #region тело
                        var list = records.Where(rec => rec.LessonClassroom == model.Classrooms[r - 1]).ToList();
                        currentdate = Convert.ToDateTime(model.Dates.DateBeginExamination);
                        for (int i = 0; i < days; ++i)
                        {
                            InsertCell(rows, i + 2, 2, "", CellValues.String, 1);
                            InsertCell(rows, i + 2, 3, "", CellValues.String, 1);
                            InsertCell(rows, i + 2, 4, "", CellValues.String, 1);
                            InsertCell(rows, i + 2, 5, "", CellValues.String, 1);
                            var elems = list.Where(x => x.DateExamination == currentdate.AddDays(i)).OrderBy(x => x.LessonGroup).GroupBy(x => x.DateExamination);
                            if (elems != null && elems.Count() > 0)
                            {
                                foreach(var elem in elems)
                                {

                                    string text = string.Format("{0} {1}{2}{3}{2}{4}",
                                        elem.Key.ToShortTimeString(),
                                        string.Join(" ", elem.Select(x => x.LessonDiscipline).Distinct()),
                                        Environment.NewLine,
                                        string.Join(" ", elem.Select(x => x.LessonLecturer).Distinct()),
                                        string.Join(" ", elem.Select(x => x.LessonGroup).Distinct()));

                                    if(elem.Key.Hour < 10)
                                    {
                                        InsertCell(rows, i + 2 , 2, text, CellValues.String, 1);
                                    }
                                    else
                                    {
                                        InsertCell(rows, i + 2, 3, text, CellValues.String, 1);
                                    }
                                }
                            }
                            elems = list.Where(x => x.DateConsultation == currentdate.AddDays(i)).OrderBy(x => x.LessonGroup).GroupBy(x => x.DateExamination);
                            if (elems != null && elems.Count() > 0)
                            {
                                foreach (var elem in elems)
                                {

                                    string text = string.Format("{0} {1}{2}{3}{2}{4}",
                                        elem.Key.ToShortTimeString(),
                                        string.Join(" ", elem.Select(x => x.LessonDiscipline).Distinct()),
                                        Environment.NewLine,
                                        string.Join(" ", elem.Select(x => x.LessonLecturer).Distinct()),
                                        string.Join(" ", elem.Select(x => x.LessonGroup).Distinct()));

                                    if (elem.Key.Hour < 17)
                                    {
                                        InsertCell(rows, i + 2, 4, text, CellValues.String, 1);
                                    }
                                    else
                                    {
                                        InsertCell(rows, i + 2, 5, text, CellValues.String, 1);
                                    }
                                }
                            }
                        }
                        #endregion

                        WorksheetPart worksheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new Worksheet();

                        SheetProperties sheetProperties = new SheetProperties(new PageSetupProperties());
                        worksheetPart.Worksheet.SheetProperties = sheetProperties;
                        worksheetPart.Worksheet.SheetProperties.PageSetupProperties.FitToPage = BooleanValue.FromBoolean(true);
                        // Задаем колонки и их ширину
                        Columns listColumns = new Columns();
                        listColumns.Append(new Column() { Min = 1, Max = 1, Width = 11.90, CustomWidth = true });
                        listColumns.Append(new Column() { Min = 2, Max = 5, Width = 40.90, CustomWidth = true });
                        worksheetPart.Worksheet.Append(listColumns);

                        // Add a WorksheetPart to the WorkbookPart.
                        worksheetPart.Worksheet.AppendChild(sheetData);

                        MergeCells mergeCells = new MergeCells();
                        MergeCell mergeCell = new MergeCell() { Reference = new StringValue("A1" + ":" + simbols[model.Times.Count] + "1") };
                        mergeCells.Append(mergeCell);
                        worksheetPart.Worksheet.AppendChild(mergeCells);

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
            if (!string.IsNullOrEmpty(val))
            {
                newCell.CellValue = new CellValue(val);
                newCell.DataType = new EnumValue<CellValues>(type);
            }

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