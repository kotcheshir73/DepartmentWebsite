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
            var steps = (body.TicketTemplateTablePageViewModel?.List.Count ?? 0) + (body.TicketTemplateParagraphPageViewModel?.List.Count ?? 0);
            StringBuilder html = new StringBuilder("<body>");
            string size = "";
            for (int i = 0; i < steps; ++i)
            {
                html.Append(LoadParagraph(body.TicketTemplateParagraphPageViewModel?.List.FirstOrDefault(x => x.Order == i), ref size));
                html.Append(LoadTable(body.TicketTemplateTablePageViewModel?.List.FirstOrDefault(x => x.Order == i), ref size));
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
            if(paragraph == null)
            {
                return string.Empty;
            }

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
                    if (elem.TabChar)
                    {
                        paragraphBuilder.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    else if (elem.Break)
                    {
                        paragraphBuilder.Append("<br />");
                    }
                    else
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
            }
            return paragraphBuilder.ToString();
        }

        /// <summary>
        /// Обработка таблицы
        /// </summary>
        /// <param name="table"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private string LoadTable(TicketTemplateTableViewModel table, ref string size)
        {
            if (table == null)
            {
                return string.Empty;
            }

            if (table.TicketTemplateTableRowPageViewModel != null)
            {
                StringBuilder tableRes = new StringBuilder();
                List<double> percentWidth = new List<double>();
                var totalWidth = table.TicketTemplateTableGridColumnPageViewModel?.List.Sum(x => Convert.ToInt32(x.Width)) ?? 0;
                if (totalWidth == 0)
                {
                    return string.Empty;
                }
                foreach (var elem in table.TicketTemplateTableGridColumnPageViewModel?.List)
                {
                    percentWidth.Add(Convert.ToDouble(elem?.Width ?? "0") / totalWidth);
                }
                for (int i = 0; i < table.TicketTemplateTableRowPageViewModel.List.Count; ++i)
                {
                    int cellIndex = 0;
                    StringBuilder rowCompile = new StringBuilder();
                    foreach (var cell in table.TicketTemplateTableRowPageViewModel.List[i].TicketTemplateTableCellPageViewModel.List)
                    {
                        var colMerge = cell.TicketTemplateTableCellPropertiesViewModel?.GridSpan ?? string.Empty;
                        string colSpan = "";
                        if (!string.IsNullOrEmpty(colMerge))
                        {
                            // объединение по строкам
                            colSpan = $" colspan=\"{colMerge}\"";
                        }
                        // проверка на объединение по строкам
                        var rowMerge = cell.TicketTemplateTableCellPropertiesViewModel?.VerticalMerge ?? string.Empty;
                        string rowSpan = "";
                        if (!string.IsNullOrEmpty(rowMerge))
                        {
                            if (rowMerge == "restart")
                            {
                                int countRows = 1;
                                // если это первая строка при объединение, ищем, как далеко вниз она идет
                                for (int j = i + 1; j < table.TicketTemplateTableRowPageViewModel.List.Count; ++j)
                                {
                                    int countSecCells = 0;
                                    // идем дло нужной ячейки
                                    foreach (var cellSec in table.TicketTemplateTableRowPageViewModel.List[j].TicketTemplateTableCellPageViewModel.List)
                                    {
                                        if (cellIndex == countSecCells)
                                        {
                                            var rowSecMerge = cellSec.TicketTemplateTableCellPropertiesViewModel?.VerticalMerge;
                                            if (string.IsNullOrEmpty(rowSecMerge))
                                            {
                                                // если у ячейки по нужной позиции нет признака объединения, то прерываем
                                                j = table.TicketTemplateTableRowPageViewModel.List.Count;
                                            }
                                            else
                                            {
                                                if (rowSecMerge != "continue")
                                                {
                                                    // началось другое объединение, прерываем это
                                                    j = table.TicketTemplateTableRowPageViewModel.List.Count;
                                                }
                                                else
                                                {
                                                    countRows++;
                                                }
                                            }
                                            break;
                                        }
                                        var colSecMerge = cell.TicketTemplateTableCellPropertiesViewModel?.GridSpan;
                                        if (!string.IsNullOrEmpty(colSecMerge))
                                        {
                                            // либо несколько ячеек объединенных
                                            countSecCells += Convert.ToInt32(colSecMerge);
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
                            else if (rowMerge == "continue")
                            {
                                continue;
                            }
                        }
                        string width = colMerge == null ? $" width=\"{percentWidth[cellIndex]}%\"" : "";

                        StringBuilder cellCompile = new StringBuilder();
                        foreach (var paragraph in cell.TicketTemplateParagraphPageViewModel.List)
                        {
                              cellCompile.Append(LoadParagraph(paragraph, ref size));
                        }

                        rowCompile.Append($"<td{rowSpan}{colSpan}{width}>{cellCompile.ToString()}</td>");
                        if (!string.IsNullOrEmpty(colMerge))
                        {
                            cellIndex += Convert.ToInt32(colMerge);
                        }
                        else
                        {
                            cellIndex++;
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