using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonTaskStudentAccept
{
    public partial class DisciplineLessonTaskStudentAcceptControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskStudentAcceptService _service;

        private Guid _dlId;

        private Guid _sgId;

        public DisciplineLessonTaskStudentAcceptControl(IDisciplineLessonTaskStudentAcceptService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "DisciplineLessonTask", Title = "Задание", Width = 200, Visible = true },
                new ColumnConfig { Name = "Student", Title = "Студент", Width = 200, Visible = true },
                new ColumnConfig { Name = "DateAccept", Title = "Дата", Width = 100, Visible = true },
                new ColumnConfig { Name = "Result", Title = "Статус", Width = 200, Visible = true },
                new ColumnConfig { Name = "Score", Title = "Балл", Width = 100, Visible = true },
                new ColumnConfig { Name = "Comment", Title = "Комментарий", Width = 400, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "CreateTasksToolStripMenuItem", "Выдать задания"},
                    { "AssignTasksToolStripMenuItem", "Прием задания"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 40, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("CreateTasksToolStripMenuItem", CreateTasksToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("AssignTasksToolStripMenuItem", AssignTasksToolStripMenuItem_Click);
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

        public void LoadData(Guid dlId, Guid sgId)
        {
            _dlId = dlId;
            _sgId = sgId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineLessonTaskStudentAccepts(new DisciplineLessonTaskStudentAcceptGetBindingModel
            {
                DisciplineLessonId = _dlId,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControl.GetDataGridViewRows.Add(
                    res.Id,
                    res.DisciplineLessonTask,
                    res.Student,
                    res.DateAccept.ToShortDateString(),
                    res.Result.ToString(),
                    res.Score,
                    res.Comment
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonTaskStudentAcceptForm>(
                new ParameterOverrides
                {
                    { "dlId", _dlId },
                    { "sgId", _sgId },
                    { "id", Guid.Empty }
                }
                .OnType<DisciplineLessonTaskStudentAcceptForm>());
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
                var form = Container.Resolve<DisciplineLessonTaskStudentAcceptForm>(
                    new ParameterOverrides
                    {
                        { "dlId", _dlId },
                        { "sgId", _sgId },
                        { "id", id }
                    }
                    .OnType<DisciplineLessonTaskStudentAcceptForm>());
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
                        var result = _service.DeleteDisciplineLessonTaskStudentAccept(new DisciplineLessonTaskStudentAcceptGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void CreateTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CreateAssignTasksForm>(
                new ParameterOverrides
                {
                    { "dlId", _dlId },
                    { "sgId", _sgId }
                }
                .OnType<CreateAssignTasksForm>());
            form.ShowDialog();
        }

        private void AssignTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<AssignTasksForm>(
                new ParameterOverrides
                {
                    { "dlId", _dlId },
                    { "sgId", _sgId }
                }
                .OnType<AssignTasksForm>());
            form.ShowDialog();
        }
    }
}
