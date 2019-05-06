using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AcademicYearControlsAndForms.DisciplineTimeDistribution
{
    public partial class ControlDisciplineTimeDistribution : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineTimeDistributionService _service;

        private readonly IAcademicYearProcess _process;

        private Guid? _aprId;

        private Guid? _ayId;

        public ControlDisciplineTimeDistribution(IDisciplineTimeDistributionService service, IAcademicYearProcess process)
        {
            InitializeComponent();
            _service = service;
            _process = process;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "StudentGroupName", Title = "Группа", Width = 150, Visible = true },
                new ColumnConfig { Name = "Disciplne", Title = "Дисциплина", Width = 200, Visible = true },
                new ColumnConfig { Name = "Semester", Title = "Семестр", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAdd" };

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
            {
                { "ImportLecturersToolStripMenuItem", "Выгрузить расчасовки по преподавателям"}
            };

            standartControl.Configurate(columns, hideToolStripButtons, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("ImportLecturersToolStripMenuItem", ImportLecturersToolStripMenuItem_Click);
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

        public void LoadData(Guid? aprId, Guid? ayId)
        {
            _aprId = aprId;
            _ayId = ayId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineTimeDistributions(new DisciplineTimeDistributionGetBindingModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                AcademicPlanRecordId = _aprId,
                AcademicYearId = _ayId
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
                    res.StudentGroupName,
                    res.DisciplineName,
                    res.Semester
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormDisciplineTimeDistribution>(
                new ParameterOverrides
                {
                    { "aprId", _aprId },
                    { "id", Guid.Empty }
                }
                .OnType<FormDisciplineTimeDistribution>());
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
                var form = Container.Resolve<FormDisciplineTimeDistribution>(
                    new ParameterOverrides
                    {
                        { "aprId", _aprId },
                        { "id", id }
                    }
                    .OnType<FormDisciplineTimeDistribution>());
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
                        var result = _service.DeleteDisciplineTimeDistribution(new DisciplineTimeDistributionGetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void ImportLecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var result = _process.ImportDisciplineTimeDistributionsLecturers(new ImportDisciplineTimeDistributions
                {
                    AcademicYearId = _ayId.Value,
                    Path = fbd.SelectedPath
                });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При выгрузке возникла ошибка: ", result.Errors);
                }
                else
                {
                    MessageBox.Show("Готово", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}