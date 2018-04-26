using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    public partial class SoftwareRecordAddClaimForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareRecordService _service;

        private readonly IMaterialTechnicalValueService _serviceMTV;

        public SoftwareRecordAddClaimForm(ISoftwareRecordService service, IMaterialTechnicalValueService serviceMTV)
        {
            InitializeComponent();
            _service = service;
            _serviceMTV = serviceMTV;
        }

        private void buttonInventoryNumberSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxInventoryNumberSearch.Text))
            {
                var findRecords = _serviceMTV.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel { InventoryNumber = textBoxInventoryNumberSearch.Text });
                if (findRecords.Succeeded)
                {
                    dataGridViewFindInventoryNumbers.Rows.Clear();
                    foreach (var rec in findRecords.Result.List)
                    {
                        dataGridViewFindInventoryNumbers.Rows.Add(new object[] { rec.Id, rec.InventoryNumber });
                    }
                }
            }
        }

        private void buttonAddInvenotyNumbers_Click(object sender, EventArgs e)
        {
            if (dataGridViewFindInventoryNumbers.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridViewFindInventoryNumbers.SelectedRows.Count; ++i)
                {
                    dataGridViewSelectedInventoryNumbers.Rows.Add(new object[]
                        {
                            Guid.Parse(dataGridViewFindInventoryNumbers.SelectedRows[i].Cells[0].Value.ToString()),
                            dataGridViewFindInventoryNumbers.SelectedRows[i].Cells[1].Value.ToString()
                        });
                }
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxInventoryNumberSearch.Text))
            {
                MessageBox.Show("Не заполнен номер заявки", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridViewSelectedInventoryNumbers.Rows.Count == 0)
            {
                MessageBox.Show("Не выбран ни один инв. номер", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridViewSoftware.Rows.Count == 1)
            {
                MessageBox.Show("Не добавлена ни одна запись по добавляемому ПО", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < dataGridViewSoftware.Rows.Count - 1; ++i)
            {
                string softWareName = dataGridViewSoftware.Rows[i].Cells[0].Value.ToString();
                string softWareKey = dataGridViewSoftware.Rows[i].Cells[1].Value?.ToString();
                string softWareK = dataGridViewSoftware.Rows[i].Cells[2].Value?.ToString();
                for (int j = 0; j < dataGridViewSelectedInventoryNumbers.Rows.Count; ++j)
                {
                    ResultService result = _service.CreateSoftwareRecord(new SoftwareRecordRecordBindingModel
                    {
                        MaterialTechnicalValueId = new Guid(dataGridViewSelectedInventoryNumbers.Rows[j].Cells[0].Value.ToString()),
                        DateSetup = dateTimePickerDateSetup.Value,
                        SoftwareName = softWareName,
                        SoftwareKey = softWareKey,
                        SoftwareK = softWareK,
                        ClaimNumber = textBoxClaimNumber.Text
                    });
                    if (!result.Succeeded)
                    {
                        Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    }
                }
            }
            MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
