using AcademicYearInterfaces.BindingModels;
using DatabaseContext;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Enums;
using Microsoft.EntityFrameworkCore;
using Models.AcademicYearData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace AcademicYearImplementations.Helper
{
    public static class WordHelper
    {
        private static Dictionary<string, RunProperties> Properties
        {
            get
            {
                Dictionary<string, RunProperties> runProperties = new Dictionary<string, RunProperties>()
                    {
                        { "MainBold12", new RunProperties(new RunFonts { Ascii = "Times New Roman", HighAnsi = "Times New Roman" }) },
                        { "Main12", new RunProperties(new RunFonts { Ascii = "Times New Roman", HighAnsi = "Times New Roman" }) }
                    };
                runProperties["MainBold12"].Append(new Bold());
                runProperties["MainBold12"].Append(new FontSize { Val = new StringValue("24") });
                runProperties["Main12"].Append(new FontSize { Val = new StringValue("24") });

                return runProperties;
            }
        }

        public static ResultService ImportDisciplineTimeDistributionsLecturers(ImportDisciplineTimeDistributionsBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Расчасовки, AccessType.View, "Расчасовки");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var lecturers = context.Lecturers.Where(x => !x.IsDeleted);

                    foreach (var lecturer in lecturers)
                    {
                        // выбираем нагрузку преподавателя в этом учебном году в этом семестре
                        var query = context.AcademicPlanRecordMissions.Where(x => !x.IsDeleted && x.LecturerId == lecturer.Id &&
                                                                x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId)
                                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.AcademicPlan)
                                    .Include(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions);

                        var grahp = query.SelectMany(x => x.AcademicPlanRecordElement.AcademicPlanRecord.DisciplineTimeDistributions).Distinct()
                                        .Include(x => x.AcademicPlanRecord.Discipline).Include(x => x.AcademicPlanRecord.AcademicPlan.AcademicYear)
                                        .Include(x => x.AcademicPlanRecord.Contingent)
                                        .Include(x => x.StudentGroup).Include(x => x.StudentGroup.EducationDirection).Include(x => x.StudentGroup.Students)
                                        .OrderBy(x => x.AcademicPlanRecord.Discipline.DisciplineName).ThenBy(x => x.StudentGroup.GroupName);

                        CreateDisciplineTimeDistributionDocument(string.Format("{0}/{1}.docx", model.Path, lecturer.ToString()), grahp.ToList());
                    }

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public static ResultService ImportDisciplineTimeDistributionsDisciplines(ImportDisciplineTimeDistributionsBindingModel model)
        {
            try
            {
                DepartmentUserManager.CheckAccess(AccessOperation.Расчасовки, AccessType.View, "Расчасовки");

                using (var context = DepartmentUserManager.GetContext)
                {
                    var disciplines = context.Disciplines.Where(x => !x.IsDeleted);

                    foreach (var discipline in disciplines)
                    {
                        // выбираем нагрузку по дисциплине в этом учебном году в этом семестре
                        var grahp = context.DisciplineTimeDistributions.Where(x => !x.IsDeleted && x.AcademicPlanRecord.DisciplineId == discipline.Id && 
                                                                x.AcademicPlanRecord.AcademicPlan.AcademicYearId == model.AcademicYearId)
                                        .Include(x => x.AcademicPlanRecord.Discipline).Include(x => x.AcademicPlanRecord.AcademicPlan.AcademicYear)
                                        .Include(x => x.AcademicPlanRecord.Contingent)
                                        .Include(x => x.StudentGroup).Include(x => x.StudentGroup.EducationDirection).Include(x => x.StudentGroup.Students)
                                        .OrderBy(x => x.AcademicPlanRecord.Discipline.DisciplineName).ThenBy(x => x.StudentGroup.GroupName);

                        CreateDisciplineTimeDistributionDocument(string.Format("{0}/{1}.docx", model.Path, discipline.ToString()), grahp.ToList());
                    }

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private static void CreateDisciplineTimeDistributionDocument(string fileName, List<DisciplineTimeDistribution> grahp)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                using (WordprocessingDocument myDocument = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = myDocument.AddMainDocumentPart();

                    mainPart.Document = new Document();
                    Body body = new Body();

                    // Настройки страницы
                    SectionProperties sectProp = new SectionProperties();
                    sectProp.Append(new PageSize() { Width = 16838U, Height = 11906U, Orient = PageOrientationValues.Landscape });
                    sectProp.Append(new PageMargin() { Top = 285, Right = 565U, Bottom = 285, Left = 565U });
                    body.Append(sectProp);

                    // идем по записям расчасовок
                    foreach (var elem in grahp)
                    {
                        // получаем гормы времени записи учебного плана 
                        // нас интересует наличие лекций, лабораторных, практик для вывода часов
                        // наличие курсовых, зачетов экзаменов для вывода отчености
                        var timeNorms = context.AcademicPlanRecordElements.Where(x => x.AcademicPlanRecordId == elem.AcademicPlanRecordId && !x.IsDeleted)
                                                        .Include(x => x.TimeNorm)
                                                        .Select(x => x.TimeNorm)
                                                        .ToList();

                        StringBuilder reporting = new StringBuilder();
                        if (timeNorms.Any(x => x.TimeNormShortName == "Экз"))
                        {
                            reporting.AppendLine("Экзамен");
                        }
                        if (timeNorms.Any(x => x.TimeNormShortName == "Зач"))
                        {
                            reporting.AppendLine("Зачет");
                        }
                        if (timeNorms.Any(x => x.TimeNormShortName == "ЗсО"))
                        {
                            reporting.AppendLine("Зачет с оценкой");
                        }
                        if (timeNorms.Any(x => x.TimeNormShortName == "КР"))
                        {
                            reporting.AppendLine("Курсовая работа");
                        }
                        if (timeNorms.Any(x => x.TimeNormShortName == "КП"))
                        {
                            reporting.AppendLine("Курсовой проект");
                        }

                        #region заголовок
                        body.AppendChild(CreateParagraph(new List<Tuple<string, RunProperties>>
                                {
                                    new Tuple<string, RunProperties>("ГРАФИК УЧЕБНОГО ПРОЦЕССА: ", Properties["MainBold12"])
                                }, center: true));
                        #endregion

                        #region Общая информация: направление, группа, дисциплина
                        Table tableHeader = new Table();
                        tableHeader.AppendChild(new TableWidth
                        {
                            Width = "5000",
                            Type = TableWidthUnitValues.Pct
                        });
                        tableHeader.AppendChild(new TableProperties(
                            new TableBorders(
                                new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                                new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                                new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                                new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                                new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 },
                                new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.None), Size = 1 }
                            )
                        ));

                        #region rowFirst
                        TableRow rowFirst = new TableRow();
                        rowFirst.Append(CreateCell("40", centerCell: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("Факультет: ", Properties["MainBold12"]),
                            new Tuple<string, RunProperties>("ФИСТ", Properties["Main12"])
                        }));
                        rowFirst.Append(CreateCell("30", centerCell: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("Направление: ", Properties["MainBold12"]),
                            new Tuple<string, RunProperties>(elem.StudentGroup.EducationDirection.Cipher, Properties["Main12"])
                        }));
                        rowFirst.Append(CreateCell("30", centerCell: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("Группа: ", Properties["MainBold12"]),
                            new Tuple<string, RunProperties>(elem.StudentGroup.GroupName, Properties["Main12"])
                        }));
                        tableHeader.Append(rowFirst);
                        #endregion

                        #region rowSecond
                        TableRow rowSecond = new TableRow();
                        rowSecond.Append(CreateCell(string.Empty, centerCell: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("Дисциплина: ", Properties["MainBold12"]),
                            new Tuple<string, RunProperties>(elem.AcademicPlanRecord.Discipline.DisciplineName, Properties["Main12"])
                        }));
                        rowSecond.Append(CreateCell(string.Empty, centerCell: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>(elem.AcademicPlanRecord.Semester.ToString(), Properties["Main12"]),
                            new Tuple<string, RunProperties>(" семестр ", Properties["MainBold12"]),
                            new Tuple<string, RunProperties>(elem.AcademicPlanRecord.AcademicPlan.AcademicYear.Title, Properties["Main12"]),
                            new Tuple<string, RunProperties>(" гг.", Properties["MainBold12"])
                        }));
                        rowSecond.Append(CreateCell(string.Empty, centerCell: true));
                        tableHeader.Append(rowSecond);
                        #endregion

                        body.Append(tableHeader);
                        #endregion

                        #region Empty Paragraph
                        body.AppendChild(new Paragraph());
                        #endregion

                        #region MainTable
                        Table tableMain = new Table();
                        tableMain.AppendChild(new TableWidth
                        {
                            Width = "5000",
                            Type = TableWidthUnitValues.Pct
                        });
                        tableMain.AppendChild(new TableProperties(
                            new TableBorders(
                                new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                                new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                                new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                                new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                                new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                                new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 }
                            )
                        ));

                        #region Header
                        TableRow rowHeader = new TableRow();
                        rowHeader.Append(new TableRowHeight
                        {
                            Val = 500
                        });
                        rowHeader.Append(CreateCell("200", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("Форма занятий", Properties["MainBold12"])
                        }));
                        rowHeader.Append(CreateCell("100", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("Недели", Properties["MainBold12"])
                        }));
                        for (int i = 1; i <= 16; ++i)
                        {
                            rowHeader.Append(CreateCell("35", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                            {
                                new Tuple<string, RunProperties>(i.ToString(), Properties["MainBold12"])
                            }));
                        }
                        rowHeader.Append(CreateCell("40", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("∑", Properties["MainBold12"])
                        }));
                        rowHeader.Append(CreateCell("100", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                        {
                            new Tuple<string, RunProperties>("Отчетн", Properties["MainBold12"])
                        }));
                        tableMain.Append(rowHeader);
                        #endregion

                        // Есть ли лекции
                        var timeNorm = timeNorms.FirstOrDefault(y => y.TimeNormShortName == "Лек");
                        List<AcademicPlanRecordMission> missions = timeNorm != null ? context.AcademicPlanRecordMissions.Where(x => x.AcademicPlanRecordElement.TimeNormId == timeNorm.Id &&
                                                x.AcademicPlanRecordElement.AcademicPlanRecordId == elem.AcademicPlanRecordId)
                                                .Include(x => x.AcademicPlanRecordElement)
                                                .Include(x => x.Lecturer)?.ToList() : null;
                        CreateRowDisciplineTimeDistributions(tableMain, "Лек", reporting.ToString(), timeNorm, missions, elem);

                        // Есть ли практики
                        timeNorm = timeNorms.FirstOrDefault(y => y.TimeNormShortName == "Пр");
                        missions = timeNorm != null ? context.AcademicPlanRecordMissions.Where(x => x.AcademicPlanRecordElement.TimeNormId == timeNorm.Id &&
                                                x.AcademicPlanRecordElement.AcademicPlanRecordId == elem.AcademicPlanRecordId)
                                                .Include(x => x.AcademicPlanRecordElement)
                                                .Include(x => x.Lecturer)?.ToList() : null;
                        CreateRowDisciplineTimeDistributions(tableMain, "Пр", reporting.ToString(), timeNorm, missions, elem);

                        // Есть ли лабораторные
                        timeNorm = timeNorms.FirstOrDefault(y => y.TimeNormShortName == "Лаб");
                        missions = timeNorm != null ? context.AcademicPlanRecordMissions.Where(x => x.AcademicPlanRecordElement.TimeNormId == timeNorm.Id &&
                                                x.AcademicPlanRecordElement.AcademicPlanRecordId == elem.AcademicPlanRecordId)
                                                .Include(x => x.AcademicPlanRecordElement)
                                                .Include(x => x.Lecturer)?.ToList() : null;
                        CreateRowDisciplineTimeDistributions(tableMain, "Лаб", reporting.ToString(), timeNorm, missions, elem);

                        #region Объединить
                        TableRow rowUnion = new TableRow();
                        rowUnion.Append(new TableRowHeight
                        {
                            Val = 300
                        });
                        #region cellFormUnion
                        TableCell cellFormUnion = new TableCell();
                        cellFormUnion.Append(GetTableCellProperties("", center: true));
                        cellFormUnion.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                        cellFormUnion.Append(CreateParagraph(new List<Tuple<string, RunProperties>>
                                {
                                    new Tuple<string, RunProperties>("Объединить с потоком", Properties["MainBold12"])
                                }));
                        rowUnion.Append(cellFormUnion);
                        #endregion
                        #region cellClassroomsUnion
                        TableCell cellClassroomsUnion = new TableCell();
                        cellClassroomsUnion.Append(GetTableCellProperties("", center: true));
                        cellClassroomsUnion.Append(CreateParagraph(new List<Tuple<string, RunProperties>>
                                {
                                    new Tuple<string, RunProperties>(elem.Comment, Properties["Main12"])
                                }));
                        cellClassroomsUnion.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Restart };
                        rowUnion.Append(cellClassroomsUnion);
                        for (int i = 0; i < 17; ++i)
                        {
                            TableCell cellWeekUnion = new TableCell();
                            cellWeekUnion.Append(GetTableCellProperties(""));
                            cellWeekUnion.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Continue };
                            cellWeekUnion.Append(new Paragraph());
                            rowUnion.Append(cellWeekUnion);
                        }
                        #endregion
                        #region cellResUnion
                        TableCell cellResUnion = new TableCell();
                        cellResUnion.Append(GetTableCellProperties(""));
                        cellResUnion.Append(CreateParagraph(new List<Tuple<string, RunProperties>>
                                {
                                    new Tuple<string, RunProperties>("Зав. кафедрой", Properties["Main12"])
                                }));
                        rowUnion.Append(cellResUnion);
                        #endregion
                        tableMain.Append(rowUnion);
                        #endregion
                        #region Пожелания
                        TableRow rowWishes = new TableRow();
                        rowWishes.Append(new TableRowHeight
                        {
                            Val = 300
                        });
                        #region cellFormWishes
                        TableCell cellFormWishes = new TableCell();
                        cellFormWishes.Append(GetTableCellProperties("", center: true));
                        cellFormWishes.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                        cellFormWishes.Append(CreateParagraph(new List<Tuple<string, RunProperties>>
                                {
                                    new Tuple<string, RunProperties>("Пожелания преподавателя", Properties["MainBold12"])
                                }));
                        rowWishes.Append(cellFormWishes);
                        #endregion
                        #region cellClassroomsWishes
                        TableCell cellClassroomsWishes = new TableCell();
                        cellClassroomsWishes.Append(GetTableCellProperties("", center: true));
                        cellClassroomsWishes.Append(CreateParagraph(new List<Tuple<string, RunProperties>>
                                {
                                    new Tuple<string, RunProperties>(elem.CommentWishesOfTeacher, Properties["Main12"])
                                }));
                        cellClassroomsWishes.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Restart };
                        rowWishes.Append(cellClassroomsWishes);
                        for (int i = 0; i < 17; ++i)
                        {
                            TableCell cellWeekWishes = new TableCell();
                            cellWeekWishes.Append(GetTableCellProperties(""));
                            cellWeekWishes.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Continue };
                            cellWeekWishes.Append(new Paragraph());
                            rowWishes.Append(cellWeekWishes);
                        }
                        #endregion
                        #region cellResWishes
                        TableCell cellResWishes = new TableCell();
                        cellResWishes.Append(GetTableCellProperties(""));
                        cellResWishes.Append(new Paragraph());
                        rowWishes.Append(cellResWishes);
                        #endregion
                        tableMain.Append(rowWishes);
                        #endregion

                        body.Append(tableMain);
                        #endregion

                        #region Empty Paragraphs
                        body.AppendChild(new Paragraph());
                        #endregion
                    }
                    mainPart.Document.Append(body);
                }
            }
        }

        private static void CreateRowDisciplineTimeDistributions(Table tableMain, string element, string reporting, TimeNorm timeNorm, List<AcademicPlanRecordMission> missions, DisciplineTimeDistribution elem)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                string title = string.Empty;
                switch (element)
                {
                    case "Лек":
                        title = "1. Лекции";
                        break;
                    case "Пр":
                        title = "2. Практ. занятия, семинары";
                        break;
                    case "Лаб":
                        title = "3. Лабораторные занятия";
                        break;
                }

                #region rowFirst
                TableRow rowFirst = new TableRow();
                rowFirst.Append(new TableRowHeight
                {
                    Val = 300
                });
                
                rowFirst.Append(CreateCell("", centerCell: true, vertical: MergedCellValues.Restart, elements: new List<Tuple<string, RunProperties>>
                {
                    new Tuple<string, RunProperties>(title, Properties["MainBold12"])
                }));
                rowFirst.Append(CreateCell("", centerCell: true, elements: new List<Tuple<string, RunProperties>>
                {
                    new Tuple<string, RunProperties>("аудит.", Properties["MainBold12"])
                }));

                double? firstWeek = null;
                double? secondWeek = null;
                double? nineWeek = null;
                double? tenWeek = null;
                if (timeNorm != null)
                {
                    var dtdrs = context.DisciplineTimeDistributionRecords.Where(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNorm.Id && !x.IsDeleted).ToList();
                    firstWeek = dtdrs.FirstOrDefault(x => x.WeekNumber == 1)?.Hours;
                    secondWeek = dtdrs.FirstOrDefault(x => x.WeekNumber == 2)?.Hours;
                    nineWeek = dtdrs.FirstOrDefault(x => x.WeekNumber == 9)?.Hours;
                    tenWeek = dtdrs.FirstOrDefault(x => x.WeekNumber == 10)?.Hours;
                }

                for (int i = 0; i < 8; ++i)
                {
                    rowFirst.Append(CreateCell("", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                    {
                        new Tuple<string, RunProperties>(i < 4 ? firstWeek > 0 ? firstWeek?.ToString() : "" : nineWeek > 0 ? nineWeek?.ToString() : "", Properties["Main12"])
                    }));
                    rowFirst.Append(CreateCell("", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                    {
                        new Tuple<string, RunProperties>(i < 4 ? secondWeek > 0 ? secondWeek?.ToString() : "" : tenWeek > 0 ? tenWeek?.ToString() : "", Properties["Main12"])
                    }));
                }
                rowFirst.Append(CreateCell("", centerCell: true, centerText: true, vertical: MergedCellValues.Restart, elements: new List<Tuple<string, RunProperties>>
                {
                    new Tuple<string, RunProperties>(missions?.FirstOrDefault()?.AcademicPlanRecordElement?.PlanHours.ToString("n0") ?? "", Properties["Main12"])
                }));
                if (element == "Лек")
                {
                    rowFirst.Append(CreateCell("", centerCell: true, centerText: true, vertical: MergedCellValues.Restart, elements: new List<Tuple<string, RunProperties>>
                    {
                        new Tuple<string, RunProperties>(reporting, Properties["Main12"])
                    }));
                }
                else
                {
                    rowFirst.Append(CreateCell("", centerCell: true, centerText: true, vertical: MergedCellValues.Continue));
                }
                tableMain.Append(rowFirst);
                #endregion

                #region rowSecond
                TableRow rowSecond = new TableRow();
                rowSecond.Append(new TableRowHeight
                {
                    Val = 300
                });
                rowSecond.Append(CreateCell("", vertical: MergedCellValues.Continue));

                string classroomDescription = "";
                if (timeNorm != null)
                {
                    var dtdc = context.DisciplineTimeDistributionClassrooms.FirstOrDefault(x => x.DisciplineTimeDistributionId == elem.Id && x.TimeNormId == timeNorm.Id && !x.IsDeleted);
                    if (dtdc != null)
                    {
                        classroomDescription = dtdc.ClassroomDescription;
                    }
                }
                rowSecond.Append(CreateCell("", centerCell: true, centerText: true, elements: new List<Tuple<string, RunProperties>>
                {
                    new Tuple<string, RunProperties>(classroomDescription, Properties["Main12"])
                }));
                
                for (int i = 0; i < 16; ++i)
                {
                    if (i == 0)
                    {
                        var list = new List<Tuple<string, RunProperties>>();
                        switch (element)
                        {
                            case "Лек":
                                if (missions != null && missions.Count == 1)
                                {
                                    list.Add(new Tuple<string, RunProperties>(string.Format("Лекции ведет {0}", missions.First().Lecturer), Properties["Main12"]));
                                }
                                else if(missions != null)
                                {
                                    List<string> lecturers = new List<string>();
                                    foreach (var mission in missions)
                                    {
                                        lecturers.Add(mission.Lecturer.ToString());
                                    }
                                    list.Add(new Tuple<string, RunProperties>(string.Format("Лекции ведут {0}", string.Join("," , lecturers)), Properties["Main12"]));
                                }
                                break;
                            case "Пр":
                                if (missions != null && missions.Count == 1)
                                {
                                    list.Add(new Tuple<string, RunProperties>(string.Format("Практики ведет {0}", missions.First().Lecturer), Properties["Main12"]));
                                }
                                else if (missions != null)
                                {
                                    List<string> lecturers = new List<string>();
                                    foreach (var mission in missions)
                                    {
                                        lecturers.Add(mission.Lecturer.ToString());
                                    }
                                    list.Add(new Tuple<string, RunProperties>(string.Format("Практики ведут {0}", string.Join(",", lecturers)), Properties["Main12"]));
                                }
                                break;
                            case "Лаб":
                                if (missions != null)
                                {
                                    int countStudentInSubgroup = elem.AcademicPlanRecord.Contingent.CountStudetns / elem.AcademicPlanRecord.Contingent.CountSubgroups;
                                    int countSubgroup = elem.StudentGroup.Students.Count / countStudentInSubgroup;
                                    if(elem.StudentGroup.Students.Count > countSubgroup * countStudentInSubgroup + 5)
                                    {
                                        countSubgroup++;
                                    }
                                    if (missions.Count == 1)
                                    {
                                        for(int c = 1; c <= countSubgroup; ++c)
                                        {
                                            list.Add(new Tuple<string, RunProperties>(string.Format("{0} подгруппа -  {1}", c, missions.First().Lecturer), Properties["Main12"]));
                                        }
                                    }
                                    else
                                    {
                                        if(missions.Count == countSubgroup)
                                        {
                                            int counter = 1;
                                            foreach(var mission in missions)
                                            {
                                                list.Add(new Tuple<string, RunProperties>(string.Format("{0} подгруппа -  {1}", counter++, missions.First().Lecturer), Properties["Main12"]));
                                            }
                                        }
                                        else
                                        {
                                            list.Add(new Tuple<string, RunProperties>(string.Format("Количество подгрупп: {0}", countSubgroup), Properties["Main12"]));
                                            List<string> lecturers = new List<string>();
                                            foreach (var mission in missions)
                                            {
                                                lecturers.Add(mission.Lecturer.ToString());
                                            }
                                            list.Add(new Tuple<string, RunProperties>(string.Format("Преподаватели: {0}", string.Join(",", lecturers)), Properties["Main12"]));
                                        }
                                    }
                                }
                                break;
                        }
                        rowSecond.Append(CreateCell("", horizontal: MergedCellValues.Restart, elements: list, multiParagraphs: true));
                    }
                    else if(i == 8)
                    {
                        rowSecond.Append(CreateCell("", horizontal: MergedCellValues.Restart));
                    }
                    else
                    {
                        rowSecond.Append(CreateCell("", horizontal: MergedCellValues.Continue));
                    }
                }
                rowSecond.Append(CreateCell("", vertical: MergedCellValues.Continue));
                rowSecond.Append(CreateCell("", vertical: MergedCellValues.Continue));

                tableMain.Append(rowSecond);
                #endregion
            }

        }

        private static TableCell CreateCell(string width, bool centerCell = false, List < Tuple<string, RunProperties>> elements = null, bool centerText = false,
            MergedCellValues? vertical = null, MergedCellValues? horizontal = null, bool multiParagraphs = false)
        {
            TableCell cell = new TableCell();
            cell.Append(GetTableCellProperties(width, center: centerCell));
            if(vertical.HasValue)
            {
                cell.TableCellProperties.VerticalMerge = new VerticalMerge { Val = vertical.Value };
            }
            if (horizontal.HasValue)
            {
                cell.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = horizontal.Value };
            }
            if (elements != null)
            {
                if (multiParagraphs && elements.Count > 1)
                {
                    foreach (var elem in elements)
                    {
                        cell.Append(CreateParagraph(new List<Tuple<string, RunProperties>> { new Tuple<string, RunProperties>(elem.Item1, elem.Item2) }, center: centerText).CloneNode(true));
                    }
                }
                else
                {
                    cell.Append(CreateParagraph(elements, center: centerText));
                }
            }
            else
            {
                cell.Append(new Paragraph());
            }
            return cell;
        }

        private static Paragraph CreateParagraph(List<Tuple<string, RunProperties>> elements, bool center = false)
        {
            Paragraph paragrath = new Paragraph();

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            SpacingBetweenLines spacing = new SpacingBetweenLines() { Line = "240", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = "0" };
            paragraphProperties.Append(spacing);

            if (center)
            {
                Justification justificationCenter = new Justification() { Val = JustificationValues.Center };
                paragraphProperties.Append(justificationCenter);
            }

            paragrath.Append(paragraphProperties);
            foreach (var elem in elements)
            {
                Run run = new Run();
                run.AppendChild(new Text(elem.Item1)
                {
                    Text = elem.Item1,
                    Space = SpaceProcessingModeValues.Preserve
                });
                run.PrependChild(elem.Item2.CloneNode(true));
                paragrath.AppendChild(run);
            }

            return paragrath;
        }

        private static TableCellProperties GetTableCellProperties(string width, bool center = false)
        {
            TableCellProperties tableCellProperties = new TableCellProperties();
            if (!string.IsNullOrEmpty(width))
            {
                TableCellWidth tableCellWidth = new TableCellWidth() { Type = TableWidthUnitValues.Pct, Width = width };
                tableCellProperties.Append(tableCellWidth);
            }
            if (center)
            {
                TableCellVerticalAlignment tableCellVerticalAlignment = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                tableCellProperties.Append(tableCellVerticalAlignment);
            }

            return tableCellProperties;
        }
    }
}
