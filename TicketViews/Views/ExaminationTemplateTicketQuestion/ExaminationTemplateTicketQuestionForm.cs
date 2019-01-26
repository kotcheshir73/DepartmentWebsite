using DepartmentModel;
using System;
using System.Drawing;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using Unity;
using Unity.Attributes;

namespace TicketViews.Views.ExaminationTemplateTicketQuestion
{
    public partial class ExaminationTemplateTicketQuestionForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private IExaminationTemplateTicketQuestionService _service;

        private Guid? _id = null;

        public ExaminationTemplateTicketQuestionForm(IExaminationTemplateTicketQuestionService service, IExaminationTemplateTicketService serviceT, 
            IExaminationTemplateBlockQuestionService serviceBQ, Guid examinationTemplateTicketId, Guid examinationTemplateId, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateBlockQuestionElement.Service = serviceBQ;
            examinationTemplateBlockQuestionElement.ExaminationTemplateId = examinationTemplateId;
            examinationTemplateTicketElement.Service = serviceT;
            examinationTemplateTicketElement.Id = examinationTemplateTicketId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void ExaminationTemplateTicketQuestionForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetExaminationTemplateTicketQuestion(new ExaminationTemplateTicketQuestionGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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

        private bool Save()
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