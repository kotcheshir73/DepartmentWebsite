using System;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;

namespace TicketViews.Views.ExaminationTemplateTicket
{
    public partial class ExaminationTemplateTicketElement : UserControl
    {
        private IExaminationTemplateTicketService _service;

        public IExaminationTemplateTicketService Service { set { _service = value; } }

        private Guid? _examinationTemplateId;

        public Guid? ExaminationTemplateId
        {
            get { return _examinationTemplateId; }
            set { _examinationTemplateId = value; }
        }

        public Guid? Id
        {
            get { return standartElementControl.Id; }
            set { standartElementControl.Id = value; }
        }

        public ExaminationTemplateTicketElement()
        {
            InitializeComponent();
            standartElementControl.OnSetIdAddEvent(SetValueyId);
            standartElementControl.ButtonSelectClickAddEvent(Select_Click);
        }

        private void SetValueyId()
        {
            if(standartElementControl.Id.HasValue)
            {
                if (_service == null)
                {
                    throw new Exception("Неизвестен сервер");
                }
                var result = _service.GetExaminationTemplateTicket(new ExaminationTemplateTicketGetBindingModel { Id = standartElementControl.Id.Value });
                if(result.Succeeded)
                {
                    standartElementControl.textBox.Text = string.Format("Билет №{0}", result.Result.TicketNumber);
                }
                else
                {
                    Program.PrintErrorMessage("Ошибка при получении билета", result.Errors);
                }
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            if (_service == null)
            {
                throw new Exception("Неизвестен сервер");
            }
            if (!_examinationTemplateId.HasValue)
            {
                throw new Exception("Неизвестен экзамен");
            }
            var searchForm = new ExaminationTemplateTicketSearchForm(_service, _examinationTemplateId.Value);
            if(searchForm.ShowDialog() == DialogResult.OK)
            {
                standartElementControl.Id = searchForm.SelectedId;
            }
        }
    }
}