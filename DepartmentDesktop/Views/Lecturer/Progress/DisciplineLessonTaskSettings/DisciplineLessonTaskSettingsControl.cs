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

namespace DepartmentDesktop.Views.Lecturer.DisciplineLessonTaskSettings
{
    public partial class DisciplineLessonTaskSettingsControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskService _serviceDLT;

        private readonly IDisciplineLessonTaskVariantService _serviceDLTV;
        
        private Guid _taskId;

        public DisciplineLessonTaskSettingsControl(IDisciplineLessonTaskService serviceDLT, IDisciplineLessonTaskVariantService serviceDLTV)
        {
            InitializeComponent();

            _serviceDLT = serviceDLT;
            _serviceDLTV = serviceDLTV;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "VariantNumber", Title = "Номер варианта", Width = 100, Visible = true },
                new ColumnConfig { Name = "VariantTask", Title = "Задание", Width = 200, Visible = true }
            };
            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);
            standartControl.GetPageAddEvent(LoadTaskVariantsRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => DelRecord());
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

        public void LoadData(Guid taskId)
        {
            _taskId = taskId;
            standartControl.LoadPage();
        }

        public int LoadTaskVariantsRecords(int pageNumber, int pageSize)
        {
            var result = _serviceDLTV.GetDisciplineLessonTaskVariants(new DisciplineLessonTaskVariantGetBindingModel
            {
                DisciplineLessonTaskId = _taskId,
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
                     res.VariantNumber,
                     res.VariantTask
                );
            }
            return result.Result.MaxCount;
        }
        
        private void AddRecord()
        {
            var form = Container.Resolve<DisciplineLessonTaskVariantSettingsForm>(new ParameterOverrides
                {
                    { "id", Guid.Empty},
                    {"taskId", _taskId }
                }
                .OnType<DisciplineLessonTaskVariantSettingsForm>());
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
                var form = Container.Resolve<DisciplineLessonTaskVariantSettingsForm>(
                    new ParameterOverrides
                    {
                        { "id", id },
                        {"taskId", _taskId }
                    }
                    .OnType<DisciplineLessonTaskVariantSettingsForm>());
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
                        var result = _serviceDLTV.DeleteDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantGetBindingModel { Id = id });
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
