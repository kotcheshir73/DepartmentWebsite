using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using ExaminationInterfaces.ViewModels;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.Services
{
    public partial class ControlTicketTemplateLoader : UserControl
    {
        private Guid _examinationTemplateId;

        private readonly ITicketProcess _process;

        private string _fileName;

        public ControlTicketTemplateLoader(ITicketProcess process)
        {
            InitializeComponent();
            _process = process;
        }

        public Guid SetId { set { _examinationTemplateId = value; } }

        private void ButtonLoadTemplate_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "docx|*.docx"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _fileName = ofd.FileName;
                var template = _process.LoadTemplate(new TicketProcessLoadTemplateBindingModel
                {
                    FileName = ofd.FileName
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

        private void ButtonSaveTemplate_Click(object sender, EventArgs e)
        {
            var res = _process.SaveTemplate(new TicketProcessLoadTemplateBindingModel
            {
                TemplateName = "",
                ExaminationTemplateId = _examinationTemplateId,
                FileName = _fileName
            });
            if(res.Succeeded)
            {
                MessageBox.Show("Успешно");
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", res.Errors);
            }
        }
    }
}