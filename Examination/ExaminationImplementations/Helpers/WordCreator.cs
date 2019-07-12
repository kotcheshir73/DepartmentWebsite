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

                if (template.TicketTemplateBody != null)
                {
                    foreach (var ticket in tickets.OrderBy(x => x.TicketNumber))
                    {
                        _counterQuestions.Clear();

                        _examinationTemplateTicket = ticket;
                        _examinationTemplateTicketQuestions = ticket.ExaminationTemplateTicketQuestions.OrderBy(x => x.Order).Select(x => x).ToList();

                        var steps = (template.TicketTemplateBody.TicketTemplateParagraphs?.Count ?? 0) + (template.TicketTemplateBody.TicketTemplateTables?.Count ?? 0);
                        for (int i = 0; i < steps; ++i)
                        {
                            docBody.AppendChild(CreateParagraph(template.TicketTemplateBody.TicketTemplateParagraphs?.FirstOrDefault(x => x.Order == i)));
                            docBody.AppendChild(CreateTable(template.TicketTemplateBody.TicketTemplateTables?.FirstOrDefault(x => x.Order == i)));
                        }
                    }

                    docBody.AppendChild(CreateSectionProperties(template.TicketTemplateBody));
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
                SectionProperties properties = new SectionProperties();

                PageSize pageSize = new PageSize();
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageSizeHeight))
                {
                    pageSize.Height = Convert.ToUInt32(body.TicketTemplateBodyProperties.PageSizeHeight);
                }
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageSizeWidth))
                {
                    pageSize.Width = Convert.ToUInt32(body.TicketTemplateBodyProperties.PageSizeWidth);
                }
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageSizeOrient))
                {
                    pageSize.Orient = (PageOrientationValues)Enum.Parse(typeof(PageOrientationValues), body.TicketTemplateBodyProperties.PageSizeOrient.WithBigLetter());
                }

                properties.AppendChild(pageSize);

                PageMargin pageMargin = new PageMargin();
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageMarginBottom))
                {
                    pageMargin.Bottom = Convert.ToInt32(body.TicketTemplateBodyProperties.PageMarginBottom);
                }
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageMarginFooter))
                {
                    pageMargin.Footer = Convert.ToUInt32(body.TicketTemplateBodyProperties.PageMarginFooter);
                }
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageMarginGutter))
                {
                    pageMargin.Gutter = Convert.ToUInt32(body.TicketTemplateBodyProperties.PageMarginGutter);
                }
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageMarginLeft))
                {
                    pageMargin.Left = Convert.ToUInt32(body.TicketTemplateBodyProperties.PageMarginLeft);
                }
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageMarginRight))
                {
                    pageMargin.Right = Convert.ToUInt32(body.TicketTemplateBodyProperties.PageMarginRight);
                }
                if (!string.IsNullOrEmpty(body.TicketTemplateBodyProperties.PageMarginTop))
                {
                    pageMargin.Top = Convert.ToInt32(body.TicketTemplateBodyProperties.PageMarginTop);
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
                TableProperties properties = new TableProperties();

                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.Width))
                {
                    properties.AppendChild(new TableWidth() { Width = table.TicketTemplateTableProperties.Width });
                }

                TableLook tableLook = new TableLook();
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LookValue))
                {
                    tableLook.Val = table.TicketTemplateTableProperties.LookValue;
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LookFirstRow))
                {
                    tableLook.FirstRow = new OnOffValue(table.TicketTemplateTableProperties.LookFirstRow != "0");
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LookFirstColumn))
                {
                    tableLook.FirstColumn = new OnOffValue(table.TicketTemplateTableProperties.LookFirstColumn != "0");
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LookLastRow))
                {
                    tableLook.LastRow = new OnOffValue(table.TicketTemplateTableProperties.LookLastRow != "0");
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LookLastColumn))
                {
                    tableLook.LastColumn = new OnOffValue(table.TicketTemplateTableProperties.LookLastColumn != "0");
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LookNoHorizontalBand))
                {
                    tableLook.NoHorizontalBand = new OnOffValue(table.TicketTemplateTableProperties.LookNoHorizontalBand != "0");
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LookNoVerticalBand))
                {
                    tableLook.NoVerticalBand = new OnOffValue(table.TicketTemplateTableProperties.LookNoVerticalBand != "0");
                }
                properties.AppendChild(tableLook);

                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.LayoutType))
                {
                    properties.AppendChild(new TableLayout() { Type = (TableLayoutValues)Enum.Parse(typeof(TableLayoutValues), table.TicketTemplateTableProperties.LayoutType.WithBigLetter()) });
                }

                TableBorders tableBorders = new TableBorders();

                TopBorder topBorder = new TopBorder();
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderTopValue))
                {
                    topBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), table.TicketTemplateTableProperties.BorderTopValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderTopColor))
                {
                    topBorder.Color = table.TicketTemplateTableProperties.BorderTopColor;
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderTopSize))
                {
                    topBorder.Size = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderTopSize);
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderTopSpace))
                {
                    topBorder.Space = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderTopSpace);
                }
                tableBorders.AppendChild(topBorder);

                BottomBorder bottomBorder = new BottomBorder();
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderBottomValue))
                {
                    bottomBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), table.TicketTemplateTableProperties.BorderBottomValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderBottomColor))
                {
                    bottomBorder.Color = table.TicketTemplateTableProperties.BorderBottomColor;
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderBottomSize))
                {
                    bottomBorder.Size = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderBottomSize);
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderBottomSpace))
                {
                    bottomBorder.Space = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderBottomSpace);
                }
                tableBorders.AppendChild(bottomBorder);

                LeftBorder leftBorder = new LeftBorder();
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderLeftValue))
                {
                    leftBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), table.TicketTemplateTableProperties.BorderLeftValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderLeftColor))
                {
                    leftBorder.Color = table.TicketTemplateTableProperties.BorderLeftColor;
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderLeftSize))
                {
                    leftBorder.Size = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderLeftSize);
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderLeftSpace))
                {
                    leftBorder.Space = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderLeftSpace);
                }
                tableBorders.AppendChild(leftBorder);

                RightBorder rightBorder = new RightBorder();
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderRightValue))
                {
                    rightBorder.Val = (BorderValues)Enum.Parse(typeof(BorderValues), table.TicketTemplateTableProperties.BorderRightValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderRightColor))
                {
                    rightBorder.Color = table.TicketTemplateTableProperties.BorderRightColor;
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderRightSize))
                {
                    rightBorder.Size = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderRightSize);
                }
                if (!string.IsNullOrEmpty(table.TicketTemplateTableProperties.BorderRightSpace))
                {
                    rightBorder.Space = Convert.ToUInt32(table.TicketTemplateTableProperties.BorderRightSpace);
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
                if (!string.IsNullOrEmpty(row.TicketTemplateTableRowProperties.CantSplit) || !string.IsNullOrEmpty(row.TicketTemplateTableRowProperties.TableRowHeight))
                {
                    TableRowProperties properties = new TableRowProperties();

                    if (!string.IsNullOrEmpty(row.TicketTemplateTableRowProperties.CantSplit))
                    {
                        properties.AppendChild(new CantSplit() { Val = (OnOffOnlyValues)Enum.Parse(typeof(OnOffOnlyValues), row.TicketTemplateTableRowProperties.CantSplit.WithBigLetter()) });
                    }

                    if (!string.IsNullOrEmpty(row.TicketTemplateTableRowProperties.TableRowHeight))
                    {
                        properties.AppendChild(new TableRowHeight() { Val = Convert.ToUInt32(row.TicketTemplateTableRowProperties.TableRowHeight) });
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
                TableCellProperties properties = new TableCellProperties();

                if (!string.IsNullOrEmpty(cell.TicketTemplateTableCellProperties.TableCellWidth))
                {
                    properties.AppendChild(new TableCellWidth() { Width = cell.TicketTemplateTableCellProperties.TableCellWidth });
                }

                if (!string.IsNullOrEmpty(cell.TicketTemplateTableCellProperties.GridSpan))
                {
                    properties.AppendChild(new GridSpan() { Val = Convert.ToInt32(cell.TicketTemplateTableCellProperties.GridSpan) });
                }

                if (!string.IsNullOrEmpty(cell.TicketTemplateTableCellProperties.VerticalMerge))
                {
                    if (cell.TicketTemplateTableCellProperties.VerticalMerge != "continue")
                    {
                        properties.AppendChild(new VerticalMerge()
                        {
                            Val = (MergedCellValues)Enum.Parse(typeof(MergedCellValues), cell.TicketTemplateTableCellProperties.VerticalMerge.WithBigLetter())
                        });
                    }
                    else
                    {
                        properties.AppendChild(new VerticalMerge());
                    }
                }

                Shading shading = new Shading();
                if (!string.IsNullOrEmpty(cell.TicketTemplateTableCellProperties.ShadingValue))
                {
                    shading.Val = (ShadingPatternValues)Enum.Parse(typeof(ShadingPatternValues), cell.TicketTemplateTableCellProperties.ShadingValue.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(cell.TicketTemplateTableCellProperties.ShadingColor))
                {
                    shading.Color = cell.TicketTemplateTableCellProperties.ShadingColor;
                }
                if (!string.IsNullOrEmpty(cell.TicketTemplateTableCellProperties.ShadingFill))
                {
                    shading.Fill = cell.TicketTemplateTableCellProperties.ShadingFill;
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
                ParagraphProperties properties = new ParagraphProperties();

                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.NumberingLevelReference) || !string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.NumberingId))
                {
                    NumberingProperties numberingProperties = new NumberingProperties();
                    if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.NumberingLevelReference))
                    {
                        NumberingLevelReference numberingLevelReference = new NumberingLevelReference
                        {
                            Val = Convert.ToInt32(paragraph.TicketTemplateParagraphProperties.NumberingLevelReference)
                        };
                        numberingProperties.AppendChild(numberingLevelReference);
                    }
                    if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.NumberingId))
                    {
                        NumberingId numberingLevelReference = new NumberingId
                        {
                            Val = Convert.ToInt32(paragraph.TicketTemplateParagraphProperties.NumberingId) * 100 + _examinationTemplateTicket.TicketNumber
                        };
                        numberingProperties.AppendChild(numberingLevelReference);
                    }

                    properties.AppendChild(numberingProperties);
                }

                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.Justification))
                {
                    Justification justification = new Justification()
                    {
                        Val = (JustificationValues)Enum.Parse(typeof(JustificationValues), paragraph.TicketTemplateParagraphProperties.Justification.WithBigLetter())
                    };

                    properties.AppendChild(justification);
                }

                SpacingBetweenLines spacingBetweenLines = new SpacingBetweenLines();
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesLine))
                {
                    spacingBetweenLines.Line = paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesLine;
                }
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesLineRule))
                {
                    spacingBetweenLines.LineRule = (LineSpacingRuleValues)Enum.Parse(typeof(LineSpacingRuleValues), paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesLineRule.WithBigLetter());
                }
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesBefore))
                {
                    spacingBetweenLines.Before = paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesBefore;
                }
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesAfter))
                {
                    spacingBetweenLines.After = paragraph.TicketTemplateParagraphProperties.SpacingBetweenLinesAfter;
                }
                properties.AppendChild(spacingBetweenLines);

                Indentation indentation = new Indentation();
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.IndentationFirstLine))
                {
                    indentation.FirstLine = paragraph.TicketTemplateParagraphProperties.IndentationFirstLine;
                }
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.IndentationHanging))
                {
                    indentation.Hanging = paragraph.TicketTemplateParagraphProperties.IndentationHanging;
                }
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.IndentationLeft))
                {
                    indentation.Left = paragraph.TicketTemplateParagraphProperties.IndentationLeft;
                }
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.IndentationRight))
                {
                    indentation.Right = paragraph.TicketTemplateParagraphProperties.IndentationRight;
                }
                properties.AppendChild(indentation);

                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphProperties.RunSize))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraph.TicketTemplateParagraphProperties.RunSize });
                }
                if (paragraph.TicketTemplateParagraphProperties.RunBold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                if (paragraph.TicketTemplateParagraphProperties.RunItalic)
                {
                    paragraphMarkRunProperties.AppendChild(new Italic());
                }
                if (paragraph.TicketTemplateParagraphProperties.RunUnderline)
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
                if (!string.IsNullOrEmpty(run.TicketTemplateParagraphRunProperties.RunSize) || run.TicketTemplateParagraphRunProperties.RunBold ||
                    run.TicketTemplateParagraphRunProperties.RunItalic || run.TicketTemplateParagraphRunProperties.RunUnderline)
                {
                    RunProperties properties = new RunProperties();

                    if (!string.IsNullOrEmpty(run.TicketTemplateParagraphRunProperties.RunSize))
                    {
                        properties.AppendChild(new FontSize { Val = run.TicketTemplateParagraphRunProperties.RunSize });
                    }
                    if (run.TicketTemplateParagraphRunProperties.RunBold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    if (run.TicketTemplateParagraphRunProperties.RunItalic)
                    {
                        properties.AppendChild(new Italic());
                    }
                    if (run.TicketTemplateParagraphRunProperties.RunUnderline)
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