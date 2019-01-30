using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LaboratoryHead.SoftwareRecord
{
    public partial class UnInstallSowtwareForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareService _serviceS;

        private readonly IMaterialTechnicalValueService _serviceMTV;

        private readonly ILaboratoryProcess _process;

        public UnInstallSowtwareForm(ISoftwareService serviceS, IMaterialTechnicalValueService serviceMTV, ILaboratoryProcess process)
        {
            InitializeComponent();
            _serviceS = serviceS;
            _serviceMTV = serviceMTV;
            _process = process;
        }

        private void buttonInventoryNumberSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxInventoryNumberSearch.Text))
            {
                var findRecords = _serviceMTV.GetMaterialTechnicalValues(new MaterialTechnicalValueGetBindingModel { InventoryNumber = textBoxInventoryNumberSearch.Text });
                if (findRecords.Succeeded)
                {
                    checkedListBoxInvNumbers.Items.Clear();
                    foreach (var rec in findRecords.Result.List)
                    {
                        checkedListBoxInvNumbers.Items.Add(rec.InventoryNumber, true);
                    }
                }
            }
        }

        private void buttonSoftwareSearch_Click(object sender, EventArgs e)
        {
            List<string> invNumbers = new List<string>();
            foreach (var elem in checkedListBoxInvNumbers.CheckedItems)
            {
                invNumbers.Add(elem.ToString());
            }

            var result = _process.GetSoftwareByInvNumbers(new LaboratoryProcessGetSoftwareByInvNumbersBindingModel
            {
                InventoryNumbers = invNumbers
            });

            if (result.Succeeded)
            {
                checkedListBoxSoftwares.Items.Clear();
                foreach (var rec in result.Result.List)
                {
                    checkedListBoxSoftwares.Items.Add(string.Format("{0};{1}", rec.SoftwareName, rec.SoftwareKey), false);
                }
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            List<string> softs = new List<string>();
            foreach (var elem in checkedListBoxSoftwares.CheckedItems)
            {
                softs.Add(elem.ToString());
            }

            List<string> invNumbers = new List<string>();
            foreach (var elem in checkedListBoxInvNumbers.CheckedItems)
            {
                invNumbers.Add(elem.ToString());
            }

            ResultService result = _process.UnInstallSoftware(new LaboratoryProcessUnInstalSoftwareBindingModel
            {
                SoftwareNames = softs,
                InventoryNumbers = invNumbers,
                DateDelete = dateTimePickerDateDelete.Value,
                DeleteReason = textBoxDeleteReason.Text
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
            else
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
