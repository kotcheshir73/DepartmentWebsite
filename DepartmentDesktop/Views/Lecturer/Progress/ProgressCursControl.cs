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
using DepartmentService.IServices;
using Unity;
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
using DepartmentDesktop.Models;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentModel.Enums;
using Unity.Resolution;

namespace DepartmentDesktop.Views.Lecturer.Progress
{
    public partial class ProgressCursControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineService _serviceD;

        private readonly IDisciplineLessonService _serviceDL;

        private Guid _dId;

        public ProgressCursControl(IDisciplineService serviceD, IDisciplineLessonService serviceDL)
        {
            InitializeComponent();
            _serviceD = serviceD;
            _serviceDL = serviceDL;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "Title", Title = "Тема", Width = 300, Visible = true },
                new ColumnConfig { Name = "Date", Title = "Дата проведения", Width = 100, Visible = true },
                new ColumnConfig { Name = "CountOfPairs", Title = "Кол-во пар", Width = 100, Visible = true },
                new ColumnConfig { Name = "CountOfTasks", Title = "Кол-во заданий", Width = 100, Visible = true }
            };
            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);
            standartControl.GetPageAddEvent(LoadCurRecords);
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

        public int LoadCurRecords(int pageNumber, int pageSize)
        {
            var result = _serviceDL.GetDisciplineLessons(new DisciplineLessonGetBindingModel
            {
                DisciplineId = _dId
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                if (res.LessonType == DisciplineLessonTypes.курсовая)
                {
                    standartControl.GetDataGridViewRows.Add(
                         res.Id,
                         res.Title,
                         res.Date,
                         res.CountOfPairs,
                         null
                    );
                }
            }
            return result.Result.MaxCount;
        }
        
        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonSettingsForm>(new ParameterOverrides
                {
                    {"id", Guid.Empty},
                    {"dId",_dId }
                }
                .OnType<DisciplineLessonSettingsForm>());
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
                var form = Container.Resolve<DisciplineLessonSettingsForm>(
                    new ParameterOverrides
                    {
                          { "id", id },
                          {"dId", _dId }
                    }
                    .OnType<DisciplineLessonSettingsForm>());
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
                        var result = _serviceDL.DeleteDisciplineLesson(new DisciplineLessonGetBindingModel { Id = id });
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
