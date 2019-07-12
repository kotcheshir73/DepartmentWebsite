using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.TicketTemplate
{
    public partial class ControlTicketTemplateSearch : UserControl
    {
        private ITicketTemplateService _service;

        public ITicketTemplateService Service { set { _service = value; } }

        public Guid? Id
        {
            get { return standartElementControl.Id; }
            set { standartElementControl.Id = value; }
        }

        public ControlTicketTemplateSearch()
        {
            InitializeComponent();
            standartElementControl.OnSetIdAddEvent(SetValueyId);
            standartElementControl.ButtonSelectClickAddEvent(Select_Click);
        }

        private void SetValueyId()
        {
            if (standartElementControl.Id.HasValue)
            {
                if (_service == null)
                {
                    throw new Exception("Неизвестен сервер");
                }
                var result = _service.GetTicketTemplate(new TicketTemplateGetBindingModel { Id = standartElementControl.Id.Value });
                if (result.Succeeded)
                {
                    standartElementControl.textBox.Text = result.Result.TemplateName;
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("Ошибка при получении билета", result.Errors);
                }
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            if (_service == null)
            {
                throw new Exception("Неизвестен сервер");
            }
            var searchForm = new FormTicketTemplateSearch(_service);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                standartElementControl.Id = searchForm.SelectedId;
            }
        }
    }
}