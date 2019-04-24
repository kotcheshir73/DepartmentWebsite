using AcademicYearControlsAndForms.AcademicPlanRecordElement;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.AcademicPlanRecord
{
    public partial class FormAcademicPlanRecord : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanRecordService _service;

        private Guid _apId;

        public FormAcademicPlanRecord(IAcademicPlanRecordService service, Guid apId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _apId = apId;
        }

        private void FormAcademicPlanRecord_Load(object sender, EventArgs e)
        {
            if (_apId == null)
            {
                MessageBox.Show("Неуказан учебный план", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var resultAP = _service.GetAcademicPlans(new AcademicPlanGetBindingModel { Id = _apId });
            if (!resultAP.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных планов возникла ошибка: ", resultAP.Errors);
                return;
            }

            var resultD = _service.GetDisciplines(new DisciplineGetBindingModel { });
            if (!resultD.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке дисциплин возникла ошибка: ", resultD.Errors);
                return;
            }

            var resultC = _service.GetContingents(new ContingentGetBindingModel { AcademicPlanId = _apId });
            if (!resultC.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке контингента возникла ошибка: ", resultC.Errors);
                return;
            }

            foreach (var elem in Enum.GetValues(typeof(Semesters)))
            {
                comboBoxSemester.Items.Add(elem.ToString());
            }

            comboBoxAcademicPlan.ValueMember = "Value";
            comboBoxAcademicPlan.DisplayMember = "Display";
            comboBoxAcademicPlan.DataSource = resultAP.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.EducationDirection + "/" + ap.AcademicYear }).ToList();
            comboBoxAcademicPlan.SelectedValue = _apId;

            comboBoxDiscipline.ValueMember = "Value";
            comboBoxDiscipline.DisplayMember = "Display";
            comboBoxDiscipline.DataSource = resultD.Result.List
                .Select(d => new { Value = d.Id, Display = d.DisciplineName }).ToList();
            comboBoxDiscipline.SelectedItem = null;

            comboBoxContingent.ValueMember = "Value";
            comboBoxContingent.DisplayMember = "Display";
            comboBoxContingent.DataSource = resultC.Result.List
                .Select(c => new { Value = c.Id, Display = c.ContingentName }).ToList();
            comboBoxContingent.SelectedItem = null;

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlAcademicPlanRecordElement>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlAcademicPlanRecordElement).LoadData(_id.Value);

            var result = _service.GetAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicPlan.SelectedValue = entity.AcademicPlanId;
            comboBoxDiscipline.SelectedValue = entity.DisciplineId;
            if (entity.ContingentId.HasValue)
            {
                comboBoxContingent.SelectedValue = entity.ContingentId;
            }
            if (!string.IsNullOrEmpty(entity.Semester))
            {
                comboBoxSemester.SelectedIndex = comboBoxSemester.Items.IndexOf(entity.Semester);
            }
            textBoxZet.Text = entity.Zet.ToString();
        }

        private bool CheckFill()
        {
            if (comboBoxAcademicPlan.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxDiscipline.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxZet.Text))
            {
                return false;
            }
            if (!int.TryParse(textBoxZet.Text, out int zet))
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                    {
                        AcademicPlanId = new Guid(comboBoxAcademicPlan.SelectedValue.ToString()),
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        ContingentId = comboBoxContingent.SelectedValue != null ? new Guid(comboBoxContingent.SelectedValue.ToString()) : (Guid?)null,
                        Semester = comboBoxSemester.Text,
                        Zet = Convert.ToInt32(textBoxZet.Text)
                    });
                }
                else
                {
                    result = _service.UpdateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                    {
                        Id = _id.Value,
                        AcademicPlanId = new Guid(comboBoxAcademicPlan.SelectedValue.ToString()),
                        DisciplineId = new Guid(comboBoxDiscipline.SelectedValue.ToString()),
                        ContingentId = comboBoxContingent.SelectedValue != null ? new Guid(comboBoxContingent.SelectedValue.ToString()) : (Guid?)null,
                        Semester = comboBoxSemester.Text,
                        Zet = Convert.ToInt32(textBoxZet.Text)
                    });
                }
                if (result.Succeeded)
                {
                    if (result.Result != null)
                    {
                        if (result.Result is Guid)
                        {
                            _id = (Guid)result.Result;
                        }
                    }
                    return true;
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}