using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Models.Examination;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationImplementations.Helpers
{
    public static class WordParser
    {
        public static TicketTemplateBody ParseDocument(string filename)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, false))
            {
                var doc = wordDocument.DocumentType;
                int order = 0;
                TicketTemplateBody body = new TicketTemplateBody
                {
                    TicketTemplateParagraphs = new List<TicketTemplateParagraph>(),
                    TicketTemplateTables = new List<TicketTemplateTable>()
                };
                // находим свойства документа
                body.TicketTemplateBodyProperties = GetBodyProperties(wordDocument.MainDocumentPart.Document.Body, body.Id);
                body.TicketTemplateBodyPropertiesId = body.TicketTemplateBodyProperties.Id;
                foreach (OpenXmlElement element in wordDocument.MainDocumentPart.Document.Body.ChildElements)
                {
                    // находим параграф
                    if (element is DocumentFormat.OpenXml.Wordprocessing.Paragraph)
                    {
                        body.TicketTemplateParagraphs.Add(GetParagraph(order++, element as DocumentFormat.OpenXml.Wordprocessing.Paragraph, body.Id, null));
                    }
                    else if (element is DocumentFormat.OpenXml.Wordprocessing.Table)
                    {
                        body.TicketTemplateTables.Add(GetTable(order++, element as DocumentFormat.OpenXml.Wordprocessing.Table, body.Id));
                    }
                }

                return body;
            }
        }

        /// <summary>
        /// Извлечение свойств документа
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static TicketTemplateBodyProperties GetBodyProperties(OpenXmlElement element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateBodyProperties bodyProperties = new TicketTemplateBodyProperties { TicketTemplateBodyId = Id };
            var elementProeprties = element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.SectionProperties>()?.FirstOrDefault();
            if (elementProeprties != null)
            {
                foreach (var elem in element.ChildElements)
                {
                    if (elem is DocumentFormat.OpenXml.Wordprocessing.PageSize)
                    {
                        bodyProperties.PageSizeHeight = (elem as DocumentFormat.OpenXml.Wordprocessing.PageSize).Height;
                        bodyProperties.PageSizeWidth = (elem as DocumentFormat.OpenXml.Wordprocessing.PageSize).Width;
                        bodyProperties.PageSizeOrient = (elem as DocumentFormat.OpenXml.Wordprocessing.PageSize).Orient;
                    }
                    if (elem is DocumentFormat.OpenXml.Wordprocessing.PageMargin)
                    {
                        bodyProperties.PageMarginBottom = (elem as DocumentFormat.OpenXml.Wordprocessing.PageMargin).Bottom;
                        bodyProperties.PageMarginTop = (elem as DocumentFormat.OpenXml.Wordprocessing.PageMargin).Top;
                        bodyProperties.PageMarginLeft = (elem as DocumentFormat.OpenXml.Wordprocessing.PageMargin).Left;
                        bodyProperties.PageMarginRight = (elem as DocumentFormat.OpenXml.Wordprocessing.PageMargin).Right;
                        bodyProperties.PageMarginFooter = (elem as DocumentFormat.OpenXml.Wordprocessing.PageMargin).Footer;
                        bodyProperties.PageMarginGutter = (elem as DocumentFormat.OpenXml.Wordprocessing.PageMargin).Gutter;
                    }
                }
            }
            return bodyProperties;
        }

        private static TicketTemplateTable GetTable(int order, DocumentFormat.OpenXml.Wordprocessing.Table element, Guid? BodyId)
        {
            if (element == null) { return null; }
            TicketTemplateTable table = new TicketTemplateTable
            {
                TicketTemplateBodyId = BodyId,
                Order = order,
                TicketTemplateTableGridColumns = new List<TicketTemplateTableGridColumn>(),
                TicketTemplateTableRows = new List<TicketTemplateTableRow>()
            };
            // свойства таблицы
            table.TicketTemplateTableProperties = GetTableProperties(element as DocumentFormat.OpenXml.Wordprocessing.Table, table.Id);
            table.TicketTemplateTablePropertiesId = table.TicketTemplateTableProperties.Id;
            int rowOrder = 0;
            foreach (var elem in element.ChildElements)
            {
                if (elem is DocumentFormat.OpenXml.Wordprocessing.TableGrid)
                { // колонки таблицы
                    int columnOrder = 0;
                    foreach (var column in elem.ChildElements)
                    {
                        if (column is DocumentFormat.OpenXml.Wordprocessing.GridColumn)
                        {
                            table.TicketTemplateTableGridColumns.Add(GetTableGrid(columnOrder++, column as DocumentFormat.OpenXml.Wordprocessing.GridColumn, table.Id));
                        }
                    }
                }
                else if (elem is DocumentFormat.OpenXml.Wordprocessing.TableRow)
                {
                    table.TicketTemplateTableRows.Add(GetTableRow(rowOrder++, elem as DocumentFormat.OpenXml.Wordprocessing.TableRow, table.Id));
                }
            }

            return table;
        }

        /// <summary>
        /// Извлечение свойств таблицы
        /// </summary>
        /// <param name="element"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static TicketTemplateTableProperties GetTableProperties(DocumentFormat.OpenXml.Wordprocessing.Table element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateTableProperties properties = new TicketTemplateTableProperties { TicketTemplateTableId = Id };
            var elementProeprties = element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.TableProperties>()?.FirstOrDefault();
            if (elementProeprties != null)
            {
                // идем по вложенным элементам
                foreach (var elem in elementProeprties.ChildElements)
                {
                    //// ширина
                    if (elem is DocumentFormat.OpenXml.Wordprocessing.TableWidth)
                    {
                        properties.Width = (elem as DocumentFormat.OpenXml.Wordprocessing.TableWidth).Width;
                    }
                    // 
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.TableLook)
                    {
                        properties.LookValue = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLook).Val;
                        properties.LookFirstRow = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLook).FirstRow;
                        properties.LookLastRow = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLook).LastRow;
                        properties.LookFirstColumn = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLook).FirstColumn;
                        properties.LookLastColumn = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLook).LastColumn;
                        properties.LookNoHorizontalBand = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLook).NoHorizontalBand;
                        properties.LookNoVerticalBand = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLook).NoVerticalBand;
                    }
                    //
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.TableLayout)
                    {
                        properties.LayoutType = (elem as DocumentFormat.OpenXml.Wordprocessing.TableLayout).Type;
                    }
                    //// свойства текста
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.TableBorders)
                    {
                        foreach (var el in elem.ChildElements)
                        {
                            if (el is DocumentFormat.OpenXml.Wordprocessing.TopBorder)
                            {
                                properties.BorderTopValue = (el as DocumentFormat.OpenXml.Wordprocessing.TopBorder).Val;
                                properties.BorderTopColor = (el as DocumentFormat.OpenXml.Wordprocessing.TopBorder).Color;
                                properties.BorderTopSize = (el as DocumentFormat.OpenXml.Wordprocessing.TopBorder).Size;
                                properties.BorderTopSpace = (el as DocumentFormat.OpenXml.Wordprocessing.TopBorder).Space;
                            }
                            else if (el is DocumentFormat.OpenXml.Wordprocessing.BottomBorder)
                            {
                                properties.BorderBottomValue = (el as DocumentFormat.OpenXml.Wordprocessing.BottomBorder).Val;
                                properties.BorderBottomColor = (el as DocumentFormat.OpenXml.Wordprocessing.BottomBorder).Color;
                                properties.BorderBottomSize = (el as DocumentFormat.OpenXml.Wordprocessing.BottomBorder).Size;
                                properties.BorderBottomSpace = (el as DocumentFormat.OpenXml.Wordprocessing.BottomBorder).Space;
                            }
                            else if (el is DocumentFormat.OpenXml.Wordprocessing.LeftBorder)
                            {
                                properties.BorderLeftValue = (el as DocumentFormat.OpenXml.Wordprocessing.LeftBorder).Val;
                                properties.BorderLeftColor = (el as DocumentFormat.OpenXml.Wordprocessing.LeftBorder).Color;
                                properties.BorderLeftSize = (el as DocumentFormat.OpenXml.Wordprocessing.LeftBorder).Size;
                                properties.BorderLeftSpace = (el as DocumentFormat.OpenXml.Wordprocessing.LeftBorder).Space;
                            }
                            else if (el is DocumentFormat.OpenXml.Wordprocessing.RightBorder)
                            {
                                properties.BorderRightValue = (el as DocumentFormat.OpenXml.Wordprocessing.RightBorder).Val;
                                properties.BorderRightColor = (el as DocumentFormat.OpenXml.Wordprocessing.RightBorder).Color;
                                properties.BorderRightSize = (el as DocumentFormat.OpenXml.Wordprocessing.RightBorder).Size;
                                properties.BorderRightSpace = (el as DocumentFormat.OpenXml.Wordprocessing.RightBorder).Space;
                            }
                        }
                    }
                }
            }
            return properties;
        }

        /// <summary>
        /// Извлечение колонки таблицы
        /// </summary>
        /// <param name="order"></param>
        /// <param name="element"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static TicketTemplateTableGridColumn GetTableGrid(int order, DocumentFormat.OpenXml.Wordprocessing.GridColumn element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateTableGridColumn grid = new TicketTemplateTableGridColumn
            {
                TicketTemplateTableId = Id,
                Order = order,
                Width = element.Width
            };
            return grid;
        }

        /// <summary>
        /// Извлечение строки таблицы
        /// </summary>
        /// <param name="order"></param>
        /// <param name="element"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static TicketTemplateTableRow GetTableRow(int order, DocumentFormat.OpenXml.Wordprocessing.TableRow element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateTableRow row = new TicketTemplateTableRow
            {
                TicketTemplateTableId = Id,
                Order = order,
                TicketTemplateTableCells = new List<TicketTemplateTableCell>()
            };
            // свойства стоки
            row.TicketTemplateTableRowProperties = GetTableRowProperties(element as DocumentFormat.OpenXml.Wordprocessing.TableRow, row.Id);
            row.TicketTemplateTableRowPropertiesId = row.TicketTemplateTableRowProperties.Id;
            int orderCell = 0;
            foreach (var elem in element.ChildElements)
            {
                if(elem is DocumentFormat.OpenXml.Wordprocessing.TableCell)
                {
                    row.TicketTemplateTableCells.Add(GetTableCell(orderCell++, elem as DocumentFormat.OpenXml.Wordprocessing.TableCell, row.Id));
                }
            }
            return row;
        }

        /// <summary>
        /// Извлечение свойств строки таблицы
        /// </summary>
        /// <param name="element"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static TicketTemplateTableRowProperties GetTableRowProperties(DocumentFormat.OpenXml.Wordprocessing.TableRow element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateTableRowProperties properties = new TicketTemplateTableRowProperties { TicketTemplateTableRowId = Id };
            var elementProeprties = element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.TableRowProperties>()?.FirstOrDefault();
            if (elementProeprties != null)
            {
                // идем по вложенным элементам
                foreach (var elem in elementProeprties.ChildElements)
                {
                    if (elem is DocumentFormat.OpenXml.Wordprocessing.CantSplit)
                    {
                        properties.CantSplit = (elem as DocumentFormat.OpenXml.Wordprocessing.CantSplit).Val;
                    }
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.TableRowHeight)
                    {
                        properties.TableRowHeight = (elem as DocumentFormat.OpenXml.Wordprocessing.TableRowHeight).Val;
                    }

                }
            }
            return properties;
        }

        /// <summary>
        /// Извлечение ячейки строки таблицы
        /// </summary>
        /// <param name="order"></param>
        /// <param name="element"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static TicketTemplateTableCell GetTableCell(int order, DocumentFormat.OpenXml.Wordprocessing.TableCell element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateTableCell cell = new TicketTemplateTableCell
            {
                TicketTemplateTableRowId = Id,
                Order = order,
                TicketTemplateParagraphs = new List<TicketTemplateParagraph>()
            };
            // свойства стоки
            cell.TicketTemplateTableCellProperties = GetTableCellProperties(element as DocumentFormat.OpenXml.Wordprocessing.TableCell, cell.Id);
            cell.TicketTemplateTableCellPropertiesId = cell.TicketTemplateTableCellProperties.Id;
            int orderParagraph = 0;
            foreach (var elem in element.ChildElements)
            {
                if (elem is DocumentFormat.OpenXml.Wordprocessing.Paragraph)
                {
                    cell.TicketTemplateParagraphs.Add(GetParagraph(orderParagraph++, elem as DocumentFormat.OpenXml.Wordprocessing.Paragraph, null, cell.Id));
                }
            }
            return cell;
        }

        /// <summary>
        /// Извлечение свойств ячейки строки таблицы
        /// </summary>
        /// <param name="element"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static TicketTemplateTableCellProperties GetTableCellProperties(DocumentFormat.OpenXml.Wordprocessing.TableCell element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateTableCellProperties properties = new TicketTemplateTableCellProperties { TicketTemplateTableCellId = Id };
            var elementProeprties = element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.TableCellProperties>()?.FirstOrDefault();
            if (elementProeprties != null)
            {
                // идем по вложенным элементам
                foreach (var elem in elementProeprties.ChildElements)
                {
                    if (elem is DocumentFormat.OpenXml.Wordprocessing.TableCellWidth)
                    {
                        properties.TableCellWidth = (elem as DocumentFormat.OpenXml.Wordprocessing.TableCellWidth).Width;
                    }
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.GridSpan)
                    {
                        properties.GridSpan = (elem as DocumentFormat.OpenXml.Wordprocessing.GridSpan).Val;
                    }
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.VerticalMerge)
                    {
                        properties.VerticalMerge = (elem as DocumentFormat.OpenXml.Wordprocessing.VerticalMerge).Val ?? "continue";
                    }
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.Shading)
                    {
                        properties.ShadingValue = (elem as DocumentFormat.OpenXml.Wordprocessing.Shading).Val;
                        properties.ShadingColor = (elem as DocumentFormat.OpenXml.Wordprocessing.Shading).Color;
                        properties.ShadingFill = (elem as DocumentFormat.OpenXml.Wordprocessing.Shading).Fill;
                    }
                }
            }
            return properties;
        }

        /// <summary>
        /// Извлечение абзаца
        /// </summary>
        /// <param name="order"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private static TicketTemplateParagraph GetParagraph(int order, DocumentFormat.OpenXml.Wordprocessing.Paragraph element, Guid? BodyId, Guid? CellId)
        {
            if (element == null) { return null; }
            TicketTemplateParagraph paragraph = new TicketTemplateParagraph
            {
                TicketTemplateBodyId = BodyId,
                Order = order,
                TicketTemplateParagraphRuns = new List<TicketTemplateParagraphRun>(),
                TicketTemplateTableCellId = CellId
            };
            // свойства параграфа
            paragraph.TicketTemplateParagraphProperties = GetParagraphProperties(element as DocumentFormat.OpenXml.Wordprocessing.Paragraph, paragraph.Id);
            paragraph.TicketTemplateParagraphPropertiesId = paragraph.TicketTemplateParagraphProperties.Id;
            int orderRun = 0;
            // идем по его элементам
            foreach (var elem in element.ChildElements)
            {
                // строки параграфа
                if (elem is DocumentFormat.OpenXml.Wordprocessing.Run)
                {
                    var run = GetRun(elem as DocumentFormat.OpenXml.Wordprocessing.Run, paragraph.Id);
                    if (paragraph.TicketTemplateParagraphRuns.Count > 0)
                    {
                        var last = paragraph.TicketTemplateParagraphRuns[paragraph.TicketTemplateParagraphRuns.Count - 1];
                        bool flag = false;
                        if (run.TicketTemplateParagraphRunProperties != null && last.TicketTemplateParagraphRunProperties != null)
                        {
                            if (run.TicketTemplateParagraphRunProperties.RunBold != last.TicketTemplateParagraphRunProperties.RunBold)
                            {
                                flag = true;
                            }
                            else if (run.TicketTemplateParagraphRunProperties.RunItalic != last.TicketTemplateParagraphRunProperties.RunItalic)
                            {
                                flag = true;
                            }
                            else if (run.TicketTemplateParagraphRunProperties.RunUnderline != last.TicketTemplateParagraphRunProperties.RunUnderline)
                            {
                                flag = true;
                            }
                            else if (run.TicketTemplateParagraphRunProperties.RunSize != last.TicketTemplateParagraphRunProperties.RunSize)
                            {
                                flag = true;
                            }
                            else if (run.TabChar != last.TabChar)
                            {
                                flag = true;
                            }
                            else if (run.Break != last.Break)
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = true;
                        }

                        if (flag)
                        {
                            run.Order = orderRun++;
                            paragraph.TicketTemplateParagraphRuns.Add(run);
                        }
                        else
                        {
                            last.Text = string.Format("{0}{1}", last.Text, run.Text);
                        }
                    }
                    else
                    {
                        run.Order = orderRun++;
                        paragraph.TicketTemplateParagraphRuns.Add(run);
                    }
                }
            }

            return paragraph;
        }

        /// <summary>
        /// Извлечение свойств абзаца
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static TicketTemplateParagraphProperties GetParagraphProperties(DocumentFormat.OpenXml.Wordprocessing.Paragraph element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateParagraphProperties properties = new TicketTemplateParagraphProperties { TicketTemplateParagraphId = Id };
            var elementProeprties = element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties>()?.FirstOrDefault();
            if (elementProeprties != null)
            {
                // идем по вложенным элементам
                foreach (var elem in elementProeprties.ChildElements)
                {
                    // выравнивание
                    if (elem is DocumentFormat.OpenXml.Wordprocessing.Justification)
                    {
                        properties.Justification = (elem as DocumentFormat.OpenXml.Wordprocessing.Justification).Val;
                    }
                    // междустроками
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.SpacingBetweenLines)
                    {
                        properties.SpacingBetweenLinesLine = (elem as DocumentFormat.OpenXml.Wordprocessing.SpacingBetweenLines).Line;
                        properties.SpacingBetweenLinesLineRule = (elem as DocumentFormat.OpenXml.Wordprocessing.SpacingBetweenLines).LineRule;
                        properties.SpacingBetweenLinesBefore = (elem as DocumentFormat.OpenXml.Wordprocessing.SpacingBetweenLines).Before;
                        properties.SpacingBetweenLinesAfter = (elem as DocumentFormat.OpenXml.Wordprocessing.SpacingBetweenLines).After;
                    }
                    // отступы
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.Indentation)
                    {
                        properties.IndentationFirstLine = (elem as DocumentFormat.OpenXml.Wordprocessing.Indentation).FirstLine;
                        properties.IndentationHanging = (elem as DocumentFormat.OpenXml.Wordprocessing.Indentation).Hanging;
                        properties.IndentationLeft = (elem as DocumentFormat.OpenXml.Wordprocessing.Indentation).Left;
                        properties.IndentationRight = (elem as DocumentFormat.OpenXml.Wordprocessing.Indentation).Right;
                    }
                    // свойства текста
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.ParagraphMarkRunProperties)
                    {
                        foreach (var el in elem.ChildElements)
                        {
                            if (el is DocumentFormat.OpenXml.Wordprocessing.FontSize)
                            {
                                properties.RunSize = (el as DocumentFormat.OpenXml.Wordprocessing.FontSize).Val;
                            }
                            else if (el is DocumentFormat.OpenXml.Wordprocessing.Bold)
                            {
                                properties.RunBold = true;
                            }
                            else if (el is DocumentFormat.OpenXml.Wordprocessing.Italic)
                            {
                                properties.RunItalic = true;
                            }
                            else if (el is DocumentFormat.OpenXml.Wordprocessing.Underline)
                            {
                                properties.RunUnderline = true;
                            }
                        }
                    }
                }
            }
            return properties;
        }

        /// <summary>
        /// Извлечение строки
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static TicketTemplateParagraphRun GetRun(DocumentFormat.OpenXml.Wordprocessing.Run element, Guid Id)
        {
            if (element == null) { return null; }
            TicketTemplateParagraphRun run = new TicketTemplateParagraphRun { TicketTemplateParagraphId = Id };
            run.TicketTemplateParagraphRunProperties = GetRunProperties(element as DocumentFormat.OpenXml.Wordprocessing.Run, run.Id);
            run.TicketTemplateParagraphRunPropertiesId = run.TicketTemplateParagraphRunProperties.Id;
            foreach (var elem in element.ChildElements)
            {
                if (elem is DocumentFormat.OpenXml.Wordprocessing.Text)
                {
                    run.Text = (elem as DocumentFormat.OpenXml.Wordprocessing.Text).Text;
                }
                else if (elem is DocumentFormat.OpenXml.Wordprocessing.TabChar)
                {
                    run.TabChar = true;
                }
                else if (elem is DocumentFormat.OpenXml.Wordprocessing.Break)
                {
                    run.Break = true;
                }
            }
            return run;
        }

        /// <summary>
        /// Извлечение свойств строки
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static TicketTemplateParagraphRunProperties GetRunProperties(DocumentFormat.OpenXml.Wordprocessing.Run element, Guid Id)
        {
            if (element == null) { return null; }
            var elementProeprties = element.ChildElements.OfType<DocumentFormat.OpenXml.Wordprocessing.RunProperties>()?.FirstOrDefault();
            TicketTemplateParagraphRunProperties properties = new TicketTemplateParagraphRunProperties { TicketTemplateParagraphRunId = Id };
            if (elementProeprties != null)
            {
                foreach (var elem in elementProeprties.ChildElements)
                {
                    // свойства текста
                    if (elem is DocumentFormat.OpenXml.Wordprocessing.FontSize)
                    {
                        properties.RunSize = (elem as DocumentFormat.OpenXml.Wordprocessing.FontSize).Val;
                    }
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.Bold)
                    {
                        properties.RunBold = true;
                    }
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.Italic)
                    {
                        properties.RunItalic = true;
                    }
                    else if (elem is DocumentFormat.OpenXml.Wordprocessing.Underline)
                    {
                        properties.RunUnderline = true;
                    }
                }
            }
            return properties;
        }
    }
}