using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AcademicYearControlsAndForms.DisciplineTimeDistributionRecord
{
    public partial class ControlDisciplineTimeDistributionRecord : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineTimeDistributionRecordService _service;

        private Guid _dtdId;

        public ControlDisciplineTimeDistributionRecord(IDisciplineTimeDistributionRecordService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "DisciplineTimeDistribution", Title = "Расчасовка", Width = 400, Visible = true },
                new ColumnConfig { Name = "TimeNorm", Title = "Норма времени", Width = 150, Visible = true },
                new ColumnConfig { Name = "WeekNumber", Title = "Номер недели", Width = 150, Visible = true },
                new ColumnConfig { Name = "Hours", Title = "Часы", Width = 100, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripButtonAdd", "toolStripDropDownButtonMoves" };

            standartControl.Configurate(columns, hideToolStripButtons);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.DataGridViewListEventCellDoubleClickAddEvent((object sender, DataGridViewCellEventArgs e) => { UpdRecord(); });
            standartControl.DataGridViewListEventKeyDownAddEvent((object sender, KeyEventArgs e) => {
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

        public void LoadData(Guid dtdId)
        {
            _dtdId = dtdId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetDisciplineTimeDistributionRecords(new DisciplineTimeDistributionRecordGetBindingModel { PageNumber = pageNumber, PageSize = pageSize, DisciplineTimeDistributionId = _dtdId });
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
                    res.DisciplineTimeDistribution,
                    res.TimeNorm,
                    res.WeekNumber,
                    res.Hours
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormDisciplineTimeDistributionRecord>(
                new ParameterOverrides
                {
                    { "dtdId", _dtdId },
                    { "id", Guid.Empty }
                }
                .OnType<FormDisciplineTimeDistributionRecord>());
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
                var form = Container.Resolve<FormDisciplineTimeDistributionRecord>(
                    new ParameterOverrides
                    {
                        { "dtdId", _dtdId },
                        { "id", id }
                    }
                    .OnType<FormDisciplineTimeDistributionRecord>());
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
                        var result = _service.DeleteDisciplineTimeDistributionRecord(new DisciplineTimeDistributionRecordGetBindingModel { Id = id });
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