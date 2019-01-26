using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.Interfaces;
using TicketViews.Models;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace TicketViews.Views.ExaminationTemplateBlock
{
    public partial class ExaminationTemplateBlockControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateBlockService _service;

        private readonly ITicketProcess _process;

        private Guid _examinationTemplateId;

        public ExaminationTemplateBlockControl(IExaminationTemplateBlockService service, ITicketProcess process)
        {
            InitializeComponent();
            _service = service;
            _process = process;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "ExaminationTemplateName", Title = "Название экзамена", Width = 300, Visible = true },
                new ColumnConfig { Name = "BlockName", Title = "Название блока", Width = 200, Visible = true },
                new ColumnConfig { Name = "QuestionTagInTemplate", Title = "Тег вопроса в шаблоне", Width = 300, Visible = true },
                new ColumnConfig { Name = "CountQuestionInTicket", Title = "Количество вопросов в билете", Width = 300, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "LoadQuestionsToolStripMenuItem", "Загрузить список вопросов"}
                };

            standartListControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartListControl.GetPageAddEvent(LoadRecords);
            standartListControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartListControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartListControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartListControl.ToolStripButtonMoveEventClickAddEvent("LoadQuestionsToolStripMenuItem", LoadQuestionsToolStripMenuItem_Click);
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
            var result = _service.GetExaminationTemplateBlocks(new ExaminationTemplateBlockGetBindingModel
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
                    res.BlockName,
                    res.QuestionTagInTemplate,
                    res.CountQuestionInTicket
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<ExaminationTemplateBlockForm>(
                new ParameterOverrides
                {
                    { "examinationTemplateId", _examinationTemplateId },
                    { "id", Guid.Empty }
                }
                .OnType<ExaminationTemplateBlockForm>());
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
                var form = Container.Resolve<ExaminationTemplateBlockForm>(
                    new ParameterOverrides
                    {
                        { "examinationTemplateId", _examinationTemplateId },
                        { "id", id }
                    }
                    .OnType<ExaminationTemplateBlockForm>());
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
                        var result = _service.DeleteExaminationTemplateBlock(new ExaminationTemplateBlockGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartListControl.LoadPage();
                }
            }
        }

        private void LoadQuestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartListControl.GetDataGridViewSelectedRows.Count == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "txt file|*.txt"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Guid id = new Guid(standartListControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                    _process.LoadQuestions(new TicketProcessLoadQuestionsBindingModel
                    {
                        ExaminationTemplateBlockId = id,
                        FileName = ofd.FileName
                    });
                }
            }
        }
    }
}