using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AcademicYearControlsAndForms.AcademicPlan
{
    public partial class ControlAcademicPlan : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanService _service;

        private readonly IAcademicYearProcess _process;

        private Guid _ayId;

        public ControlAcademicPlan(IAcademicPlanService service, IAcademicYearProcess process)
        {
            InitializeComponent();
            _service = service;
            _process = process;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "EducationDirection", Title = "Направление", Width = 300, Visible = true },
                new ColumnConfig { Name = "AcademicCourses", Title = "Курсы", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "CreateContingentToolStripMenuItem", "Создать контингент"},
                    { "LoadFromXMLToolStripMenuItem", "Загрузить из xml"},
                    { "LoadFromBlueAsteriskToolStripMenuItem", "Загрузить из xml (синияя звездчока)"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("CreateContingentToolStripMenuItem", CreateContingentToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("LoadFromXMLToolStripMenuItem", LoadFromXMLToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("LoadFromBlueAsteriskToolStripMenuItem", LoadFromBlueAsteriskToolStripMenuItem_Click);
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

        public void LoadData(Guid ayId)
        {
            _ayId = ayId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = _ayId, PageNumber = pageNumber, PageSize = pageSize });
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
                    res.EducationDirection,
                    res.AcademicCoursesStrings
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormAcademicPlan>(
                new ParameterOverrides
                {
                    { "ayId", _ayId },
                    { "id", Guid.Empty }
                }
                .OnType<FormAcademicPlan>());
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
                var form = Container.Resolve<FormAcademicPlan>(
                    new ParameterOverrides
                    {
                        { "ayId", _ayId },
                        { "id", id }
                    }
                    .OnType<FormAcademicPlan>());
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
                        var result = _service.DeleteAcademicPlan(new AcademicPlanGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void CreateContingentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите создать или обновить контингент?", "Портал", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var result = _process.CreateContingentForAcademicYear(new AcademicYearGetBindingModel { Id = _ayId });
                if (result.Succeeded)
                {
                    MessageBox.Show("Операция успешно выполнена", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При выполнении возникла ошибка: ", result.Errors);
                }
                standartControl.LoadPage();
            }
        }

        private void LoadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "xml|*.xml"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Guid apId = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                    var result = _process.LoadFromXMLAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
                    {
                        Id = apId,
                        FileName = dialog.FileName
                    });
                    if (result.Succeeded)
                    {
                        MessageBox.Show("Загрузка прошла успешно", "Портал", MessageBoxButtons.OK);
                    }
                    else
                    {
                        ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    }
                }
            }
        }

        private void LoadFromBlueAsteriskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count == 1)
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "plx|*.plx"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Guid apId = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                    var result = _process.LoadFromBlueAsteriskAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
                    {
                        Id = apId,
                        FileName = dialog.FileName
                    });
                    if (result.Succeeded)
                    {
                        MessageBox.Show("Загрузка прошла успешно", "Портал", MessageBoxButtons.OK);
                    }
                    else
                    {
                        ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    }
                }
            }
        }
    }
}