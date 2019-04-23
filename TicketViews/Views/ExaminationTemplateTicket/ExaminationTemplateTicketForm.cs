using DepartmentModel;
using System;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Views.ExaminationTemplateTicketQuestion;
using Unity;
using Unity.Attributes;

namespace TicketViews.Views.ExaminationTemplateTicket
{
    public partial class ExaminationTemplateTicketForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateTicketService _service;

        private Guid? _id = null;

        public ExaminationTemplateTicketForm(IExaminationTemplateTicketService service, IExaminationTemplateService serviceET, Guid? examinationTemplateId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateElement.Service = serviceET;
            examinationTemplateElement.Id = examinationTemplateId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void ExaminationTemplateTicketForm_Load(object sender, EventArgs e)
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
                var control = Container.Resolve<ExaminationTemplateTicketQuestionControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ExaminationTemplateTicketQuestionControl).LoadData(_id.Value, examinationTemplateElement.Id.Value);

            var result = _service.GetExaminationTemplateTicket(new ExaminationTemplateTicketGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            numericUpDownTicketNumber.Value = entity.TicketNumber;
        }

        private bool CheckFill()
        {
            return true;
        }

        private bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateExaminationTemplateTicket(new ExaminationTemplateTicketSetBindingModel
                    {
                        ExaminationTemplateId = examinationTemplateElement.Id.Value,
                        TicketNumber = (int)numericUpDownTicketNumber.Value
                    });
                }
                else
                {
                    result = _service.UpdateExaminationTemplateTicket(new ExaminationTemplateTicketSetBindingModel
                    {
                        Id = _id.Value,
                        ExaminationTemplateId = examinationTemplateElement.Id.Value,
                        TicketNumber = (int)numericUpDownTicketNumber.Value
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