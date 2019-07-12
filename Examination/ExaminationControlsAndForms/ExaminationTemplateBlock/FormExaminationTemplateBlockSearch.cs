using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Collections.Generic;

namespace ExaminationControlsAndForms.ExaminationTemplateBlock
{
    public partial class FormExaminationTemplateBlockSearch : StandartSearchForm
    {
        private IExaminationTemplateBlockService _service;

        private Guid? _examinationTemplateId;

        public FormExaminationTemplateBlockSearch(IExaminationTemplateBlockService service, Guid? examinationTemplateId = null)
        {
            InitializeComponent();
            _service = service;
            _examinationTemplateId = examinationTemplateId;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "ExaminationTemplateName", Title = "Название экзамена", Width = 200, Visible = true },
                new ColumnConfig { Name = "BlockName", Title = "Название блока", Width = 200, Visible = true },
                new ColumnConfig { Name = "CountQuestionInTicket", Title = "Количество вопросов в билете", Width = 200, Visible = true },
                new ColumnConfig { Name = "IsCombine", Title = "Объединение", Width = 100, Visible = true }
            };

            Configurate(columns);
        }

        protected override void LoadRecords()
        {
            var result = _service.GetExaminationTemplateBlocks(new ExaminationTemplateBlockGetBindingModel
            {
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
                            case "ExaminationTemplateName":
                                if (res.ExaminationTemplateName.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "BlockName":
                                if (res.BlockName.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "CountQuestionInTicket":
                                if (res.CountQuestionInTicket.ToString().Contains(elem.Value))
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
                            res.ExaminationTemplateName,
                            res.BlockName,
                            res.CountQuestionInTicket,
                            res.IsCombine ? "Да" : "Нет"
                        );
                    }
                }
                else
                {
                    dataGridViewList.Rows.Add(
                        res.Id,
                        res.ExaminationTemplateName,
                        res.BlockName,
                        res.CountQuestionInTicket,
                        res.IsCombine ? "Да" : "Нет"
                    );
                }
            }
        }
    }
}