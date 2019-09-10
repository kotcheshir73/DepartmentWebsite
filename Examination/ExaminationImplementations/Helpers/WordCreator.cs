using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Models.Examination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExaminationImplementations.Helpers
{
    public static class WordCreator
    {
        private static ExaminationTemplate _examinationTemplate;

        private static ExaminationTemplateTicket _examinationTemplateTicket;

        private static List<ExaminationTemplateTicketQuestion> _examinationTemplateTicketQuestions;

        private static Dictionary<string, int> _counterQuestions;

        public static void CreateDoc(string filename, TicketTemplate template, List<ExaminationTemplateTicket> tickets, ExaminationTemplate examination)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filename, WordprocessingDocumentType.Document))
            {
                _counterQuestions = new Dictionary<string, int>();
                _examinationTemplate = examination;
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());

                CreateParts(template, mainPart);

                if (template.TicketTemplateBodies != null)
                {
                    var body = template.TicketTemplateBodies.FirstOrDefault();
                    foreach (var ticket in tickets.OrderBy(x => x.TicketNumber))
                    {
                        _counterQuestions.Clear();

                        _examinationTemplateTicket = ticket;
                        _examinationTemplateTicketQuestions = ticket.ExaminationTemplateTicketQuestions.OrderBy(x => x.Order).Select(x => x).ToList();

                        var steps = (body.TicketTemplateParagraphs?.Count ?? 0) + (body.TicketTemplateTables?.Count ?? 0);
                        for (int i = 0; i < steps; ++i)
                        {
                            docBody.AppendChild(CreateParagraph(body.TicketTemplateParagraphs?.FirstOrDefault(x => x.Order == i)));
                            docBody.AppendChild(CreateTable(body.TicketTemplateTables?.FirstOrDefault(x => x.Order == i)));
                        }
                    }

                    docBody.AppendChild(CreateSectionProperties(body));
                }

                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static void CreateParts(TicketTemplate template, MainDocumentPart mainPart)
        {
            CreateDocumentSettingsPart(template, mainPart);
            CreateFontTablesPart(template, mainPart);
            CreateNumberingsPart(template, mainPart);
            CreateStyleDefinitionsPart(template, mainPart);
            CreateThemePart(template, mainPart);
            CreateWebSetting(template, mainPart);
        }

        private static void CreateDocumentSettingsPart(TicketTemplate template, MainDocumentPart mainPart)
        {
            if (template.TicketTemplateDocumentSettings != null && template.TicketTemplateDocumentSettings.Count > 0)
            {
                var settings = mainPart.AddNewPart<DocumentSettingsPart>();
                settings.Settings = new Settings
                {
                    InnerXml = template.TicketTemplateDocumentSettings.First().InnerXml
                };
                settings.Settings.Save();
            }
        }

        private static void CreateFontTablesPart(TicketTemplate template, MainDocumentPart mainPart)
        {
            if (template.TicketTemplateFontTables != null && template.TicketTemplateFontTables.Count > 0)
            {
                var fonts = mainPart.AddNewPart<FontTablePart>();
                fonts.Fonts = new Fonts
                {
                    InnerXml = template.TicketTemplateFontTables.First().InnerXml
                };
                fonts.Fonts.Save();
            }
        }

        private static void CreateNumberingsPart(TicketTemplate template, MainDocumentPart mainPart)
        {
            if (template.TicketTemplateNumberings != null && template.TicketTemplateNumberings.Count > 0)
            {
                var numbering = mainPart.AddNewPart<NumberingDefinitionsPart>();
                numbering.Numbering = new Numbering
                {
                    InnerXml = template.TicketTemplateNumberings.First().InnerXml
                };
                numbering.Numbering.Save();
            }
        }

        private static void CreateStyleDefinitionsPart(TicketTemplate template, MainDocumentPart mainPart)
        {
            if (template.TicketTemplateStyleDefinitions != null && template.TicketTemplateStyleDefinitions.Count > 0)
            {
                var styles = mainPart.AddNewPart<StyleDefinitionsPart>();
                styles.Styles = new Styles
                {
                    InnerXml = template.TicketTemplateStyleDefinitions.First().InnerXml
                };
                styles.Styles.Save();
            }
        }

        private static void CreateThemePart(TicketTemplate template, MainDocumentPart mainPart)
        {
            if (template.TicketTemplateThemeParts != null && template.TicketTemplateThemeParts.Count > 0)
            {
                var thems = mainPart.AddNewPart<ThemePart>();
                thems.Theme = new DocumentFormat.OpenXml.Drawing.Theme
                {
                    InnerXml = template.TicketTemplateThemeParts.First().InnerXml
                };
                thems.Theme.Save();
            }
        }

        private static void CreateWebSetting(TicketTemplate template, MainDocumentPart mainPart)
        {
            if (template.TicketTemplateWebSettings != null && template.TicketTemplateWebSettings.Count > 0)
            {
                var settings = mainPart.AddNewPart<WebSettingsPart>();
                settings.WebSettings = new WebSettings
                {
                    InnerXml = template.TicketTemplateWebSettings.First().InnerXml
                };
                settings.WebSettings.Save();
            }
        }


        private static SectionProperties CreateSectionProperties(TicketTemplateBody body)
        {
            if (body.TicketTemplateBodyProperties != null)
            {
                var bodyPropertie = body.TicketTemplateBodyProperties.FirstOrDefault();

                SectionProperties properties = new SectionProperties();

                PageSize pageSize = new PageSize();
                if (!string.IsNullOrEmpty(bodyPropertie.PageSizeHeight))
                {
                    pageSize.Height = Convert.ToUInt32(bodyPropertie.PageSizeHeight);
                }
                if (!string.IsNullOrEmpty(bodyPropertie.PageSizeWidth))
                {
                    pageSize.Width = Convert.ToUInt32(bodyPropertie.PageSizeWidth);
                }
                if (!string.IsNullOrEmpty(bodyPropertie.PageSizeOrient))
                {
                    pageSize.Orient = (PageOrientationValues)Enum.Parse(typeof(PageOrientationValues), bodyPropertie.PageSizeOrient.WithBigLetter());
                }

                properties.AppendChild(pageSize);

                PageMargin pageMargin = new PageMargin();
                if (!string.IsNullOrEmpty(bodyPropertie.PageMarginBottom))
                {
                    pageMargin.Bottom = Convert.ToInt32(bodyPropertie.PageMarginBottom);
                }
                if (!string.IsNullOrEmpty(bodyPropertie.PageMarginFooter))
                {
                    pageMargin.Footer = Convert.ToUInt32(bodyPropertie.PageMarginFooter);
                }
                if (!string.IsNullOrEmpty(bodyPropertie.PageMarginGutter))
                {
                    pageMargin.Gutter = Convert.ToUInt32(bodyPropertie.PageMarginGutter);
                }
                if (!string.IsNullOrEmpty(bodyPropertie.PageMarginLeft))
                {
                    pageMargin.Left = Convert.ToUInt32(bodyPropertie.PageMarginLeft);
                }
                if (!string.IsNullOrEmpty(bodyPropertie.PageMarginRight))
                {
                    pageMargin.Right = Convert.ToUInt32(bodyPropertie.PageMarginRight);
                }
                if (!string.IsNullOrEmpty(bodyPropertie.PageMarginTop))
                {
                    pageMargin.Top = Convert.ToInt32(bodyPropertie.PageMarginTop);
                }

                properties.AppendChild(pageMargin);

                return properties;
            }

            return null;
        }

        private static Table CreateTable(TicketTemplateTable table)
        {
            if (table != null)
            {
                Table docTable = new Table();

                docTable.AppendChild<TableProperties>(CreateTableProperties(table));

                docTable.AppendChild(CreateGridColumn(table));

                foreach (var row in table.TicketTemplateTableRows.OrderBy(x => x.Order))
                {
                    docTable.AppendChild(CreateTableRow(row));
                }

                return docTable;
            }

            return null;
        }

        private static TableGrid CreateGridColumn(TicketTemplateTable table)
        {
            if (table != null)
            {
                TableGrid tableGrid = new TableGrid();

                foreach (var grid in table.TicketTemplateTableGridColumns.OrderBy(x => x.Order))
                {
                    tableGrid.AppendChild(new GridColumn() { Width = grid.Width });
                }

                return tableGrid;
            }

            return null;
        }

        private static TableProperties CreateTableProperties(TicketTemplateTable table)
        {
            if (table.TicketTemplateTableProperties != null)
            {
                var tableProperties = table.TicketTemplateTableProperties.FirstOrDefault();

                TableProperties properties = new TableProperties();

                if (!string.IsNullOrEmpty(tableProperties.Width))
                {
                    properties.AppendChild(new TableWidth() { Width = tableProperties.Width });
                }

                TableLook tableLook = new TableLook();
                if (!string.IsNullOrEmpty(tableProperties.LookValue))
                {
                    tableLook.Val = tableProperties.LookValue;
                }
                if (!string.IsNullOrEmpty(tableProperties.LookFirstRow))
                {
                    tableLook.FirstRow = new OnOffValue(tableProperties.LookFirstRow != "0");
                }
                if (!string.IsNullOrEmpty(tableProperties.LookFirstColumn))
                {
                    tableLook.FirstColumn = new OnOffValue(tableProperties.LookFirstColumn != "0");
                }
                if (!string.IsNullOrEmpty(tableProperties.LookLastRow))
                {
                    tableLook.LastRow = new OnOffValue(tableProperties.LookLastRow != "0");
                }
                if (!string.IsNullOrEmpty(tableProperties.LookLastColumn))
                {
                    tableLook.LastColumn = new OnOffValue(tableProperties.LookLastColumn != "0");
                }
                if (!string.IsNullOrEmpty(tableProperties.LookNoHorizontalBand))
                {
                    tableLook.NoHorizontalBand = new OnOffValue(tableProperties.LookNoHorizontalBand != "0");
                }
                if (!string.IsNullOrEmpty(tableProperties.LookNoVerticalBand))
                {
                    tableLook.NoVerticalBand = new OnOffValue(tableProperties.LookNoVerticalBand != "0");
                }
                properties.AppendChild(tableLook);

                if (!string.IsNullOrEmpty(tableProperties.LayoutType))
                {
                    properties.AppendChild(new TableLayout() { Type = (TableLayoutValues)Enum.Parse(typeof(TableLayoutValues), tableProperties.LayoutType.WithBigLetter()) });
                }

                TableBorders tableBorders = new TableBorders();

                TopBorder topBorder = new TopBorder();
                if (!string.IsNullOrEmpty(tableProperties.BorderTopValue))
                {
                    topBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), tableProperties.BorderTopValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderTopColor))
                {
                    topBorder.Color = tableProperties.BorderTopColor;
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderTopSize))
                {
                    topBorder.Size = Convert.ToUInt32(tableProperties.BorderTopSize);
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderTopSpace))
                {
                    topBorder.Space = Convert.ToUInt32(tableProperties.BorderTopSpace);
                }
                tableBorders.AppendChild(topBorder);

                BottomBorder bottomBorder = new BottomBorder();
                if (!string.IsNullOrEmpty(tableProperties.BorderBottomValue))
                {
                    bottomBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), tableProperties.BorderBottomValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderBottomColor))
                {
                    bottomBorder.Color = tableProperties.BorderBottomColor;
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderBottomSize))
                {
                    bottomBorder.Size = Convert.ToUInt32(tableProperties.BorderBottomSize);
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderBottomSpace))
                {
                    bottomBorder.Space = Convert.ToUInt32(tableProperties.BorderBottomSpace);
                }
                tableBorders.AppendChild(bottomBorder);

                LeftBorder leftBorder = new LeftBorder();
                if (!string.IsNullOrEmpty(tableProperties.BorderLeftValue))
                {
                    leftBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), tableProperties.BorderLeftValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderLeftColor))
                {
                    leftBorder.Color = tableProperties.BorderLeftColor;
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderLeftSize))
                {
                    leftBorder.Size = Convert.ToUInt32(tableProperties.BorderLeftSize);
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderLeftSpace))
                {
                    leftBorder.Space = Convert.ToUInt32(tableProperties.BorderLeftSpace);
                }
                tableBorders.AppendChild(leftBorder);

                RightBorder rightBorder = new RightBorder();
                if (!string.IsNullOrEmpty(tableProperties.BorderRightValue))
                {
                    rightBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), tableProperties.BorderRightValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderRightColor))
                {
                    rightBorder.Color = tableProperties.BorderRightColor;
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderRightSize))
                {
                    rightBorder.Size = Convert.ToUInt32(tableProperties.BorderRightSize);
                }
                if (!string.IsNullOrEmpty(tableProperties.BorderRightSpace))
                {
                    rightBorder.Space = Convert.ToUInt32(tableProperties.BorderRightSpace);
                }
                tableBorders.AppendChild(rightBorder);

                properties.AppendChild(tableBorders);

                return properties;
            }

            return null;
        }

        private static TableRow CreateTableRow(TicketTemplateTableRow row)
        {
            if (row != null)
            {
                TableRow docRow = new TableRow();

                docRow.AppendChild(CreateTableRowProperties(row));

                foreach (var cell in row.TicketTemplateTableCells.OrderBy(x => x.Order))
                {
                    docRow.AppendChild(CreateTableCell(cell));
                }

                return docRow;
            }

            return null;
        }

        private static TableRowProperties CreateTableRowProperties(TicketTemplateTableRow row)
        {
            if (row.TicketTemplateTableRowProperties != null)
            {
                var tablerowProperties = row.TicketTemplateTableRowProperties.FirstOrDefault();

                if (!string.IsNullOrEmpty(tablerowProperties.CantSplit) || !string.IsNullOrEmpty(tablerowProperties.TableRowHeight))
                {
                    TableRowProperties properties = new TableRowProperties();

                    if (!string.IsNullOrEmpty(tablerowProperties.CantSplit))
                    {
                        properties.AppendChild(new CantSplit() { Val = (OnOffOnlyValues)Enum.Parse(typeof(OnOffOnlyValues), tablerowProperties.CantSplit.WithBigLetter()) });
                    }

                    if (!string.IsNullOrEmpty(tablerowProperties.TableRowHeight))
                    {
                        properties.AppendChild(new TableRowHeight() { Val = Convert.ToUInt32(tablerowProperties.TableRowHeight) });
                    }

                    return properties;
                }
            }

            return null;
        }

        private static TableCell CreateTableCell(TicketTemplateTableCell cell)
        {
            if (cell != null)
            {
                TableCell docCell = new TableCell();

                docCell.AppendChild(CreateTableCellProperties(cell));

                foreach (var paragraph in cell.TicketTemplateParagraphs.OrderBy(x => x.Order))
                {
                    docCell.AppendChild(CreateParagraph(paragraph));
                }

                return docCell;
            }

            return null;
        }

        private static TableCellProperties CreateTableCellProperties(TicketTemplateTableCell cell)
        {
            if (cell.TicketTemplateTableCellProperties != null)
            {
                var tablecellPropertie = cell.TicketTemplateTableCellProperties.FirstOrDefault();

                TableCellProperties properties = new TableCellProperties();

                if (!string.IsNullOrEmpty(tablecellPropertie.TableCellWidth))
                {
                    properties.AppendChild(new TableCellWidth() { Width = tablecellPropertie.TableCellWidth });
                }

                if (!string.IsNullOrEmpty(tablecellPropertie.GridSpan))
                {
                    properties.AppendChild(new GridSpan() { Val = Convert.ToInt32(tablecellPropertie.GridSpan) });
                }

                if (!string.IsNullOrEmpty(tablecellPropertie.VerticalMerge))
                {
                    if (tablecellPropertie.VerticalMerge != "continue")
                    {
                        properties.AppendChild(new VerticalMerge()
                        {
                            Val = (MergedCellValues)Enum.Parse(typeof(MergedCellValues), tablecellPropertie.VerticalMerge.WithBigLetter())
                        });
                    }
                    else
                    {
                        properties.AppendChild(new VerticalMerge());
                    }
                }

                Shading shading = new Shading();
                if (!string.IsNullOrEmpty(tablecellPropertie.ShadingValue))
                {
                    shading.Val = (ShadingPatternValues)Enum.Parse(typeof(ShadingPatternValues), tablecellPropertie.ShadingValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(tablecellPropertie.ShadingColor))
                {
                    shading.Color = tablecellPropertie.ShadingColor;
                }
                if (!string.IsNullOrEmpty(tablecellPropertie.ShadingFill))
                {
                    shading.Fill = tablecellPropertie.ShadingFill;
                }
                properties.AppendChild(shading);

                return properties;
            }

            return null;
        }

        private static Paragraph CreateParagraph(TicketTemplateParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph));

                foreach (var run in paragraph.TicketTemplateParagraphRuns.OrderBy(x => x.Order))
                {
                    docParagraph.AppendChild(CreateRun(run));
                }

                return docParagraph;
            }

            return null;
        }

        private static ParagraphProperties CreateParagraphProperties(TicketTemplateParagraph paragraph)
        {
            if (paragraph.TicketTemplateParagraphProperties != null)
            {
                var paragraphPropertie = paragraph.TicketTemplateParagraphProperties.FirstOrDefault();

                ParagraphProperties properties = new ParagraphProperties();

                if (!string.IsNullOrEmpty(paragraphPropertie.NumberingLevelReference) || !string.IsNullOrEmpty(paragraphPropertie.NumberingId))
                {
                    NumberingProperties numberingProperties = new NumberingProperties();
                    if (!string.IsNullOrEmpty(paragraphPropertie.NumberingLevelReference))
                    {
                        NumberingLevelReference numberingLevelReference = new NumberingLevelReference
                        {
                            Val = Convert.ToInt32(paragraphPropertie.NumberingLevelReference)
                        };
                        numberingProperties.AppendChild(numberingLevelReference);
                    }
                    if (!string.IsNullOrEmpty(paragraphPropertie.NumberingId))
                    {
                        NumberingId numberingLevelReference = new NumberingId
                        {
                            Val = Convert.ToInt32(paragraphPropertie.NumberingId) * 100 + _examinationTemplateTicket.TicketNumber
                        };
                        numberingProperties.AppendChild(numberingLevelReference);
                    }

                    properties.AppendChild(numberingProperties);
                }

                if (!string.IsNullOrEmpty(paragraphPropertie.Justification))
                {
                    Justification justification = new Justification()
                    {
                        Val = (JustificationValues)Enum.Parse(typeof(JustificationValues), paragraphPropertie.Justification.WithBigLetter())
                    };

                    properties.AppendChild(justification);
                }

                SpacingBetweenLines spacingBetweenLines = new SpacingBetweenLines();
                if (!string.IsNullOrEmpty(paragraphPropertie.SpacingBetweenLinesLine))
                {
                    spacingBetweenLines.Line = paragraphPropertie.SpacingBetweenLinesLine;
                }
                if (!string.IsNullOrEmpty(paragraphPropertie.SpacingBetweenLinesLineRule))
                {
                    spacingBetweenLines.LineRule = (LineSpacingRuleValues)Enum.Parse(typeof(LineSpacingRuleValues), paragraphPropertie.SpacingBetweenLinesLineRule.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(paragraphPropertie.SpacingBetweenLinesBefore))
                {
                    spacingBetweenLines.Before = paragraphPropertie.SpacingBetweenLinesBefore;
                }
                if (!string.IsNullOrEmpty(paragraphPropertie.SpacingBetweenLinesAfter))
                {
                    spacingBetweenLines.After = paragraphPropertie.SpacingBetweenLinesAfter;
                }
                properties.AppendChild(spacingBetweenLines);

                Indentation indentation = new Indentation();
                if (!string.IsNullOrEmpty(paragraphPropertie.IndentationFirstLine))
                {
                    indentation.FirstLine = paragraphPropertie.IndentationFirstLine;
                }
                if (!string.IsNullOrEmpty(paragraphPropertie.IndentationHanging))
                {
                    indentation.Hanging = paragraphPropertie.IndentationHanging;
                }
                if (!string.IsNullOrEmpty(paragraphPropertie.IndentationLeft))
                {
                    indentation.Left = paragraphPropertie.IndentationLeft;
                }
                if (!string.IsNullOrEmpty(paragraphPropertie.IndentationRight))
                {
                    indentation.Right = paragraphPropertie.IndentationRight;
                }
                properties.AppendChild(indentation);

                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphPropertie.RunSize))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphPropertie.RunSize });
                }
                if (paragraphPropertie.RunBold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                if (paragraphPropertie.RunItalic)
                {
                    paragraphMarkRunProperties.AppendChild(new Italic());
                }
                if (paragraphPropertie.RunUnderline)
                {
                    paragraphMarkRunProperties.AppendChild(new Underline());
                }
                properties.AppendChild(paragraphMarkRunProperties);

                return properties;
            }

            return null;
        }

        private static Run CreateRun(TicketTemplateParagraphRun run)
        {
            if (run != null)
            {
                Run docRun = new Run();

                docRun.AppendChild(CreateRunProperties(run));

                if (run.Break)
                {
                    Break @break = new Break();
                    if (!string.IsNullOrEmpty(run.BreakType))
                    {
                        @break.Type = (BreakValues)Enum.Parse(typeof(BreakValues), run.BreakType.WithBigLetter());
                    }
                    docRun.AppendChild(@break);
                }
                else if (run.TabChar)
                {
                    docRun.AppendChild(new TabChar());
                }
                else
                {
                    string textResult = run.Text;
                    Match match = Regex.Match(textResult, @"\{\#[a-z,A-Z,0-9,\:,\,]*\}");
                    if (match.Success)
                    {
                        string matchValue = match.Value.Substring(2, match.Value.Length - 3);
                        docRun.AppendChild(new Text { Text = run.Text.Replace(match.Value, ReplacmentText(matchValue)), Space = SpaceProcessingModeValues.Preserve });
                    }
                    else
                    {
                        docRun.AppendChild(new Text { Text = run.Text, Space = SpaceProcessingModeValues.Preserve });
                    }
                }

                return docRun;
            }

            return null;
        }

        private static RunProperties CreateRunProperties(TicketTemplateParagraphRun run)
        {
            if (run.TicketTemplateParagraphRunProperties != null)
            {
                var runPropertie = run.TicketTemplateParagraphRunProperties.FirstOrDefault();

                if (!string.IsNullOrEmpty(runPropertie.RunSize) || runPropertie.RunBold || runPropertie.RunItalic || runPropertie.RunUnderline)
                {
                    RunProperties properties = new RunProperties();

                    if (!string.IsNullOrEmpty(runPropertie.RunSize))
                    {
                        properties.AppendChild(new FontSize { Val = runPropertie.RunSize });
                    }
                    if (runPropertie.RunBold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    if (runPropertie.RunItalic)
                    {
                        properties.AppendChild(new Italic());
                    }
                    if (runPropertie.RunUnderline)
                    {
                        properties.AppendChild(new Underline());
                    }

                    return properties;
                }
            }

            return null;
        }

        private static string ReplacmentText(string maket)
        {
            if (maket.StartsWith("question"))
            {
                var block = _examinationTemplate.ExaminationTemplateBlocks.FirstOrDefault(x => x.QuestionTagInTemplate == maket);
                if (block != null)
                {
                    var question = _examinationTemplateTicket.ExaminationTemplateTicketQuestions.FirstOrDefault(x => x.ExaminationTemplateBlockId == block.Id);
                    if (question != null)
                    {
                        _examinationTemplateTicketQuestions.Remove(question);
                        //TODO Image
                        return question.ExaminationTemplateBlockQuestion.QuestionText;
                    }
                }
            }
            else if (maket.StartsWith("random"))
            {
                if(!_counterQuestions.ContainsKey(maket.Split(':')[0]))
                {
                    _counterQuestions.Add(maket.Split(':')[0], 0);
                }
                else
                {
                    _counterQuestions[maket.Split(':')[0]]++;
                }
                var block = _examinationTemplate.ExaminationTemplateBlocks.FirstOrDefault(x => x.QuestionTagInTemplate == maket.Split(':')[0]);
                if (block != null)
                {
                    var question = _examinationTemplateTicketQuestions.FirstOrDefault(x => x.ExaminationTemplateBlockId == block.Id);
                    if (question != null)
                    {
                        _examinationTemplateTicketQuestions.Remove(question);
                        //TODO Image
                        return question.ExaminationTemplateBlockQuestion.QuestionText;
                    }
                }
            }
            else if (maket.StartsWith("number"))
            {
                if (_examinationTemplateTicket != null)
                {
                    return _examinationTemplateTicket.TicketNumber.ToString();
                }
            }
            else if (maket.StartsWith("discipline"))
            {
                if (_examinationTemplate != null && _examinationTemplate.Discipline != null)
                {
                    return _examinationTemplate.Discipline.DisciplineName;
                }
            }
            else if (maket.StartsWith("education"))
            {
                if (_examinationTemplate != null && _examinationTemplate.EducationDirection != null)
                {
                    return $"{_examinationTemplate.EducationDirection.Cipher} {_examinationTemplate.EducationDirection.Title}";
                }
            }
            else if (maket.StartsWith("semester"))
            {
                if (_examinationTemplate != null && _examinationTemplate.Semester != null)
                {
                    return _examinationTemplate.Semester.ToString();
                }
            }
            else if (maket.StartsWith("date"))
            {
                return DateTime.Now.ToShortDateString();
            }

            return maket;
        }

        private static string WithBigLetter(this string value)
        {
            return value[0].ToString().ToUpper() + value.Substring(1);
        }
    }
}