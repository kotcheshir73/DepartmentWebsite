using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Collections.Generic;

namespace ExaminationControlsAndForms.ExaminationTemplateBlockQuestion
{
    public partial class FormExaminationTemplateBlockQuestionSearch : StandartSearchForm
    {
        private IExaminationTemplateBlockQuestionService _service;

        private Guid? _examinationTemplateBlockId;

        private Guid? _examinationTemplateId;

        public FormExaminationTemplateBlockQuestionSearch(IExaminationTemplateBlockQuestionService service, Guid? examinationTemplateBlockId = null, Guid? examinationTemplateId = null) : base()
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

            Configurate(columns);
        }

        protected override void LoadRecords()
        {
            var result = _service.GetExaminationTemplateBlockQuestions(new ExaminationTemplateBlockQuestionGetBindingModel
            {
                ExaminationTemplateBlockId = _examinationTemplateBlockId,
                ExaminationTemplateId = _examinationTemplateId
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }

            dataGridViewList.Rows.Clear();
            foreach (var res in result.Result.List)
            {
                bool isNeedFilters = false;
                bool isInclude = false;
                foreach (var elem in _searchWords)
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
                        dataGridViewList.Rows.Add(
                            res.Id,
                            res.ExaminationTemplateBlockName,
                            res.QuestionNumber,
                            res.QuestionText
                        );
                    }
                }
                else
                {
                    dataGridViewList.Rows.Add(
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