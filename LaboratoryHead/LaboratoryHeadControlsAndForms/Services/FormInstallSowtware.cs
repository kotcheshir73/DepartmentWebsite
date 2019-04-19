using ControlsAndForms.Messangers;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.IServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tools;
using Unity;

namespace LaboratoryHeadControlsAndForms.Services
{
    public partial class FormInstallSowtware : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISoftwareService _serviceS;

        private readonly IMaterialTechnicalValueService _serviceMTV;

        private readonly ILaboratoryProcess _process;

        public FormInstallSowtware(ISoftwareService serviceS, IMaterialTechnicalValueService serviceMTV, ILaboratoryProcess process)
        {
            InitializeComponent();
            _serviceS = serviceS;
            _serviceMTV = serviceMTV;
            _process = process;
        }

        private void FormInstallSowtware_Load(object sender, EventArgs e)
        {
            var softwares = _serviceS.GetSoftwares(new SoftwareGetBindingModel { });
            if (softwares.Succeeded)
            {
                checkedListBoxSoftwares.Items.Clear();
                foreach (var rec in softwares.Result.List)
                {
                    checkedListBoxSoftwares.Items.Add(string.Format("{0};{1}", rec.SoftwareName, rec.SoftwareKey), false);
                }
            }
        }

        private void ButtonInventoryNumberSearch_Click(object sender, EventArgs e)
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

        private void ButtonApply_Click(object sender, EventArgs e)
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

            ResultService result = _process.InstallSoftware(new LaboratoryProcessInstalSoftwareBindingModel
            {
                SoftwareNames = softs,
                InventoryNumbers = invNumbers,
                DateSetup = dateTimePickerDateSetup.Value,
                SetupDescription = textBoxSetupDescription.Text,
                ClaimNumber = textBoxClaimNumber.Text
            });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
            else
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}