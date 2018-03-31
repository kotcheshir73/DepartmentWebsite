﻿using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LaboratoryHead.MaterialTechnicalValue
{
    public partial class MaterialTechnicalValueApplyTMCRecrodsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMaterialTechnicalValueService _service;

        private readonly ILaboratoryProcess _process;

        private Guid? _id = null;

        public MaterialTechnicalValueApplyTMCRecrodsForm(IMaterialTechnicalValueService service, ILaboratoryProcess process, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _process = process;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void MaterialTechnicalValueApplyTMCRecrodsForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                var result = _service.GetMaterialTechnicalValue(new MaterialTechnicalValueGetBindingModel { Id = _id.Value });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    Close();
                }
                var entity = result.Result;

                textBoxSelectedInventoryNumber.Text = entity.InventoryNumber;
            }
        }

        private void buttonInventoryNumberSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxInventoryNumberSearch.Text))
            {
                var findRecords = _service.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel { InventoryNumber = textBoxInventoryNumberSearch.Text });
                if (findRecords.Succeeded)
                {
                    dataGridViewFindInventoryNumbers.Rows.Clear();
                    foreach (var rec in findRecords.Result.List)
                    {
                        dataGridViewFindInventoryNumbers.Rows.Add(new object[] { rec.Id, false, rec.InventoryNumber });
                    }
                }
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                List<Guid> seletedIds = new List<Guid>();
                for (int i = 0; i < dataGridViewFindInventoryNumbers.Rows.Count; ++i)
                {
                    if (Convert.ToBoolean(dataGridViewFindInventoryNumbers.Rows[i].Cells[1].Value))
                    {
                        seletedIds.Add(new Guid(dataGridViewFindInventoryNumbers.Rows[i].Cells[0].Value.ToString()));
                    }
                }
                if (seletedIds.Count == 0)
                {
                    MessageBox.Show("Не выбран ни один инв. номер", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ResultService result = _process.ApplyMTVRecords(new LaboratoryProcessApplyMTVRecordsBindingModel
                {
                    Id = _id.Value,
                    ApllyIds = seletedIds
                });
                if (!result.Succeeded)
                {
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                }
                MessageBox.Show("Сделано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}