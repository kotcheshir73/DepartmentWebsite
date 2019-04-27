using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Collections.Generic;

namespace ExaminationControlsAndForms.ExaminationTemplateTicket
{
    public partial class FormExaminationTemplateTicketSearch : StandartSearchForm
    {
        private IExaminationTemplateTicketService _service;

        private Guid _examinationTemplateId;

        public FormExaminationTemplateTicketSearch(IExaminationTemplateTicketService service, Guid examinationTemplateId)
        {
            InitializeComponent();
            _service = service;
            _examinationTemplateId = examinationTemplateId;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "ExaminationTemplateName", Title = "Экзамен", Width = 100, Visible = true },
                new ColumnConfig { Name = "TicketNumber", Title = "Номер билета", Width = 150, Visible = true }
            };

            Configurate(columns);
        }

        protected override void LoadRecords()
        {
            var result = _service.GetExaminationTemplateTickets(new ExaminationTemplateTicketGetBindingModel { ExaminationTemplateId = _examinationTemplateId });
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
                foreach(var elem in _searchWords)
                {
                    if(elem.Value.Length > 0)
                    {
                        isNeedFilters = true;
                        switch (elem.Key)
                        {
                            case "ExaminationTemplateName":
                                if(res.ExaminationTemplateName.Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                            case "TicketNumber":
                                if (res.TicketNumber.ToString().Contains(elem.Value))
                                {
                                    isInclude = true;
                                }
                                break;
                        }
                    }
                }
                if(isNeedFilters)
                {
                    if(isInclude)
                    {
                        dataGridViewList.Rows.Add(
                            res.Id,
                            res.ExaminationTemplateName,
                            res.TicketNumber
                        );
                    }
                }
                else
                {
                    dataGridViewList.Rows.Add(
                        res.Id,
                        res.ExaminationTemplateName,
                        res.TicketNumber
                    );
                }
            }
        }
    }
}