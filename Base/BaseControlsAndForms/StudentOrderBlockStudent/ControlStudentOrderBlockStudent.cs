using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace BaseControlsAndForms.StudentOrderBlockStudent
{
    public partial class ControlStudentOrderBlockStudent : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentOrderBlockStudentService _service;

        private Guid? _soId;

        private Guid? _sobId;

        public ControlStudentOrderBlockStudent(IStudentOrderBlockStudentService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "StudentOrderBlock", Title = "Блок приказа", Width = 200, Visible = true },
                new ColumnConfig { Name = "Student", Title = "Студент", Width = 150, Visible = true },
                new ColumnConfig { Name = "StudentGromFrom", Title = "Из группы", Width = 100, Visible = true },
                new ColumnConfig { Name = "StudentGroupTo", Title = "В группу", Width = 100, Visible = true },
                new ColumnConfig { Name = "Info", Title = "Информация", Width = 200, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAddEvent", "toolStripDropDownButtonMoves" };

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

        public void LoadData(Guid? sobId, Guid? soId)
        {
            _soId = soId;
            _sobId = sobId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetStudentOrderBlockStudents(new StudentOrderBlockStudentGetBindingModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                StudentOrderId = _soId,
                StudentOrderBlockId = _sobId
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
                    res.StudentOrderBlock,
                    res.Student,
                    res.StudentGromFrom,
                    res.StudentGroupTo,
                    res.Info
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormStudentOrderBlockStudent>(
                new ParameterOverrides
                {
                    { "sobId", _sobId },
                    { "id", Guid.Empty }
                }
                .OnType<FormStudentOrderBlockStudent>());
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
                var form = Container.Resolve<FormStudentOrderBlockStudent>(
                    new ParameterOverrides
                    {
                        { "sobId", _sobId },
                        { "id", id }
                    }
                    .OnType<FormStudentOrderBlockStudent>());
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
                        var result = _service.DeleteStudentOrderBlockStudent(new StudentOrderBlockStudentGetBindingModel { Id = id });
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