using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;

namespace TicketViews.Views.ExaminationTemplateTicket
{
    public partial class ExaminationTemplateTicketSearchForm : Form
    {
        private IExaminationTemplateTicketService _service;

        private Guid _examinationTemplateId;

        private Guid _selectedId;

        public Guid SelectedId { get { return _selectedId; } }

        public ExaminationTemplateTicketSearchForm(IExaminationTemplateTicketService service, Guid examinationTemplateId)
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

            standartSearchControl.Configurate(columns);
            standartSearchControl.SelectElementAddEvent((Guid Id) => { _selectedId = Id; DialogResult = DialogResult.OK; Close(); });
            standartSearchControl.GetPageAddEvent(LoadRecords);
        }

        private void LoadRecords()
        {
            var result = _service.GetExaminationTemplateTickets(new ExaminationTemplateTicketGetBindingModel { ExaminationTemplateId = _examinationTemplateId });
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
                foreach(var elem in standartSearchControl.SearchWords)
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
                        standartSearchControl.GetDataGridViewRows.Add(
                            res.Id,
                            res.ExaminationTemplateName,
                            res.TicketNumber
                        );
                    }
                }
                else
                {
                    standartSearchControl.GetDataGridViewRows.Add(
                        res.Id,
                        res.ExaminationTemplateName,
                        res.TicketNumber
                    );
                }
            }
        }
    }
}