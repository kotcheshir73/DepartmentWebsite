using System;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;

namespace TicketViews.Views.ExaminationTemplateBlockQuestion
{
    public partial class ExaminationTemplateBlockQuestionElement : UserControl
    {
        private IExaminationTemplateBlockQuestionService _service;

        public IExaminationTemplateBlockQuestionService Service { set { _service = value; } }

        private Guid? _examinationTemplateBlockId;

        public Guid? ExaminationTemplateBlockId
        {
            get { return _examinationTemplateBlockId; }
            set { _examinationTemplateBlockId = value; }
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

        public ExaminationTemplateBlockQuestionElement()
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
                var result = _service.GetExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionGetBindingModel { Id = standartElementControl.Id.Value });
                if (result.Succeeded)
                {
                    standartElementControl.textBox.Text = string.Format("{0}. {1}", result.Result.QuestionNumber, result.Result.QuestionText);
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
            var searchForm = new ExaminationTemplateBlockQuestionSearchForm(_service, examinationTemplateBlockId: _examinationTemplateBlockId, examinationTemplateId: _examinationTemplateId);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                standartElementControl.Id = searchForm.SelectedId;
            }
        }
    }
}