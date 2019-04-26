using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ExaminationControlsAndForms.ExaminationTemplateBlockQuestion;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.ExaminationTemplateBlock
{
    public partial class FormExaminationTemplateBlock : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateBlockService _service;

        public FormExaminationTemplateBlock(IExaminationTemplateBlockService service, IExaminationTemplateService _serviceET, Guid? examinationTemplateId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateElement.Service = _serviceET;
            examinationTemplateElement.Id = examinationTemplateId;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlExaminationTemplateBlockQuestion>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlExaminationTemplateBlockQuestion).LoadData(_id.Value, examinationTemplateElement.Id.Value);

            var result = _service.GetExaminationTemplateBlock(new ExaminationTemplateBlockGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxBlockName.Text = entity.BlockName;
            textBoxQuestionTagInTemplate.Text = entity.QuestionTagInTemplate;
            numericUpDownCountQuestionInTicket.Value = entity.CountQuestionInTicket;
            checkBoxIsCombine.Checked = entity.IsCombine;
            textBoxCombineBlocks.Text = entity.CombineBlocks;
        }

        protected override bool CheckFill()
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

        protected override bool Save()
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
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
        }
    }
}