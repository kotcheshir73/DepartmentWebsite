using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.ExaminationTemplateTicketQuestion
{
    public partial class FormExaminationTemplateTicketQuestion : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private IExaminationTemplateTicketQuestionService _service;

        public FormExaminationTemplateTicketQuestion(IExaminationTemplateTicketQuestionService service, IExaminationTemplateTicketService serviceT, 
            IExaminationTemplateBlockQuestionService serviceBQ, Guid examinationTemplateTicketId, Guid examinationTemplateId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateBlockQuestionElement.Service = serviceBQ;
            examinationTemplateBlockQuestionElement.ExaminationTemplateId = examinationTemplateId;
            examinationTemplateTicketElement.Service = serviceT;
            examinationTemplateTicketElement.Id = examinationTemplateTicketId;
        }

        private void FormExaminationTemplateTicketQuestion_Load(object sender, EventArgs e)
        {
            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetExaminationTemplateTicketQuestion(new ExaminationTemplateTicketQuestionGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            examinationTemplateBlockQuestionElement.Id = entity.ExaminationTemplateBlockQuestionId;
            numericUpDownOrder.Value = entity.Order;
        }

        private bool CheckFill()
        {
            labelExaminationTemplateBlockQuestion.ForeColor = SystemColors.ControlText;
            if (examinationTemplateBlockQuestionElement.Id == null)
            {
                labelExaminationTemplateBlockQuestion.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateExaminationTemplateTicketQuestion(new ExaminationTemplateTicketQuestionSetBindingModel
                    {
                        ExaminationTemplateTicketId = examinationTemplateTicketElement.Id.Value,
                        ExaminationTemplateBlockQuestionId = examinationTemplateBlockQuestionElement.Id.Value,
                        Order = (int)numericUpDownOrder.Value
                    });
                }
                else
                {
                    result = _service.UpdateExaminationTemplateTicketQuestion(new ExaminationTemplateTicketQuestionSetBindingModel
                    {
                        Id = _id.Value,
                        ExaminationTemplateTicketId = examinationTemplateTicketElement.Id.Value,
                        ExaminationTemplateBlockQuestionId = examinationTemplateBlockQuestionElement.Id.Value,
                        Order = (int)numericUpDownOrder.Value
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
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}