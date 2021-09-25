using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace AcademicYearControlsAndForms.Services
{
    public partial class FormChangeAPRcs : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicYearProcess _process;

        private Guid _aprId;

        private Guid _id;

        public FormChangeAPRcs(IAcademicYearProcess process, Guid aprId, Guid id)
        {
            InitializeComponent();
            _aprId = aprId;
            _id = id;
            _process = process;
        }

        private void FormChangeAPRcs_Load(object sender, EventArgs e)
        {
            if (_aprId == null)
            {
                MessageBox.Show("Неуказана запись учебного плана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultAPR = _process.GetOtherAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { Id = _aprId });
            if (!resultAPR.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке записей учебных планов возникла ошибка: ", resultAPR.Errors);
                return;
            }

            comboBoxAcademicPlanRecord.ValueMember = "Value";
            comboBoxAcademicPlanRecord.DisplayMember = "Display";
            comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.Discipline }).ToList();
            comboBoxAcademicPlanRecord.SelectedValue = _aprId;
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            if (comboBoxAcademicPlanRecord.SelectedValue != null)
            {
                var result = _process.ChangeAPRFromAPRE(new AcademicPlanRecordElementGetBindingModel
                {
                    Id = _id,
                    AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString())
                });

                if (result.Succeeded)
                {
                    MessageBox.Show("Изменено", "Портал", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                }
            }
        }
    }
}
