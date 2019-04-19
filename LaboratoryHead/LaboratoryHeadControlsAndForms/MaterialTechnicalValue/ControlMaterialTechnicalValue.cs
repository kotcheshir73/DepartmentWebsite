using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using LaboratoryHeadControlsAndForms.Reports;
using LaboratoryHeadControlsAndForms.Services;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace LaboratoryHeadControlsAndForms.MaterialTechnicalValue
{
    public partial class ControlMaterialTechnicalValue : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueService _service;

        private readonly ILaboratoryProcess _process;

        public ControlMaterialTechnicalValue(IMaterialTechnicalValueService service, ILaboratoryProcess process)
        {
            InitializeComponent();
            _service = service;
            _process = process;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 150, Visible = false },
                new ColumnConfig { Name = "Classroom", Title = "Аудитория", Width = 100, Visible = true },
                new ColumnConfig { Name = "DateInclude", Title = "Дата принятия", Width = 150, Visible = true },
                new ColumnConfig { Name = "InventoryNumber", Title = "Инв. номер", Width = 150, Visible = true },
                new ColumnConfig { Name = "FullName", Title = "Наименование", Width = 250, Visible = true },
                new ColumnConfig { Name = "Location", Title = "Расположение", Width = 200, Visible = true },
                new ColumnConfig { Name = "Cost", Title = "Цена", Width = 150, Visible = true }
            };

            List<string> hideToolStripButtons = new List<string>();

            Dictionary<string, string> buttonsToMoveButton = new Dictionary<string, string>
                {
                    { "MakeCloneToolStripMenuItem", "Создать дубликат"},
                    { "ApplyTMVRecrodsToolStripMenuItem", "Применить характеристики ТМЦ на другие ТМЦ"},
                    { "PrintReportToolStripMenuItem", "Распечатать по аудитории"},
                    { "PrintPassportToolStripMenuItem", "Технический паспорт"}
                };

            standartControl.Configurate(columns, hideToolStripButtons, countElementsOnPage: 30, controlOnMoveElem: buttonsToMoveButton);

            standartControl.GetPageAddEvent(LoadRecords);
            standartControl.ToolStripButtonAddEventClickAddEvent((object sender, EventArgs e) => { AddRecord(); });
            standartControl.ToolStripButtonUpdEventClickAddEvent((object sender, EventArgs e) => { UpdRecord(); });
            standartControl.ToolStripButtonDelEventClickAddEvent((object sender, EventArgs e) => { DelRecord(); });
            standartControl.ToolStripButtonMoveEventClickAddEvent("MakeCloneToolStripMenuItem", LoadFromXMLToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("ApplyTMVRecrodsToolStripMenuItem", ApplyTMVRecrodsToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("PrintReportToolStripMenuItem", ShowReportFormToolStripMenuItem_Click);
            standartControl.ToolStripButtonMoveEventClickAddEvent("PrintPassportToolStripMenuItem", PrintPassportToolStripMenuItem_Click);
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
            var result = _service.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
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
                    res.Classroom,
                    res.DateInclude.ToShortDateString(),
                    res.InventoryNumber,
                    res.FullName,
                    res.Location,
                    res.Cost.ToString("N2")
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormMaterialTechnicalValue>(
                new ParameterOverrides
                {
                    { "id", Guid.Empty }
                }
                .OnType<FormMaterialTechnicalValue>());
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
                var form = Container.Resolve<FormMaterialTechnicalValue>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<FormMaterialTechnicalValue>());
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
                        var result = _service.DeleteMaterialTechnicalValue(new MaterialTechnicalValueSetBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void LoadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите создать копию?", "Создание дубликата", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < standartControl.GetDataGridViewSelectedRows.Count; ++i)
                    {
                        Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[i].Cells[0].Value.ToString());
                        var result = _process.MakeClone(new LaboratoryProcessMakeCloneBindingModel { Id = id });
                        if (!result.Succeeded)
                        {
                            ErrorMessanger.PrintErrorMessage("При клонировании возникла ошибка: ", result.Errors);
                        }
                    }
                    standartControl.LoadPage();
                }
            }
        }

        private void ApplyTMVRecrodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы уверены, что хотите применить?", "Создание дубликата", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                    var form = Container.Resolve<FormMaterialTechnicalValueApplyTMCRecrods>(
                        new ParameterOverrides
                        {
                        { "id", id }
                        }
                        .OnType<FormMaterialTechnicalValueApplyTMCRecrods>());
                    form.ShowDialog();
                }
            }
        }

        private void ShowReportFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ReportMaterialTechnicalValueByClassroom>();
            form.Show();
        }

        private void PrintPassportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standartControl.GetDataGridViewSelectedRows.Count > 0)
            {
                Guid id = new Guid(standartControl.GetDataGridViewSelectedRows[0].Cells[0].Value.ToString());
                var form = Container.Resolve<ReportMaterialTechnicalValuePassport>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<ReportMaterialTechnicalValuePassport>());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    standartControl.LoadPage();
                }
            }
        }
    }
}