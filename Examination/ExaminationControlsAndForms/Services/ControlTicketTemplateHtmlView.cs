using ExaminationInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.Services
{
    public partial class ControlTicketTemplateHtmlView : UserControl
    {
        public ControlTicketTemplateHtmlView()
        {
            InitializeComponent();
        }

        public void LoadView(TicketTemplateViewModel model)
        {
            if (model.Body != null)
            {
                LoadBody(model.Body);
            }
        }

        /// <summary>
        /// Загрузка шаблона билета
        /// </summary>
        /// <param name="body"></param>
        private void LoadBody(TicketTemplateBodyViewModel body)
        {
            var steps = /*(body.Tables?.Count ?? 0) + */(body.TicketTemplateParagraphPageViewModel?.List.Count ?? 0);
            StringBuilder html = new StringBuilder("<body>");
            string size = "";
            for (int i = 0; i < steps; ++i)
            {
                // html.Append(ProcessTable(body.Tables?.FirstOrDefault(x => x.Order == i), ref size));
                html.Append(LoadParagraph(body.TicketTemplateParagraphPageViewModel?.List.FirstOrDefault(x => x.Order == i), ref size));
            }
            webBrowser.DocumentText = html.ToString();
        }

        /// <summary>
        /// Обработка параграфа
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private string LoadParagraph(TicketTemplateParagraphViewModel paragraph, ref string size)
        {
            string aligment = "";
            if (paragraph.TicketTemplateParagraphPropertiesViewModel != null)
            {
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphPropertiesViewModel.Justification))
                {
                    aligment = $" align={paragraph.TicketTemplateParagraphPropertiesViewModel.Justification}";
                }
                if (!string.IsNullOrEmpty(paragraph.TicketTemplateParagraphPropertiesViewModel.RunSize))
                {
                    size = $" style=\"font-size:{paragraph.TicketTemplateParagraphPropertiesViewModel.RunSize}pt \"";
                }
            }
            StringBuilder paragraphBuilder = new StringBuilder($"<p{aligment}{size}>");
            if (paragraph.TicketTemplateParagraphRunPageViewModel != null)
            {
                foreach (var elem in paragraph.TicketTemplateParagraphRunPageViewModel.List)
                {
                    string text = elem.Text;
                    if (elem.TicketTemplateParagraphRunPropertiesViewModel != null)
                    {
                        if (elem.TicketTemplateParagraphRunPropertiesViewModel.RunBold)
                        {
                            text = $"<b>{text}</b>";
                        }
                        if (elem.TicketTemplateParagraphRunPropertiesViewModel.RunItalic)
                        {
                            text = $"<i>{text}</i>";
                        }
                        if (elem.TicketTemplateParagraphRunPropertiesViewModel.RunUnderline)
                        {
                            text = $"<u>{text}</u>";
                        }
                        if (!string.IsNullOrEmpty(elem.TicketTemplateParagraphRunPropertiesViewModel.RunSize))
                        {
                            text = $"<span style=\"font-size:{elem.TicketTemplateParagraphRunPropertiesViewModel.RunSize}pt \">{text}</span>";
                        }
                        else if (!string.IsNullOrEmpty(size))
                        {
                            text = $"<span style=\"font-size:{size}pt \">{text}</span>";
                        }
                    }
                    paragraphBuilder.Append(text);
                }
            }
            return paragraphBuilder.ToString();
        }

        /// <summary>
        /// Обработка таблицы
        /// </summary>
        /// <param name="table"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private string ProcessTable(TicketProcessTableViewModel table, ref string size)
        {
            if (table != null && table.TableRows != null)
            {
                StringBuilder tableRes = new StringBuilder();
                List<double> percentWidth = new List<double>();
                var totalWidth = table.Columns?.ChildElementaryUnits?.Sum(x => Convert.ToInt32(x.ElementaryAttributes?.FirstOrDefault(a => a.Name == "w:w")?.Value ?? "0")) ?? 0;
                if (totalWidth == 0)
                {
                    return string.Empty;
                }
                foreach (var elem in table.Columns.ChildElementaryUnits)
                {
                    percentWidth.Add(Convert.ToDouble(elem.ElementaryAttributes?.FirstOrDefault(a => a.Name == "w:w")?.Value ?? "0") / totalWidth);
                }
                for (int i = 0; i < table.TableRows.Count; ++i)
                {
                    int countCells = 0;
                    StringBuilder rowCompile = new StringBuilder();
                    foreach (var cell in table.TableRows[i].TableCells)
                    {
                        var colMerge = cell.Properties?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:gridSpan");
                        string colSpan = "";
                        if (colMerge != null)
                        {
                            // объединение по строкам
                            colSpan = $" colspan=\"{colMerge.ElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val")?.Value}\"";
                        }
                        // проверка на объединение по строкам
                        var rowMerge = cell.Properties?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:vmerge");
                        string rowSpan = "";
                        if (rowMerge != null)
                        {
                            if (rowMerge.ElementaryAttributes != null && rowMerge.ElementaryAttributes.Count > 0)
                            {
                                int countRows = 1;
                                // если это первая строка при объединение, ищем, как далеко вниз она идет
                                for (int j = i + 1; j < table.TableRows.Count; ++j)
                                {
                                    int countSecCells = 0;
                                    // идем дло нужной ячейки
                                    foreach (var cellSec in table.TableRows[j].TableCells)
                                    {
                                        if (countCells == countSecCells)
                                        {
                                            var rowSecMerge = cellSec.Properties?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:vmerge");
                                            if (rowSecMerge == null)
                                            {
                                                // если у ячейки по нужной позиции нет признака объежинения, то прерываем
                                                j = table.TableRows.Count;
                                            }
                                            else
                                            {
                                                if (rowSecMerge.ElementaryAttributes != null && rowSecMerge.ElementaryAttributes.Count > 0)
                                                {
                                                    // началось другое объединение, прерываем это
                                                    j = table.TableRows.Count;
                                                }
                                                else
                                                {
                                                    countRows++;
                                                }
                                            }
                                            break;
                                        }
                                        var colSecMerge = cell.Properties?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:gridSpan");
                                        if (colSecMerge != null)
                                        {
                                            // либо несколько ячеек объединенных
                                            countSecCells += Convert.ToInt32(colSecMerge.ElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val")?.Value ?? "0");
                                        }
                                        else
                                        {
                                            // либо одна
                                            countSecCells++;
                                        }
                                    }
                                }
                                rowSpan = $" rowspan=\"{countRows}\"";
                            }
                            else
                            {
                                // иначе, игнорируем строку
                                continue;
                            }
                        }
                        string width = colMerge == null ? $" width=\"{percentWidth[countCells]}%\"" : "";

                        StringBuilder cellCompile = new StringBuilder();
                        foreach (var paragraph in cell.Paragraphs)
                        {
                            //  cellCompile.Append(ProcessParagraph(paragraph, ref size));
                        }

                        rowCompile.Append($"<td{rowSpan}{colSpan}{width}>{cellCompile.ToString()}</td>");
                        if (colMerge != null)
                        {
                            countCells += Convert.ToInt32(colMerge.ElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val")?.Value ?? "0");
                        }
                        else
                        {
                            countCells++;
                        }
                    }
                    tableRes.Append($"<tr>{rowCompile.ToString()}</tr>");
                }

                return $"<table width=\"100%\" border=\"1px solid black\">{tableRes.ToString()}</table>";
            }
            return string.Empty;
        }
    }
}