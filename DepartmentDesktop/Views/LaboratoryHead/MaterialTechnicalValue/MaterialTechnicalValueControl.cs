﻿using DepartmentDesktop.Models;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.LaboratoryHead.MaterialTechnicalValue
{
    public partial class MaterialTechnicalValueControl : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueService _service;

        public MaterialTechnicalValueControl(IMaterialTechnicalValueService service)
        {
            InitializeComponent();
            _service = service;

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

        public void LoadData()
        {
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel { PageNumber = pageNumber, PageSize = pageSize });
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
                    res.Classroom,
                    res.DateInclude.ToShortDateString(),
                    res.InventoryNumber,
                    res.FullName,
                    res.Location,
                    res.Cost
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<MaterialTechnicalValueForm>(
                new ParameterOverrides
                {
                    { "id", Guid.Empty }
                }
                .OnType<MaterialTechnicalValueForm>());
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
                var form = Container.Resolve<MaterialTechnicalValueForm>(
                    new ParameterOverrides
                    {
                        { "id", id }
                    }
                    .OnType<MaterialTechnicalValueForm>());
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
                        var result = _service.DeleteMaterialTechnicalValue(new MaterialTechnicalValueRecordBindingModel { Id = id });
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
