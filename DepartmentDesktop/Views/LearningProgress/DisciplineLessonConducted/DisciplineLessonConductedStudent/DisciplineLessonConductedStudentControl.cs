using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonConducted.DisciplineLessonConductedStudent
{
    public partial class DisciplineLessonConductedStudentControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedStudentService _service;

        private Guid _dlcId;

        private Guid _sgId;

        public DisciplineLessonConductedStudentControl(IDisciplineLessonConductedStudentService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "DisciplineLesson", Title = "Занятие", Width = 200, Visible = true },
                new ColumnConfig { Name = "Student", Title = "Студент", Width = 100, Visible = true },
                new ColumnConfig { Name = "Status", Title = "Статус", Width = 200, Visible = true },
                new ColumnConfig { Name = "Ball", Title = "Балл", Width = 100, Visible = true },
                new ColumnConfig { Name = "Comment", Title = "Комментарий", Width = 500, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "FillGroupToolStripMenuItem", "Заполнить группу"},
                    { "PrintVariantsToolStripMenuItem", "Распечатать варианты"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("FillGroupToolStripMenuItem", FillGroupToolStripMenuItem_Click);
            //standartControl.ToolStripButtonMoveEventClickAddEvent("PrintVariantsToolStripMenuItem", PrintVariantsToolStripMenuItem_Click);
            //standartControl.ToolStripButtonMoveEventClickAddEvent("PrintSubgroupsToolStripMenuItem", PrintSubgroupsToolStripMenuItem_Click);
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

        public void LoadData(Guid dlcId, Guid sgId)
        {
            _dlcId = dlcId;
            _sgId = sgId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineLessonConductedStudents(new DisciplineLessonConductedStudentGetBindingModel
            {
                DisciplineLessonConductedId = _dlcId,
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
                    res.DisciplineLesson,
                    res.Student,
                    res.Status,
                    res.Ball?.ToString() ?? "Нет",
                    res.Comment
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonConductedStudentForm>(
                new ParameterOverrides
                {
                    { "dlcId", _dlcId },
                    { "sgId", _sgId },
                    { "id", Guid.Empty }
                }
                .OnType<DisciplineLessonConductedStudentForm>());
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
                var form = Container.Resolve<DisciplineLessonConductedStudentForm>(
                    new ParameterOverrides
                    {
                        { "dlcId", _dlcId },
                        { "sgId", _sgId },
                        { "id", id }
                    }
                    .OnType<DisciplineLessonConductedStudentForm>());
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
                        var result = _service.DeleteDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void FillGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<DisciplineLessonConductedStudentFillStudentsForm>(
                new ParameterOverrides
                {
                    { "dlcId", _dlcId },
                    { "sgId", _sgId }
                }
                .OnType<DisciplineLessonConductedStudentFillStudentsForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
        }
    }
}
