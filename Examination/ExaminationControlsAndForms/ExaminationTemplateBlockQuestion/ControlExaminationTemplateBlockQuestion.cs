using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace ExaminationControlsAndForms.ExaminationTemplateBlockQuestion
{
    public partial class ControlExaminationTemplateBlockQuestion : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IExaminationTemplateBlockQuestionService _service;

        private Guid _examinationTemplateBlockId;

        private Guid _examinationTemplateId;

        public ControlExaminationTemplateBlockQuestion(IExaminationTemplateBlockQuestionService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "ExaminationTemplateBlockName", Title = "Название блока", Width = 100, Visible = true },
                new ColumnConfig { Name = "QuestionNumber", Title = "Номер вопроса из блока", Width = 150, Visible = true },
                new ColumnConfig { Name = "QuestionText", Title = "Текст вопроса", Width = 400, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
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

        public void LoadData(Guid examinationTemplateBlockId, Guid examinationTemplateId)
        {
            _examinationTemplateBlockId = examinationTemplateBlockId;
            _examinationTemplateId = examinationTemplateId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetExaminationTemplateBlockQuestions(new ExaminationTemplateBlockQuestionGetBindingModel
            {
                ExaminationTemplateBlockId = _examinationTemplateBlockId,
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
                    res.ExaminationTemplateBlockName,
                    res.QuestionNumber,
                    res.QuestionText
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormExaminationTemplateBlockQuestion>(
                new ParameterOverrides
                {
                    { "examinationTemplateBlockId", _examinationTemplateBlockId },
                    { "examinationTemplateId", _examinationTemplateId },
                    { "id", Guid.Empty }
                }
                .OnType<FormExaminationTemplateBlockQuestion>());
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
                var form = Container.Resolve<FormExaminationTemplateBlockQuestion>(
                    new ParameterOverrides
                    {
                        { "examinationTemplateBlockId", _examinationTemplateBlockId },
                        { "examinationTemplateId", _examinationTemplateId },
                        { "id", id }
                    }
                    .OnType<FormExaminationTemplateBlockQuestion>());
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
                        var result = _service.DeleteExaminationTemplateBlockQuestion(new ExaminationTemplateBlockQuestionGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }
    }
}