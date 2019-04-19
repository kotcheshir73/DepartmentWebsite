﻿using ControlsAndForms.Messangers;
using ControlsAndForms.Models;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Resolution;

namespace LaboratoryHeadControlsAndForms.MaterialTechnicalValueRecord
{
    public partial class ControlMaterialTechnicalValueRecord : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueRecordService _service;

        private Guid _mtvId;

        public ControlMaterialTechnicalValueRecord(IMaterialTechnicalValueRecordService service)
        {
            InitializeComponent();
            _service = service;

            List<ColumnConfig> columns = new List<ColumnConfig>
            {
                new ColumnConfig { Name = "Id", Title = "Id", Width = 100, Visible = false },
                new ColumnConfig { Name = "GroupName", Title = "Группа описаний", Width = 150, Visible = true },
                new ColumnConfig { Name = "FieldName", Title = "Название", Width = 200, Visible = true },
                new ColumnConfig { Name = "FieldValue", Title = "Значение", Width = 200, Visible = true },
                new ColumnConfig { Name = "Order", Title = "Порядковый номер", Width = 150, Visible = true }
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

        public void LoadData(Guid mtvId)
        {
            _mtvId = mtvId;
            standartControl.LoadPage();
        }

        private int LoadRecords(int pageNumber, int pageSize)
        {
            var result = _service.GetMaterialTechnicalValueRecords(new MaterialTechnicalValueRecordGetBindingModel
            {
                MaterialTechnicalValueId = _mtvId,
                PageNumber = pageNumber,
                PageSize = pageSize
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
                    res.GroupName,
                    res.FieldName,
                    res.FieldValue,
                    res.Order
                );
            }
            return result.Result.MaxCount;
        }

        private void AddRecord()
        {
            var form = Container.Resolve<FormMaterialTechnicalValueRecord>(
                new ParameterOverrides
                {
                    { "mtvId", _mtvId },
                    { "id", Guid.Empty }
                }
                .OnType<FormMaterialTechnicalValueRecord>());
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
                var form = Container.Resolve<FormMaterialTechnicalValueRecord>(
                    new ParameterOverrides
                    {
                        { "mtvId", _mtvId },
                        { "id", id }
                    }
                    .OnType<FormMaterialTechnicalValueRecord>());
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
                        var result = _service.DeleteMaterialTechnicalValueRecord(new MaterialTechnicalValueRecordSetBindingModel { Id = id });
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