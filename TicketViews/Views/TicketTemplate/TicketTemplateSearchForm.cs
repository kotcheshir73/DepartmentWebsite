using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;

namespace TicketViews.Views.TicketTemplate
{
    public partial class TicketTemplateSearchForm : Form
    {
        private ITicketTemplateService _service;

        private Guid _examinationTemplateId;

        private Guid _selectedId;

        public Guid SelectedId { get { return _selectedId; } }

        public TicketTemplateSearchForm(ITicketTemplateService service, Guid examinationTemplateId)
        {
            InitializeComponent();
            _service = service;
            _examinationTemplateId = examinationTemplateId;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "TemplateName", Title = "Название шаблона", Width = 300, Visible = true }
            };

            standartSearchControl.Configurate(columns);
            standartSearchControl.SelectElementAddEvent((Guid Id) => { _selectedId = Id; DialogResult = DialogResult.OK; Close(); });
            standartSearchControl.GetPageAddEvent(LoadRecords);
        }

        private void LoadRecords()
        {
            var result = _service.GetTicketTemplates(new TicketTemplateGetBindingModel { ExaminationTemplateId = _examinationTemplateId });
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
                            case "TemplateName":
                                if (res.TemplateName.Contains(elem.Value))
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
                            res.TemplateName
                        );
                    }
                }
                else
                {
                    standartSearchControl.GetDataGridViewRows.Add(
                        res.Id,
                        res.TemplateName
                    );
                }
            }
        }
    }
}