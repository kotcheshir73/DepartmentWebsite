using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineStudentRecord
{
    public partial class DisciplineStudentRecordControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineStudentRecordService _service;

        private Guid _dId;

        private Guid _sgId;

        private string _semester;

        public DisciplineStudentRecordControl(IDisciplineStudentRecordService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Discipline", Title = "Дисциплина", Width = 200, Visible = true },
                new ColumnConfig { Name = "StudentGroup", Title = "Группа", Width = 100, Visible = true },
                new ColumnConfig { Name = "Student", Title = "Студент", Width = 200, Visible = true },
                new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 100, Visible = true },
                new ColumnConfig { Name = "Variant", Title = "Вариант", Width = 150, Visible = true },
                new ColumnConfig { Name = "SubGroup", Title = "Подгруппа", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAdd" };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "FillGroupToolStripMenuItem", "Заполнить на группу"},
                    { "PrintToolStripMenuItem", "Распечатать"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("FillGroupToolStripMenuItem", FillGroupToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("PrintToolStripMenuItem", PrintToolStripMenuItem_Click);
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

        public void LoadData(Guid dId, Guid sgId, string semester)
        {
            _dId = dId;
            _sgId = sgId;
            _semester = semester;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineStudentRecords(new DisciplineStudentRecordGetBindingModel
            {
                DisciplineId = _dId,
                StudentGroupId = _sgId,
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
                    res.Discipline,
                    res.StudentGroup,
                    res.Student,
                    res.Semester,
                    res.Variant,
                    res.SubGroup
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineStudentRecordForm>(
                new ParameterOverrides
                {
                    { "dId", _dId },
                    { "sgId", _sgId },
                    { "semester", _semester },
                    { "id", Guid.Empty }
                }
                .OnType<DisciplineStudentRecordForm>());
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
                var form = Container.Resolve<DisciplineStudentRecordForm>(
                    new ParameterOverrides
                    {
                        { "dId", _dId },
                        { "sgId", _sgId },
                        { "semester", _semester },
                        { "id", id }
                    }
                    .OnType<DisciplineStudentRecordForm>());
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
                        var result = _service.DeleteDisciplineStudentRecord(new DisciplineStudentRecordGetBindingModel { Id = id });
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
            //var form = Container.Resolve<DuplicateDisciplineLessonForm>(
            //    new ParameterOverrides
            //    {
            //        { "dlId", id }
            //    }
            //    .OnType<DuplicateDisciplineLessonForm>());
            //form.Show();
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LearningProcessFormDisciplineLessonsBindingModel model = new LearningProcessFormDisciplineLessonsBindingModel
            //{
            //    AcademicYearId = _ayId,
            //    DisciplineId = _dId,
            //    EducationDirectionId = _edId,
            //    TimeNormId = _tnId
            //};
            //var form = Container.Resolve<FormDisciplineLessonsForm>(
            //       new ParameterOverrides
            //       {
            //            { "model", model }
            //       }
            //       .OnType<FormDisciplineLessonsForm>());
            //form.Show();
        }
    }
}
