using System;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;

namespace TicketViews.Views.TicketTemplate
{
    public partial class TicketTemplateElement : UserControl
    {
        private ITicketTemplateService _service;

        public ITicketTemplateService Service { set { _service = value; } }

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

        public TicketTemplateElement()
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
                    standartElementControl.textBox.Text = string.Format("Билет №{0}", result.Result.TemplateName);
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
            var searchForm = new TicketTemplateSearchForm(_service, _examinationTemplateId.Value);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                standartElementControl.Id = searchForm.SelectedId;
            }
        }
    }
}