using AcademicYearInterfaces.BindingModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace AcademicYearImplementations.Helper
{
    public static class ExcelHelper
    {
        public static ResultService ImportLecturerWorkloads(ImportLecturerWorkloadBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    DepartmentUserManager.CheckAccess(AccessOperation.Учебные_планы, AccessType.View, "Учебные планы");

                    // получаем список видов нагрузки
                    var timeNorms = context.TimeNorms.Where(x => !x.IsDeleted && x.AcademicYearId == model.AcademicYearId).OrderBy(x => x.TimeNormOrder).ToList();

                    var lecturers = context.Lecturers.Where(x => !x.IsDeleted);

                    foreach (var lecturer in lecturers)
                    {
                        using (SpreadsheetDocument document = SpreadsheetDocument.Create(string.Format("{0}/{1}.xlsx", model.Path, lecturer.ToString()), SpreadsheetDocumentType.Workbook))
                        {
                            // Add a WorkbookPart to the document.
                            WorkbookPart workbookPart = document.AddWorkbookPart();
                            workbookPart.Workbook = new Workbook();
                            // Add a WorksheetPart to the WorkbookPart.
                            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                            worksheetPart.Worksheet = new Worksheet();

                            SheetProperties sheetProperties = new SheetProperties(new PageSetupProperties());
                            worksheetPart.Worksheet.SheetProperties = sheetProperties;
                            worksheetPart.Worksheet.SheetProperties.PageSetupProperties.FitToPage = BooleanValue.FromBoolean(true);

                            WorkbookStylesPart workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                            // Добавляем в документ набор стилей
                            workbookStylesPart.Stylesheet = GenerateStyleSheet();
                            workbookStylesPart.Stylesheet.Save();

                            // Задаем колонки и их ширину
                            Columns lstColumns = new Columns();
                            lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 7.5, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 4.5, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 9.5, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = 4, Max = 4, Width = 40, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = 5, Max = 9, Width = 4.5, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = 10, Max = (uint)(10 + timeNorms.Count), Width = 5.5, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = (uint)(10 + timeNorms.Count), Max = (uint)(10 + timeNorms.Count), Width = 10, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = (uint)(11 + timeNorms.Count), Max = (uint)(11 + timeNorms.Count), Width = 10, CustomWidth = true });
                            lstColumns.Append(new Column() { Min = (uint)(12 + timeNorms.Count), Max = (uint)(12 + timeNorms.Count), Width = 30, CustomWidth = true });

                            worksheetPart.Worksheet.Append(lstColumns);
                            worksheetPart.Worksheet.AppendChild(new SheetData());

                            // Add Sheets to the Workbook.
                            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = lecturer.ToString() };
                            sheets.Append(sheet);

                            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                            #region Head
                            uint rowIndex = 1;
                            Row row = new Row() { RowIndex = rowIndex++, Height = 100, CustomHeight = true };
                            sheetData.Append(row);
                            int counter = 1;
                            InsertCell(row, counter++, "Семестр (осень / весна)", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, "Дисциплина по выбору", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, "Код направления (специальности) по ФГОС 3 +", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, "Полное наименование дисциплин", CellValues.String, ExcelStyle.TNR10Borders);
                            InsertCell(row, counter++, "Курс", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, "Студентов", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, "Потоков", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, "Групп", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, "Подгрупп", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            foreach (var timeNorm in timeNorms)
                            {
                                InsertCell(row, counter++, timeNorm.TimeNormName, CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            }
                            InsertCell(row, counter++, "Итого", CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            InsertCell(row, counter++, lecturer.ToString(), CellValues.String, ExcelStyle.VerticalTNR10Borders);
                            #endregion

                            var aprms = context.AcademicPlanRecordMissions.Where(x => x.LecturerId == lecturer.Id && !x.IsDeleted &&
                                            x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId)
                                            .Include(x => x.AcademicPlanRecordElement)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Discipline)
                                            .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.Contingent)
                                            .ToList()
                                            .OrderByDescending(x => (int)x.AcademicPlanRecordElement.AcademicPlanRecord.Semester % 2)
                                            .ThenBy(x => (int)x.AcademicPlanRecordElement.AcademicPlanRecord.Semester)
                                            .GroupBy(x => new { x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineId, x.AcademicPlanRecordElement.AcademicPlanRecord.Semester });
                            double? workload = (context.LecturerWorkload.FirstOrDefault(x => x.LecturerId == lecturer.Id && x.AcademicYearId == model.AcademicYearId &&
                                                            !x.IsDeleted))?.Workload;
                            int? post = context.LecturerPosts.FirstOrDefault(x => x.Id == lecturer.LecturerPostId)?.Hours;
                            double? hours = workload * post;
                            double? diff = hours - (double)aprms.Sum(x => x.Sum(y => y.Hours));

                            #region 
                            row = new Row() { RowIndex = rowIndex++ };
                            sheetData.Append(row);
                            for (int i = 0; i < counter - 2; ++i)
                            {
                                InsertCell(row, i + 1, "", CellValues.String, ExcelStyle.TNR10Borders);
                            }
                            InsertCell(row, counter - 2, workload?.ToString() ?? "", CellValues.String, ExcelStyle.TNR10Borders);
                            InsertCell(row, counter - 1, "Ставка по договору", CellValues.String, ExcelStyle.TNR10);
                            #endregion

                            #region 
                            row = new Row() { RowIndex = rowIndex++ };
                            sheetData.Append(row);
                            for (int i = 0; i < counter - 2; ++i)
                            {
                                InsertCell(row, i + 1, "", CellValues.String, ExcelStyle.TNR10Borders);
                            }
                            InsertCell(row, counter - 2, hours?.ToString() ?? "", CellValues.String, ExcelStyle.TNR10Borders);
                            InsertCell(row, counter - 1, "Кол-во часов по ставке", CellValues.String, ExcelStyle.TNR10);
                            #endregion

                            #region 
                            row = new Row() { RowIndex = rowIndex++ };
                            sheetData.Append(row);
                            for (int i = 0; i < counter - 2; ++i)
                            {
                                InsertCell(row, i + 1, "", CellValues.String, ExcelStyle.TNR10Borders);
                            }
                            InsertCell(row, counter - 2, diff?.ToString() ?? "", CellValues.String, (diff > 0 ? ExcelStyle.TNR10Red : diff < 0 ? ExcelStyle.TNR10Green : ExcelStyle.TNR10Borders));
                            InsertCell(row, counter - 1, "недогруз/перегруз", CellValues.String, ExcelStyle.TNR10);
                            #endregion

                            #region 
                            row = new Row() { RowIndex = rowIndex++ };
                            sheetData.Append(row);
                            for (int i = 0; i < counter - 3; ++i)
                            {
                                InsertCell(row, i + 1, "", CellValues.String, ExcelStyle.TNR10Borders);
                            }
                            InsertCell(row, counter - 3, (context.AcademicPlanRecordMissions.Where(x => x.LecturerId == lecturer.Id && !x.IsDeleted &&
                                            x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId)
                                            .Include(x => x.AcademicPlanRecordElement)
                                            .Select(x => x.AcademicPlanRecordElement)
                                            .Distinct()
                                            .Sum(x => x.FactHours)).ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                            InsertCell(row, counter - 2, aprms.Sum(x => x.Sum(y => y.Hours)).ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                            InsertCell(row, counter - 1, "Фактические часы", CellValues.String, ExcelStyle.TNR10);
                            #endregion

                            MergeCells mergeCells = new MergeCells();
                            string[] symbols = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                            for (int i = 0; i < counter - 2; ++i)
                            {
                                MergeCell mergeCell;
                                if (i < symbols.Length)
                                {
                                    mergeCell = new MergeCell() { Reference = new StringValue(symbols[i] + "1" + ":" + symbols[i] + "4") };
                                }
                                else
                                {
                                    mergeCell = new MergeCell()
                                    {
                                        Reference = new StringValue(symbols[i / symbols.Length - 1] + symbols[i % symbols.Length] + "1" + ":" +
                                        symbols[i / symbols.Length - 1] + symbols[i % symbols.Length] + "4")
                                    };
                                }
                                mergeCells.Append(mergeCell);
                            }
                            worksheetPart.Worksheet.AppendChild(mergeCells);

                            foreach (var aprm in aprms)
                            {
                                row = new Row() { RowIndex = rowIndex++ };
                                counter = 1;
                                sheetData.Append(row);
                                var mission = aprm.FirstOrDefault();
                                if (mission == null)
                                {
                                    continue;
                                }
                                #region строка
                                InsertCell(row, counter++, (int)mission.AcademicPlanRecordElement.AcademicPlanRecord.Semester % 2 == 0 ? "весна" : "осень", CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, mission.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineParentId.HasValue ? "да" : "", CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, mission.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.EducationDirection.Cipher, CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, mission.AcademicPlanRecordElement.AcademicPlanRecord.Discipline.DisciplineName, CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, (Math.Log((double)mission.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.Course, 2) + 1).ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, mission.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.CountStudetns.ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, "1", CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, mission.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.CountGroups.ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, mission.AcademicPlanRecordElement.AcademicPlanRecord.Contingent.CountSubgroups.ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                                foreach (var timeNorm in timeNorms)
                                {
                                    var rec = aprm.Where(x => x.AcademicPlanRecordElement.TimeNormId == timeNorm.Id);
                                    if (rec != null && rec.Count() > 0)
                                    {
                                        InsertCell(row, counter++, rec.Sum(x => x.Hours).ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                                    }
                                    else
                                    {
                                        InsertCell(row, counter++, "", CellValues.String, ExcelStyle.TNR10Borders);
                                    }
                                }
                                InsertCell(row, counter++, (context.AcademicPlanRecordElements.Where(x => x.AcademicPlanRecordId == mission.AcademicPlanRecordElement.AcademicPlanRecordId &&
                                                                         !x.IsDeleted && x.AcademicPlanRecord.Semester == aprm.Key.Semester).Sum(x => x.FactHours)).ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                                InsertCell(row, counter++, aprm.Sum(x => x.Hours).ToString(), CellValues.String, ExcelStyle.TNR10Borders);
                                #endregion
                            }

                            PageSetup pageSetup = new PageSetup
                            {
                                Orientation = OrientationValues.Landscape
                            };
                            worksheetPart.Worksheet.AppendChild(pageSetup);

                            workbookPart.Workbook.Save();
                            document.Close();
                        }
                    }

                    return ResultService.Success();
                }
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
        private static void InsertCell(Row row, int cell_num, string val, CellValues type, ExcelStyle styleIndex)
        {
            Cell refCell = null;
            Cell newCell = new Cell() { CellReference = cell_num.ToString() + ":" + row.RowIndex.ToString(), StyleIndex = (uint)styleIndex };
            row.InsertBefore(newCell, refCell);
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
                    new Font(                                                               // Стиль под номером 0 - Шрифт по умолчанию.
                        new FontSize() { Val = 10 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 1 - Жирный шрифт Times New Roman.
                        new Bold(),
                        new FontSize() { Val = 10 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 0 - Шрифт по умолчанию.
                        new FontSize() { Val = 10 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "db1313" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 0 - Шрифт по умолчанию.
                        new FontSize() { Val = 10 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "19e857" } },
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
                    new Border(                                                         // Стиль под номером 1 - Границы.
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
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyFont = true, ApplyBorder = true },

                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyFont = true, ApplyBorder = true },                          // Стиль под номером 0 - The default cell style.  (по умолчанию)

                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true, TextRotation = 90U })
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyFont = true, ApplyBorder = true },       // Стиль под номером 1 - текст вертикально

                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true, ApplyFont = true },      // 

                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 2, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyFont = true, ApplyBorder = true },      // 

                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true })
                    { FontId = 3, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyFont = true, ApplyBorder = true }       // 

                )
            ); // Выход
        }
    }
}