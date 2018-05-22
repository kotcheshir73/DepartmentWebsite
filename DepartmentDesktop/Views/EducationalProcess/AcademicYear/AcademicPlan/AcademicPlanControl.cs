using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
using Unity.Resolution;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicPlan
{
    public partial class AcademicPlanControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanService _service;

        private readonly IEducationalProcessService _serviceEP;

        private Guid _ayId;

        public AcademicPlanControl(IAcademicPlanService service, IEducationalProcessService serviceEP)
        {
            InitializeComponent();
            _service = service;
            _serviceEP = serviceEP;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "EducationDirection", Title = "Направление", Width = 100, Visible = true },
                new ColumnConfig { Name = "AcademicLevel", Title = "Уровень", Width = 150, Visible = true },
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
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return -1;
            }
            standartControl.GetDataGridViewRows.Clear();
            foreach (var res in result.Result.List)
            {
                standartControl.GetDataGridViewRows.Add(
                    res.Id,
                    res.EducationDirection,
                    res.AcademicLevel,
                    res.AcademicCoursesStrings
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<AcademicPlanForm>(
                new ParameterOverrides
                {
                    { "ayId", _ayId },
                    { "id", Guid.Empty }
                }
                .OnType<AcademicPlanForm>());
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
                var form = Container.Resolve<AcademicPlanForm>(
                    new ParameterOverrides
                    {
                        { "ayId", _ayId },
                        { "id", id }
                    }
                    .OnType<AcademicPlanForm>());
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
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
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
                var result = _serviceEP.CreateContingentForAcademicYear(new AcademicYearGetBindingModel { Id = _ayId });
                if (result.Succeeded)
                {
                    MessageBox.Show("Операция успешно выполнена", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
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
                    var result = _serviceEP.LoadFromXMLAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
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
                        Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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
                    var result = _serviceEP.LoadFromBlueAsteriskAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
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
                        Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    }
                }
            }
        }
    }
}
