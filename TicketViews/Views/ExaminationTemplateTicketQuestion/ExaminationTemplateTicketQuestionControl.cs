using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace TicketViews.Views.ExaminationTemplateTicketQuestion
{
    public partial class ExaminationTemplateTicketQuestionControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateTicketQuestionService _service;

        private Guid _examinationTemplateTicketId;

        private Guid _examinationTemplateId;

        public ExaminationTemplateTicketQuestionControl(IExaminationTemplateTicketQuestionService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "ExaminationTemplateTicketNumber", Title = "Номер билета", Width = 100, Visible = true },
                new ColumnConfig { Name = "ExaminationTemplateBlockQuestionNumber", Title = "Номер вопроса из блока", Width = 150, Visible = true },
                new ColumnConfig { Name = "ExaminationTemplateBlockQuestionQuestion", Title = "Вопрос", Width = 250, Visible = true },
                new ColumnConfig { Name = "Order", Title = "Порядковый номер", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartListControl.Configurate(columns, hideToolStripButtons);

            standartListControl.GetPageAddEvent(LoadRecords);
            standartListControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartListControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartListControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
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

        public void LoadData(Guid examinationTemplateTicketId, Guid examinationTemplateId)
        {
            _examinationTemplateTicketId = examinationTemplateTicketId;
            _examinationTemplateId = examinationTemplateId;
            standartListControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetExaminationTemplateTicketQuestions(new ExaminationTemplateTicketQuestionGetBindingModel
            {
                ExaminationTemplateTicketId = _examinationTemplateTicketId,
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
                    res.ExaminationTemplateTicketNumber,
                    res.ExaminationTemplateBlockQuestionNumber,
                    res.ExaminationTemplateBlockQuestionQuestion,
                    res.Order
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<ExaminationTemplateTicketQuestionForm>(
                new ParameterOverrides
                {
                    { "examinationTemplateTicketId", _examinationTemplateTicketId },
                    { "examinationTemplateId", _examinationTemplateId },
                    { "id", Guid.Empty }
                }
                .OnType<ExaminationTemplateTicketQuestionForm>());
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
                var form = Container.Resolve<ExaminationTemplateTicketQuestionForm>(
                    new ParameterOverrides
                    {
                        { "examinationTemplateTicketId", _examinationTemplateTicketId },
                        { "examinationTemplateId", _examinationTemplateId },
                        { "id", id }
                    }
                    .OnType<ExaminationTemplateTicketQuestionForm>());
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
                        var result = _service.DeleteExaminationTemplateTicketQuestion(new ExaminationTemplateTicketQuestionGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartListControl.LoadPage();
                }
            }
        }
    }
}