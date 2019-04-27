using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace AcademicYearControlsAndForms.IndividualPlanRecord
{
    public partial class ControlIndividualPlanRecord : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IIndividualPlanRecordService _service;

        private Guid _ipId;

        public ControlIndividualPlanRecord(IIndividualPlanRecordService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "IndividualPlanName", Title = "Индивидуальный план", Width = 200, Visible = true },
                new ColumnConfig { Name = "IndividualPlanKindOfWorkName", Title = "Вид работ", Width = 200, Visible = true },
                new ColumnConfig { Name = "PlanAutumn", Title = "Осенний семестр по плану", Width = 200, Visible = true },
                new ColumnConfig { Name = "FactAutumn", Title = "Осенний семестр по факту", Width = 200, Visible = true },
                new ColumnConfig { Name = "PlanSpring", Title = "Весенний семестр по плану", Width = 200, Visible = true },
                new ColumnConfig { Name = "FactSpring", Title = "Весенний семестр по факту", Width = 200, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string> { "toolStripDropDownButtonMoves" };

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

        public void LoadData(Guid ipId)
        {
            _ipId = ipId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetIndividualPlanRecords(new IndividualPlanRecordGetBindingModel { PageNumber = pageNumber, PageSize = pageSize, IndividualPlanId = _ipId });
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
                    res.IndividualPlanName,
                    res.IndividualPlanKindOfWorkName,
                    res.PlanAutumn,
                    res.FactAutumn,
                    res.PlanSpring,
                    res.FactSpring
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormIndividualPlanRecord>(
                new ParameterOverrides
                {
                    { "ipId", _ipId },
                    { "id", Guid.Empty }
                }
                .OnType<FormIndividualPlanRecord>());
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
                var form = Container.Resolve<FormIndividualPlanRecord>(
                    new ParameterOverrides
                    {
                        { "ipId", _ipId },
                        { "id", id }
                    }
                    .OnType<FormIndividualPlanRecord>());
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
                        var result = _service.DeleteIndividualPlanRecord(new IndividualPlanRecordGetBindingModel { Id = id });
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