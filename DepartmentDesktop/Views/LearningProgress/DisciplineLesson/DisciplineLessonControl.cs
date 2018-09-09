using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson
{
    public partial class DisciplineLessonControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _service;

        private Guid _ayId;

        private Guid _dId;

        private Guid _edId;

        private Guid _tnId;

        public DisciplineLessonControl(IDisciplineLessonService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "AcademicYear", Title = "Учбеный год", Width = 100, Visible = true },
                new ColumnConfig { Name = "EducationDirection", Title = "Направление", Width = 100, Visible = true },
                new ColumnConfig { Name = "Discipline", Title = "Дисциплина", Width = 200, Visible = true },
                new ColumnConfig { Name = "TimeNorm", Title = "Тип", Width = 100, Visible = true },
                new ColumnConfig { Name = "Title", Title = "Заголовок", Width = 300, Visible = true },
                new ColumnConfig { Name = "CountOfPairs", Title = "Кол-во пар", Width = 100, Visible = true },
                new ColumnConfig { Name = "CountOfTasks", Title = "Кол-во заданий", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "MakeCloneToolStripMenuItem", "Дублировать"},
                    { "FormDisciplineLessonsToolStripMenuItem", "Сформировать занятия"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("MakeCloneToolStripMenuItem", MakeCloneToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("FormDisciplineLessonsToolStripMenuItem", FormDisciplineLessonsToolStripMenuItem_Click);
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

        public void LoadData(Guid ayId, Guid dId, Guid edId, Guid tnId)
        {
            _ayId = ayId;
            _dId = dId;
            _edId = edId;
            _tnId = tnId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineLessons(new DisciplineLessonGetBindingModel
            {
                AcademicYearId = _ayId,
                DisciplineId = _dId,
                EducationDirectionId = _edId,
                TimeNormId = _tnId,
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
                    res.AcademicYear,
                    res.EducationDirection,
                    res.Discipline,
                    res.TimeNorm,
                    res.Title,
                    res.CountOfPairs,
                    res.CountTasks
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonForm>(
                new ParameterOverrides
                {
                    { "ayId", _ayId },
                    { "dId", _dId },
                    { "edId", _edId },
                    { "tnId", _tnId },
                    { "id", Guid.Empty }
                }
                .OnType<DisciplineLessonForm>());
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
                var form = Container.Resolve<DisciplineLessonForm>(
                    new ParameterOverrides
                    {
                        { "ayId", _ayId },
                        { "dId", _dId },
                        { "edId", _edId },
                        { "tnId", _tnId },
                        { "id", id }
                    }
                    .OnType<DisciplineLessonForm>());
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
                        var result = _service.DeleteDisciplineLesson(new DisciplineLessonGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void MakeCloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = Container.Resolve<MaterialTechnicalValueReport>();
            //form.Show();
        }

        private void FormDisciplineLessonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LearningProcessFormDisciplineLessonsBindingModel model = new LearningProcessFormDisciplineLessonsBindingModel
            {
                AcademicYearId = _ayId,
                DisciplineId = _dId,
                EducationDirectionId = _edId,
                TimeNormId = _tnId
            };
            var form = Container.Resolve<FormDisciplineLessonsForm>(
                   new ParameterOverrides
                   {
                        { "model", model }
                   }
                   .OnType<FormDisciplineLessonsForm>());
            form.Show();
        }
    }
}
