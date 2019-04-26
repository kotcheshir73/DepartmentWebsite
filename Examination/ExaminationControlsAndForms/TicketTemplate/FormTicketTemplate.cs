using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.TicketTemplate
{
    public partial class FormTicketTemplate : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITicketTemplateService _service;

        private readonly ITicketProcess _process;

        public FormTicketTemplate(ITicketTemplateService service, IExaminationTemplateService _serviceET, ITicketProcess process, Guid? examinationTemplateId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            examinationTemplateElement.Service = _serviceET;
            examinationTemplateElement.Id = examinationTemplateId;

            if (id != Guid.Empty)
            {
                buttonLoadTemplate.Visible = labelLinkToFile.Visible = textBoxLinkToFile.Visible = false;
            }
        }

        protected override void LoadData()
        {
            var result = _service.GetTicketTemplate(new TicketTemplateGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxTemplateName.Text = entity.TemplateName;
            LoadBody(entity.Body);
        }

        /// <summary>
        /// Загрузка шаблона билета
        /// </summary>
        /// <param name="body"></param>
        private void LoadBody(TicketProcessBodyViewModel body)
        {
            var steps = (body.Tables?.Count ?? 0) + (body.Paragraphs?.Count ?? 0);
            StringBuilder html = new StringBuilder("<body>");
            string size = "";
            for (int i = 0; i < steps; ++i)
            {
                html.Append(ProcessTable(body.Tables?.FirstOrDefault(x => x.Order == i), ref size));
                html.Append(ProcessParagraph(body.Paragraphs?.FirstOrDefault(x => x.Order == i), ref size));
            }
            webBrowser.DocumentText = html.ToString();
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
                            cellCompile.Append(ProcessParagraph(paragraph, ref size));
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

        /// <summary>
        /// Обработка параграфа
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private string ProcessParagraph(TicketProcessParagraphViewModel paragraph, ref string size)
        {
            if (paragraph != null && paragraph.ParagraphDatas != null)
            {
                string aligment = "";
                if (paragraph.ParagraphFormat != null)
                {
                    var attrAlign = paragraph.ParagraphFormat?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:jc")?
                        .ElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val");
                    if (attrAlign != null)
                    {
                        aligment = $" align={attrAlign.Value}";
                    }
                    var elemSize = paragraph.ParagraphFormat?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:rPr")?
                        .ChildElementaryUnits?.FirstOrDefault(x => x.Name.StartsWith("w:sz"))?.ElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val");
                    if (elemSize != null)
                    {
                        size = $" style=\"font-size:{elemSize.Value}pt \"";
                    }
                }
                StringBuilder pdata = new StringBuilder($"<p{aligment}{size}>");
                foreach (var data in paragraph.ParagraphDatas)
                {
                    string text = data.Text;
                    var brElem = data.ElementaryUnits?.FirstOrDefault(x => x.Name == "w:br");
                    if (brElem != null)
                    {
                        text = $"{text}<br />";
                    }
                    var bold = data.Font?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:b");
                    if (bold != null)
                    {
                        text = $"<b>{text}</b>";
                    }
                    var underline = data.Font?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:u");
                    if (underline != null)
                    {
                        text = $"<u>{text}</u>";
                    }
                    var italic = data.Font?.ChildElementaryUnits?.FirstOrDefault(x => x.Name == "w:i");
                    if (italic != null)
                    {
                        text = $"<i>{text}</i>";
                    }
                    var rSize = data.Font?.ChildElementaryUnits?.FirstOrDefault(x => x.Name.StartsWith("w:sz"))?
                        .ElementaryAttributes?.FirstOrDefault(x => x.Name == "w:val");
                    if (rSize != null)
                    {
                        text = $"<span style=\"font-size:{rSize.Value}pt \">{text}</span>";
                    }
                    else if (!string.IsNullOrEmpty(size))
                    {
                        text = $"<span style=\"font-size:{size}pt \">{text}</span>";
                    }
                    pdata.Append(text);
                }
                return pdata.ToString();
            }

            return string.Empty;
        }

        protected override bool CheckFill()
        {
            labelLinkToFile.ForeColor =
            labelTemplateName.ForeColor =
                SystemColors.ControlText;
            if (textBoxLinkToFile.Visible)
            {
                if (string.IsNullOrEmpty(textBoxLinkToFile.Text))
                {
                    labelLinkToFile.ForeColor = Color.Red;
                    return false;
                }
            }
            if (string.IsNullOrEmpty(textBoxTemplateName.Text))
            {
                labelTemplateName.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _process.SaveTemplate(new TicketProcessLoadTemplateBindingModel
                {
                    ExaminationTemplateId = examinationTemplateElement.Id.Value,
                    TemplateName = textBoxTemplateName.Text,
                    FileName = textBoxLinkToFile.Text
                });
            }
            else
            {
                result = _service.UpdateTicketTemplate(new TicketTemplateSetBindingModel
                {
                    Id = _id.Value,
                    ExaminationTemplateId = examinationTemplateElement.Id.Value,
                    TemplateName = textBoxTemplateName.Text
                });
            }
            if (result.Succeeded)
            {
                if (result.Result != null)
                {
                    if (result.Result is Guid)
                    {
                        _id = (Guid)result.Result;
                    }
                }
                return true;
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
        }

        private void ButtonLoadTemplate_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxLinkToFile.Text = ofd.FileName;
                var template = _process.LoadTemplate(new TicketProcessLoadTemplateBindingModel
                {
                    FileName = ofd.FileName,
                    TemplateName = textBoxTemplateName.Text
                });
                if (template.Succeeded)
                {
                    var result = template.Result;
                    if (result.Body != null)
                    {
                        LoadBody(result.Body);
                    }
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", template.Errors);
                }
            }
        }
    }
}