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
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using DepartmentDesktop.Models;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using Unity.Resolution;

namespace DepartmentDesktop.Views.Lecturer.DisciplineLessonSettings
{
    public partial class DisciplineLessonSettingsControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskService _serviceDLT;

        private Guid _dId;

        public DisciplineLessonSettingsControl(IDisciplineLessonTaskService serviceDLT)
        {
            InitializeComponent();
            _serviceDLT = serviceDLT;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Description", Title = "Описание", Width = 200, Visible = true },
                new ColumnConfig { Name = "MaxBall", Title = "Максимальный балл", Width = 100, Visible = true },
                new ColumnConfig { Name = "IsNecessarily", Title = "Обязательное", Width = 100, Visible = true }
            };
            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);
            standartControl.GetPageAddEvent(LoadTasksRecords);
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

        public void LoadData(Guid dId)
        {
            _dId = dId;
            standartControl.LoadPage();
        }

        public int LoadTasksRecords(int pageNumber, int pageSize)
        {
            var result = _serviceDLT.GetDisciplineLessonTasks(new DisciplineLessonTaskGetBindingModel
            {
                DisciplineLessonId = _dId,
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
                     res.Task,
                     res.MaxBall,
                     res.IsNecessarily
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonTaskSettingsForm>(new ParameterOverrides
                {
                    { "id", Guid.Empty},
                    {"lessonId", _dId }
                }
                .OnType<DisciplineLessonTaskSettingsForm>());
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
                var form = Container.Resolve<DisciplineLessonTaskSettingsForm>(
                    new ParameterOverrides
                    {
                        { "id", id },
                        {"lessonId", _dId }
                    }
                    .OnType<DisciplineLessonTaskSettingsForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }

        private void DelRecord()
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _serviceDLT.DeleteDisciplineLessonTask(new DisciplineLessonTaskGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }
    }
}
