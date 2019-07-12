using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ExaminationControlsAndForms.ExaminationTemplateTicketQuestion;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace ExaminationControlsAndForms.ExaminationTemplateTicket
{
    public partial class FormExaminationTemplateTicket : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateTicketService _service;

        public FormExaminationTemplateTicket(IExaminationTemplateTicketService service, IExaminationTemplateService serviceET, Guid? examinationTemplateId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            examinationTemplateElement.Service = serviceET;
            examinationTemplateElement.Id = examinationTemplateId;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlExaminationTemplateTicketQuestion>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlExaminationTemplateTicketQuestion).LoadData(_id.Value, examinationTemplateElement.Id.Value);

            var result = _service.GetExaminationTemplateTicket(new ExaminationTemplateTicketGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            numericUpDownTicketNumber.Value = entity.TicketNumber;
        }

        protected override bool Save()
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
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
        }
    }
}