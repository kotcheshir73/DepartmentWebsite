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
                    TicketTemplateParagraphs = new List<TicketTemplateParagraph>()
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