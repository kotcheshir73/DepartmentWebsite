using BaseControlsAndForms.StudentGroup;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using Enums;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace BaseControlsAndForms.Student
{
    public partial class ControlStudent : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentService _service;

        private StudentState? _state;

        private Guid? _sgId;

        public ControlStudent(IStudentService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "NumberOfBook", Title = "Номер зачетки", Width = 150, Visible = true },
                new ColumnConfig { Name = "LastName", Title = "Фамилия", Width = 100, Visible = true },
                new ColumnConfig { Name = "FirstName", Title = "Имя", Width = 100, Visible = true },
                new ColumnConfig { Name = "Patronymic", Title = "Отчество", Width = 150, Visible = true },
                new ColumnConfig { Name = "State", Title = "Статус", Width = 150, Visible = true },
                new ColumnConfig { Name = "StudentGroup", Title = "Группа", Width = 150, Visible = true },
                new ColumnConfig { Name = "Description", Title = "Описание", Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAdd", "toolStripButtonDel" };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
            {
                { "EnrollmentToolStripMenuItem", "Зачислить"},
                { "DeductionToolStripMenuItem", "Отчислить"},
                { "TransferToolStripMenuItem", "Перевести"},
                { "ToAcademToolStripMenuItem", "В академ"},
                { "FromAcademToolStripMenuItem", "Из академа"},
                { "ReestablishToolStripMenuItem", "Восстановить"}
            };

            standartControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            //TODO прописать метод для пункта Восстановить
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("EnrollmentToolStripMenuItem", EnrollmentToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("DeductionToolStripMenuItem", DeductionToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("TransferToolStripMenuItem", TransferToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("ToAcademToolStripMenuItem", ToAcademToolStripMenuItem_Click);
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

        public void LoadData(StudentState? state, Guid? sgId)
        {
            _state = state;
            _sgId = sgId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetStudents(new StudentGetBindingModel
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                StudentStatus = _state,
                StudentGroupId = _sgId
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
                    res.NumberOfBook,
                    res.LastName,
                    res.FirstName,
                    res.Patronymic,
                    res.State,
                    res.StudentGroup,
                    res.Description
                );
            }
            return result.Result.MaxCount;
        }

        private void UpdRecord()
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<FormStudent>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<FormStudent>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }

        private void EnrollmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<StudentGroupEnrollmentForm>(
                    new ParameterOverrides
                    {
                        { "id", _sgId }
                    }
                    .OnType<StudentGroupEnrollmentForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
        }

        private void DeductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                List<Guid> ids = new List<Guid>();
                for(int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                {
                    ids.Add(new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString()));
                }
                var form = Container.Resolve<StudentGroupDeductionForm>(
                        new ParameterOverrides
                        {
                            { "ids", ids }
                        }
                        .OnType<StudentGroupDeductionForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }

        private void TransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<StudentGroupTransferForm>(
                    new ParameterOverrides
                    {
                        { "id", _sgId }
                    }
                    .OnType<StudentGroupTransferForm>());
            if (form.ShowDialog() == DialogResult.OK)
            {
                standartControl.LoadPage();
            }
        }

        private void ToAcademToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                List<Guid> ids = new List<Guid>();
                for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                {
                    ids.Add(new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString()));
                }
                var form = Container.Resolve<StudentGroupToAcademForm>(
                        new ParameterOverrides
                        {
                            { "ids", ids }
                        }
                        .OnType<StudentGroupToAcademForm>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }
    }
}