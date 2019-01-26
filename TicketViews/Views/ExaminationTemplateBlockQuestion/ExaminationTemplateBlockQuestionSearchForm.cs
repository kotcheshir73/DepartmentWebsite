using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;

namespace TicketViews.Views.ExaminationTemplateBlockQuestion
{
    public partial class ExaminationTemplateBlockQuestionSearchForm : Form
    {
        private IExaminationTemplateBlockQuestionService _service;

        private Guid? _examinationTemplateBlockId;

        private Guid? _examinationTemplateId;

        private Guid _selectedId;

        public Guid SelectedId { get { return _selectedId; } }

        public ExaminationTemplateBlockQuestionSearchForm(IExaminationTemplateBlockQuestionService service, Guid? examinationTemplateBlockId = null, Guid? examinationTemplateId = null)
        {
            InitializeComponent();
            _service = service;
            _examinationTemplateBlockId = examinationTemplateBlockId;
            _examinationTemplateId = examinationTemplateId;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "ExaminationTemplateBlockName", Title = "Название блока", Width = 200, Visible = true },
                new ColumnConfig { Name = "QuestionNumber", Title = "Номер вопроса", Width = 150, Visible = true },
                new ColumnConfig { Name = "QuestionText", Title = "Текст вопроса", Width = 250, Visible = true }
            };

            standartSearchControl.Configurate(columns);
            standartSearchControl.SelectElementAddEvent((Guid Id) => { _selectedId = Id; DialogResult = DialogResult.OK; Close(); });
            standartSearchControl.GetPageAddEvent(LoadRecords);
        }

        private void LoadRecords()
        {
            var result = _service.GetExaminationTemplateBlockQuestions(new ExaminationTemplateBlockQuestionGetBindingModel
            {
                ExaminationTemplateBlockId = _examinationTemplateBlockId,
                ExaminationTemplateId = _examinationTemplateId
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }

            standartSearchControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                bool isNeedFilters = false;
                bool isInclude = false;
                foreach (var elem in standartSearchControl.SearchWords)
                {
                    if (elem.Value.Length > 0)
                    {
                        isNeedFilters = true;
                        switch (elem.Key)
                        {
                            case "ExaminationTemplateBlockName":
                                if (res.ExaminationTemplateBlockName.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "QuestionNumber":
                                if (res.QuestionNumber.ToString().Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "QuestionText":
                                if (res.QuestionText.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                        }
                    }
                }
                if (isNeedFilters)
                {
                    if (isInclude)
                    {
                        standartSearchControl.GetDataGridViewRows.Add(
                            res.Id,
                            res.ExaminationTemplateBlockName,
                            res.QuestionNumber,
                            res.QuestionText
                        );
                    }
                }
                else
                {
                    standartSearchControl.GetDataGridViewRows.Add(
                        res.Id,
                        res.ExaminationTemplateBlockName,
                        res.QuestionNumber,
                        res.QuestionText
                    );
                }
            }
        }
    }
}