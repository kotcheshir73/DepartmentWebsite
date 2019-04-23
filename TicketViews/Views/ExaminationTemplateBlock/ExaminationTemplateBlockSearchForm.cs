using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;

namespace TicketViews.Views.ExaminationTemplateBlock
{
    public partial class ExaminationTemplateBlockSearchForm : Form
    {
        private IExaminationTemplateBlockService _service;

        private Guid? _examinationTemplateId;

        private Guid _selectedId;

        public Guid SelectedId { get { return _selectedId; } }

        public ExaminationTemplateBlockSearchForm(IExaminationTemplateBlockService service, Guid? examinationTemplateId = null)
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

            standartSearchControl.Configurate(columns);
            standartSearchControl.SelectElementAddEvent((Guid Id) => { _selectedId = Id; DialogResult = DialogResult.OK; Close(); });
            standartSearchControl.GetPageAddEvent(LoadRecords);
        }

        private void LoadRecords()
        {
            var result = _service.GetExaminationTemplateBlocks(new ExaminationTemplateBlockGetBindingModel
            {
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
                        standartSearchControl.GetDataGridViewRows.Add(
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
                    standartSearchControl.GetDataGridViewRows.Add(
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