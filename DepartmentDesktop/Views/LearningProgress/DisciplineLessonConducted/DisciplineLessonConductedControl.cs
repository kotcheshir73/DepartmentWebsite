using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity.Attributes;
using Unity;
using DepartmentService.IServices;
using DepartmentService.BindingModels;
using Unity.Resolution;
using DepartmentDesktop.Models;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLessonConducted
{
    public partial class DisciplineLessonConductedControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedService _service;

        private Guid _ayId;

        private Guid _edId;

        private Guid _dId;

        private Guid _tnId;

        private string _semester;

        public DisciplineLessonConductedControl(IDisciplineLessonConductedService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 100, Visible = true },
                new ColumnConfig { Name = "DisciplineLesson", Title = "Занятие", Width = 200, Visible = true },
                new ColumnConfig { Name = "StudentGroup", Title = "Группа", Width = 100, Visible = true },
                new ColumnConfig { Name = "Date", Title = "Дата проведения", Width = 200, Visible = true },
                new ColumnConfig { Name = "SubGroup", Title = "Подгруппа", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "PrintLessonConductedToolStripMenuItem", "Посещаемость"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("PrintLessonConductedToolStripMenuItem", PrintLessonConductedToolStripMenuItem_Click);
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

        public void LoadData(Guid ayId, Guid edId, Guid dId, Guid tnId, string semester)
        {
            _ayId = ayId;
            _edId = edId;
            _dId = dId;
            _tnId = tnId;
            _semester = semester;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineLessonConducteds(new DisciplineLessonConductedGetBindingModel
            {
                EducationFirectionId = _edId,
                DisciplineId = _dId,
                Semester = _semester,
                TimeNormId =_tnId,
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
                    res.Semester,
                    res.DisciplineLesson,
                    res.StudentGroup,
                    res.Date.ToShortDateString(),
                    res.Subgroup
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonConductedForm>(
                new ParameterOverrides
                {
                    { "ayId", _ayId },
                    { "edId", _edId },
                    { "dId", _dId },
                    { "tnId", _tnId },
                    { "semester", _semester },
                    { "id", Guid.Empty }
                }
                .OnType<DisciplineLessonConductedForm>());
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
                var form = Container.Resolve<DisciplineLessonConductedForm>(
                    new ParameterOverrides
                    {
                        { "ayId", _ayId },
                        { "edId", _edId },
                        { "dId", _dId },
                        { "tnId", _tnId },
                        { "semester", _semester },
                        { "id", id }
                    }
                    .OnType<DisciplineLessonConductedForm>());
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
                        var result = _service.DeleteDisciplineLessonConducted(new DisciplineLessonConductedGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void PrintLessonConductedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<LessonConductedForm>(
                new ParameterOverrides
                {
                    { "ayId", _ayId },
                    { "edId", _edId },
                    { "dId", _dId },
                    { "tnId", _tnId },
                    { "semester", _semester }
                }
                .OnType<LessonConductedForm>());
            form.ShowDialog();
        }
    }
}
