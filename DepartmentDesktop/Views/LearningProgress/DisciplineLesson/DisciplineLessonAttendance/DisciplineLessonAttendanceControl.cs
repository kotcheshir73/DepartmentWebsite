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
using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using Unity.Resolution;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonAttendance
{
    public partial class DisciplineLessonAttendanceControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonService _service;

        private Guid _dId;

        private string _type;

        public DisciplineLessonAttendanceControl(IDisciplineLessonService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Lesson", Title = "Занятие", Width = 200, Visible = true },
                new ColumnConfig { Name = "Date", Title = "Дата", Width = 100, Visible = true },
                new ColumnConfig { Name = "CountOfStudents", Title = "Кол-во присутствующих", Width = 200, Visible = true },
                new ColumnConfig { Name = "CountOfMissed", Title = "Кол-во пропустивших", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "ToolStripButtonAddEventClickAddEvent", "ToolStripButtonDelEventClickAddEvent", "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        UpdRecord();
                        break;
                }
            });
        }

        public void LoadData(Guid dId, string type)
        {
            _dId = dId;
            _type = type;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineLessons(new DisciplineLessonGetBindingModel { DisciplineId = _dId, /*LessonType = _type,*/ PageNumber = pageNumber, PageSize = pageSize });
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
                    res.Title,
                    res.Date,
                    null,
                    null
                    );
            }
            return result.Result.MaxCount;
        }

        private void UpdRecord()
        {
            var form = Container.Resolve<DisciplineLessonAttendanceStudentRecordsForm>(
                new ParameterOverrides
                {
                    { "dlId", _dId },
                    { "id", Guid.Empty }
                }
                .OnType<DisciplineLessonAttendanceStudentRecordsForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
        }
    }
}
