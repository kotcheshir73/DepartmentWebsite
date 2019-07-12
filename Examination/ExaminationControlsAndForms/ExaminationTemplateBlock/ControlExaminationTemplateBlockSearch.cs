using ControlsAndForms.Messangers;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Windows.Forms;

namespace ExaminationControlsAndForms.ExaminationTemplateBlock
{
    public partial class ControlExaminationTemplateBlockSearch : UserControl
    {
        private IExaminationTemplateBlockService _service;

        public IExaminationTemplateBlockService Service
        {
            set { _service = value; }
            get { return _service; }
        }

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

        public ControlExaminationTemplateBlockSearch()
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
            var searchForm = new FormExaminationTemplateBlockSearch(_service, examinationTemplateId: _examinationTemplateId);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                standartElementControl.Id = searchForm.SelectedId;
            }
        }
    }
}