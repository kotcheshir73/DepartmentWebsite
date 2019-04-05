using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    public partial class SoftwareRecordControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareRecordService _service;

        private readonly ILaboratoryProcess _process;

        public SoftwareRecordControl(ISoftwareRecordService service, ILaboratoryProcess process)
        {
            InitializeComponent();
            _service = service;
            _process = process;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 150, Visible = false },
                new ColumnConfig { Name = "InventoryNumber", Title = "Инв. номер", Width = 100, Visible = true },
                new ColumnConfig { Name = "SoftwareName", Title = "Название ПО", Width = 250, Visible = true },
                new ColumnConfig { Name = "DateSetup", Title = "Дата устанвоки", Width = 150, Visible = true },
                new ColumnConfig { Name = "SetupDescription", Title = "Особенности установки", Width = 350, Visible = true },
                new ColumnConfig { Name = "SoftwareKey", Title = "Ключ", Width = 250, Visible = true },
                new ColumnConfig { Name = "ClaimNumber", Title = "Номер заявки", Width = 200, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "UpdateSoftwareRecordsToolStripMenuItem", "Изменить подобные"},
                    { "InstallSoftwareToolStripMenuItem", "Установить ПО"},
                    { "UnInstallSowtwareToolStripMenuItem", "Деинсталяция ПО"},
                    { "PrintReportForClassroomToolStripMenuItem", "Получить список ПО по аудитории"},
                    { "PrintReportForClaimToolStripMenuItem", "Получить список ПО по заявке"},
                    { "PrintReportForInventoryNumberToolStripMenuItem", "Получить список ПО по инв. номеру"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("UpdateSoftwareRecordsToolStripMenuItem", UpdateSoftwareRecordsToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("InstallSoftwareToolStripMenuItem", InstallSoftwareToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("UnInstallSowtwareToolStripMenuItem", UnInstallSowtwareToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("PrintReportForClassroomToolStripMenuItem", PrintReportForClassroomToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("PrintReportForClaimToolStripMenuItem", PrintReportForClaimToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("PrintReportForInventoryNumberToolStripMenuItem", PrintReportForInventoryNumberToolStripMenuItem_Click);
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

        public void LoadData()
        {
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetSoftwareRecords(new SoftwareRecordGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
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
                    res.InventoryNumber,
                    res.SoftwareName,
                    res.DateSetup.ToShortDateString(),
                    res.SetupDescription,
                    res.SoftwareKey,
                    res.ClaimNumber
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<SoftwareRecordForm>(
                new ParameterOverrides
                {
                    { "id", Guid.Empty }
                }
                .OnType<SoftwareRecordForm>());
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
                var form = Container.Resolve<SoftwareRecordForm>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<SoftwareRecordForm>());
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
                        var result = _service.DeleteSoftwareRecord(new SoftwareRecordSetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void UpdateSoftwareRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите изменить другие записи?", "Пакетное изменение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _process.ApplyInfoByAnotherSoftwareReocrds(new LaboratoryProcessApplyInfoByAnotherSoftwareReocrdsBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            Program.PrintErrorMessage("При изменении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void InstallSoftwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<InstallSowtwareForm>();
            form.Show();
        }

        private void UnInstallSowtwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<UnInstallSowtwareForm>();
            form.Show();
        }

        private void PrintReportForClassroomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ReportSoftwareRecordsByClassroom>();
            form.Show();
        }

        private void PrintReportForClaimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ReportSoftwareRecordsByClaim>();
            form.Show();
        }

        private void PrintReportForInventoryNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ReportSoftwareRecordsByInventoryNumber>();
            form.Show();
        }
    }
}
