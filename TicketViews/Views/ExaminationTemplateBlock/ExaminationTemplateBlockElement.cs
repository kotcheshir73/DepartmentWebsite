using System;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;

namespace TicketViews.Views.ExaminationTemplateBlock
{
    public partial class ExaminationTemplateBlockElement : UserControl
    {
        private IExaminationTemplateBlockService _service;

        public IExaminationTemplateBlockService Service { set { _service = value; } }

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

        public ExaminationTemplateBlockElement()
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
                var result = _service.GetExaminationTemplateBlock(new ExaminationTemplateBlockGetBindingModel { Id = standartElementControl.Id.Value });
                if (result.Succeeded)
                {
                    standartElementControl.textBox.Text = string.Format("{0}", result.Result.BlockName);
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
            var searchForm = new ExaminationTemplateBlockSearchForm(_service, examinationTemplateId: _examinationTemplateId);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                standartElementControl.Id = searchForm.SelectedId;
            }
        }
    }
}