using System;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;

namespace TicketViews.Views.ExaminationTemplate
{
    public partial class ExaminationTemplateElement : UserControl
    {
        private IExaminationTemplateService _service;

        public IExaminationTemplateService Service { set { _service = value; } }

        private Guid? _disciplineId;

        public Guid? DisciplineId
        {
            get { return _disciplineId; }
            set { _disciplineId = value; }
        }

        public Guid? Id
        {
            get { return standartElementControl.Id; }
            set { standartElementControl.Id = value; }
        }

        public ExaminationTemplateElement()
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
                var result = _service.GetExaminationTemplate(new ExaminationTemplateGetBindingModel { Id = standartElementControl.Id.Value });
                if (result.Succeeded)
                {
                    standartElementControl.textBox.Text = string.Format("{0}", result.Result.ExaminationTemplateName);
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
            var searchForm = new ExaminationTemplateSearchForm(_service, _disciplineId);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                standartElementControl.Id = searchForm.SelectedId;
            }
        }
    }
}