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
    public partial class FormDuplicate : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanService _service;

        private readonly IAcademicYearProcess _process;

        public FormDuplicate(IAcademicPlanService service, IAcademicYearProcess process)
        {
            InitializeComponent();
            _service = service;
            _process = process;
        }

        private void FormDuplicate_Load(object sender, EventArgs e)
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxFromAcademicYear.ValueMember = "Value";
            comboBoxFromAcademicYear.DisplayMember = "Display";
            comboBoxFromAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxFromAcademicYear.SelectedItem = null;

            comboBoxToAcademicYear.ValueMember = "Value";
            comboBoxToAcademicYear.DisplayMember = "Display";
            comboBoxToAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxToAcademicYear.SelectedItem = null;
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (comboBoxFromAcademicYear.SelectedValue == null)
            {
                MessageBox.Show("Не выбран учебный год - источник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxToAcademicYear.SelectedValue == null)
            {
                MessageBox.Show("Не выбран учебный год - приемник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = _process.DuplicateAcademicYearElements(new EducationalProcessDuplicateAcademicYear
            {
                FromAcademicPlanId = new Guid(comboBoxFromAcademicYear.SelectedValue.ToString()),
                ToAcademicPlanId = new Guid(comboBoxToAcademicYear.SelectedValue.ToString()),
                DuplicateAcademicPlan = checkBoxAcademicPlan.Checked,
                DuplicateContingent = checkBoxContingent.Checked,
                DuplicateSeasonDate = checkBoxSeasonDate.Checked,
                DuplicateTimeNorm = checkBoxTimeNorm.Checked
            });
            if (result.Succeeded)
            {
                MessageBox.Show("Сделано", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При выполнении произошла ошибка: ", result.Errors);
            }
        }
    }
}