using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace TicketViews.Views.ExaminationTemplateTicket
{
    public partial class ExaminationTemplateTicketControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateTicketService _service;

        private Guid _examinationTemplateId;

        public ExaminationTemplateTicketControl(IExaminationTemplateTicketService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "ExaminationTemplateName", Title = "Название экзамена", Width = 300, Visible = true },
                new ColumnConfig { Name = "TicketNumber", Title = "Номер билета", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "CreateTicketsToolStripMenuItem", "Создать билеты"}
                };

            standartListControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartListControl.GetPageAddEvent(LoadRecords);
            standartListControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartListControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartListControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartListControl.ToolStripButtonMoveEventClickAddEvent("CreateTicketsToolStripMenuItem", CreateTicketsToolStripMenuItem_Click);
            standartListControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartListControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        AddRecord();
                        break;
                    case Keys.Enter:
                        UpdRecord();
                        break;
                    case Keys.Delete:
                        DelRecord();
                        break;
                }
            });
        }

        public void LoadData(Guid examinationTemplateId)
        {
            _examinationTemplateId = examinationTemplateId;
            standartListControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetExaminationTemplateTickets(new ExaminationTemplateTicketGetBindingModel
            {
                ExaminationTemplateId = _examinationTemplateId,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartListControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartListControl.GetDataGridViewRows.Add(
                    res.Id,
                    res.ExaminationTemplateName,
                    res.TicketNumber
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<ExaminationTemplateTicketForm>(
                new ParameterOverrides
                {
                    { "examinationTemplateId", _examinationTemplateId },
                    { "id", Guid.Empty }
                }
                .OnType<ExaminationTemplateTicketForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartListControl.LoadPage();
            }
        }

        private void UpdRecord()
        {
            if (standartListControl.GetDataGridViewSelectedRows.Count == 1)
            {
                Guid id = new Guid(standartListControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<ExaminationTemplateTicketForm>(
                    new ParameterOverrides
                    {
                        { "examinationTemplateId", _examinationTemplateId },
                        { "id", id }
                    }
                    .OnType<ExaminationTemplateTicketForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartListControl.LoadPage();
                }
            }
        }

        private void DelRecord()
        {
            if (standartListControl.GetDataGridViewSelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartListControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartListControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteExaminationTemplateTicket(new ExaminationTemplateTicketGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartListControl.LoadPage();
                }
            }
        }

        private void CreateTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ExaminationTemplateTicketCreateTicketsForm>(
                new ParameterOverrides
                {
                    { "examinationTemplateId", _examinationTemplateId }
                }
                .OnType<ExaminationTemplateTicketCreateTicketsForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartListControl.LoadPage();
            }
        }
    }
}