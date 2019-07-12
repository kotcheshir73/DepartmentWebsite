using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System.Collections.Generic;

namespace ExaminationControlsAndForms.TicketTemplate
{
    public partial class FormTicketTemplateSearch : StandartSearchForm
    {
        private ITicketTemplateService _service;

        public FormTicketTemplateSearch(ITicketTemplateService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "TemplateName", Title = "Название шаблона", Width = 300, Visible = true }
            };

            Configurate(columns);
        }

        protected override void LoadRecords()
        {
            var result = _service.GetTicketTemplates(new TicketTemplateGetBindingModel());
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
                        dataGridViewList.Rows.Add(
                            res.Id,
                            res.TemplateName
                        );
                    }
                }
                else
                {
                    dataGridViewList.Rows.Add(
                        res.Id,
                        res.TemplateName
                    );
                }
            }
        }
    }
}