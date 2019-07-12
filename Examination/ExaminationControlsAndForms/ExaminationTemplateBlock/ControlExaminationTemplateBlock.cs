using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace ExaminationControlsAndForms.ExaminationTemplateBlock
{
    public partial class ControlExaminationTemplateBlock : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateBlockService _service;

        private readonly ITicketProcess _process;

        private Guid _examinationTemplateId;

        public ControlExaminationTemplateBlock(IExaminationTemplateBlockService service, ITicketProcess process)
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
                new ColumnConfig { Name = "CountQuestionInTicket", Title = "Количество вопросов в билете", Width = 300, Visible = true },
                new ColumnConfig { Name = "IsCombine", Title = "Объединение", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "LoadQuestionsToolStripMenuItem", "Загрузить список вопросов"},
                    { "SynchronizeBlocksByTemplateToolStripMenuItem", "Синхронизировать блоки по шаблону"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("LoadQuestionsToolStripMenuItem", LoadQuestionsToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("SynchronizeBlocksByTemplateToolStripMenuItem", SynchronizeBlocksByTemplateToolStripMenuItem_Click);
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) =>
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
            standartControl.LoadPage();
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
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControl.GetDataGridViewRows.Add(
                    res.Id,
                    res.ExaminationTemplateName,
                    res.BlockName,
                    res.QuestionTagInTemplate,
                    res.CountQuestionInTicket,
                    res.IsCombine? "Да" : "Нет"
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormExaminationTemplateBlock>(
                new ParameterOverrides
                {
                    { "examinationTemplateId", _examinationTemplateId },
                    { "id", Guid.Empty }
                }
                .OnType<FormExaminationTemplateBlock>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
        }

        private void UpdRecord()
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<FormExaminationTemplateBlock>(
                    new ParameterOverrides
                    {
                        { "examinationTemplateId", _examinationTemplateId },
                        { "id", id }
                    }
                    .OnType<FormExaminationTemplateBlock>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }

        private void DelRecord()
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _service.DeleteExaminationTemplateBlock(new ExaminationTemplateBlockGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void LoadQuestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "txt file|*.txt"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                    var result = _process.LoadQuestions(new TicketProcessLoadQuestionsBindingModel
                    {
                        ExaminationTemplateBlockId = id,
                        FileName = ofd.FileName
                    });
                    if(result.Succeeded)
                    {
                        MessageBox.Show("Вопросы загружены", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                    }
                }
            }
        }

        private void SynchronizeBlocksByTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Выполнить синхронизацию", "Портал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var result = _process.SynchronizeBlocksByTemplate(new TicketProcessSynchronizeBlocksByTemplateBindingModel
                {
                    ExaminationTemplateId = _examinationTemplateId
                });
                if(result.Succeeded)
                {
                    standartControl.LoadPage();
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При синхронизации возникла ошибка: ", result.Errors);
                }
            }
        }
    }
}