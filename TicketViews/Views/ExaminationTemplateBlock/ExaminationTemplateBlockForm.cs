using DepartmentModel;
using System;
using System.Drawing;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Views.ExaminationTemplateBlockQuestion;
using Unity;
using Unity.Attributes;

namespace TicketViews.Views.ExaminationTemplateBlock
{
    public partial class ExaminationTemplateBlockForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateBlockService _service;

        private Guid? _id = null;

        public ExaminationTemplateBlockForm(IExaminationTemplateBlockService service, IExaminationTemplateService _serviceET, Guid? examinationTemplateId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateElement.Service = _serviceET;
            examinationTemplateElement.Id = examinationTemplateId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void ExaminationTemplateBlockForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ExaminationTemplateBlockQuestionControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ExaminationTemplateBlockQuestionControl).LoadData(_id.Value, examinationTemplateElement.Id.Value);

            var result = _service.GetExaminationTemplateBlock(new ExaminationTemplateBlockGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxBlockName.Text = entity.BlockName;
            textBoxQuestionTagInTemplate.Text = entity.QuestionTagInTemplate;
            numericUpDownCountQuestionInTicket.Value = entity.CountQuestionInTicket;
            checkBoxIsCombine.Checked = entity.IsCombine;
            textBoxCombineBlocks.Text = entity.CombineBlocks;
        }

        private bool CheckFill()
        {
            labelBlockName.ForeColor = 
            labelQuestionTagInTemplate.ForeColor =
                SystemColors.ControlText;
            if (string.IsNullOrEmpty(textBoxBlockName.Text))
            {
                labelBlockName.ForeColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(textBoxQuestionTagInTemplate.Text))
            {
                labelQuestionTagInTemplate.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        private bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateExaminationTemplateBlock(new ExaminationTemplateBlockSetBindingModel
                    {
                        ExaminationTemplateId = examinationTemplateElement.Id.Value,
                        BlockName = textBoxBlockName.Text,
                        QuestionTagInTemplate = textBoxQuestionTagInTemplate.Text,
                        CountQuestionInTicket = (int)numericUpDownCountQuestionInTicket.Value,
                        IsCombine = checkBoxIsCombine.Checked,
                        CombineBlocks = textBoxCombineBlocks.Text
                    });
                }
                else
                {
                    result = _service.UpdateExaminationTemplateBlock(new ExaminationTemplateBlockSetBindingModel
                    {
                        Id = _id.Value,
                        ExaminationTemplateId = examinationTemplateElement.Id.Value,
                        BlockName = textBoxBlockName.Text,
                        QuestionTagInTemplate = textBoxQuestionTagInTemplate.Text,
                        CountQuestionInTicket = (int)numericUpDownCountQuestionInTicket.Value,
                        IsCombine = checkBoxIsCombine.Checked,
                        CombineBlocks = textBoxCombineBlocks.Text
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
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void ButtonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}