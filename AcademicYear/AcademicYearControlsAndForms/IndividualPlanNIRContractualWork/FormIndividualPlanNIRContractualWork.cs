using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.IndividualPlanNIRContractualWork
{
    public partial class FormIndividualPlanNIRContractualWork : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IIndividualPlanNIRContractualWorkService _service;

        private Guid _ipId;

        public FormIndividualPlanNIRContractualWork(IIndividualPlanNIRContractualWorkService service, Guid ipId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ipId = ipId;
        }

        private void FormIndividualPlanNIRContractualWork_Load(object sender, EventArgs e)
        {
            if (_ipId == null)
            {
                MessageBox.Show("Не указан индивидуальный план", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var resultIP = _service.GetIndividualPlans(new IndividualPlanGetBindingModel { Id = _ipId });
            if (!resultIP.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке индивидуальных планов возникла ошибка: ", resultIP.Errors);
                return;
            }

            comboBoxIndividualPlan.ValueMember = "Value";
            comboBoxIndividualPlan.DisplayMember = "Display";
            comboBoxIndividualPlan.DataSource = resultIP.Result.List
                .Select(x => new { Value = x.Id, Display = x.AcademicYearsTitle + " - " + x.LecturerName }).ToList();
            comboBoxIndividualPlan.SelectedValue = _ipId;

            StandartForm_Load();
        }

        protected override void LoadData()
        {
            var result = _service.GetIndividualPlanNIRContractualWork(new IndividualPlanNIRContractualWorkGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxIndividualPlan.SelectedValue = entity.IndividualPlanId;
            textBoxJobContent.Text = entity.JobContent;
            textBoxPost.Text = entity.Post;
            textBoxPlannedTerm.Text = entity.PlannedTerm;
            checkBoxReadyMark.Checked = entity.ReadyMark;
        }

        private bool CheckFill()
        {
            if (comboBoxIndividualPlan.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxJobContent.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPost.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPlannedTerm.Text))
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
                    result = _service.CreateIndividualPlanNIRContractualWork(new IndividualPlanNIRContractualWorkSetBindingModel
                    {
                        IndividualPlanId = new Guid(comboBoxIndividualPlan.SelectedValue.ToString()),
                        JobContent = textBoxJobContent.Text,
                        Post = textBoxPost.Text,
                        PlannedTerm = textBoxPlannedTerm.Text,
                        ReadyMark = checkBoxReadyMark.Checked
                    });
                }
                else
                {
                    result = _service.UpdateIndividualPlanNIRContractualWork(new IndividualPlanNIRContractualWorkSetBindingModel
                    {
                        Id = _id.Value,
                        IndividualPlanId = new Guid(comboBoxIndividualPlan.SelectedValue.ToString()),
                        JobContent = textBoxJobContent.Text,
                        Post = textBoxPost.Text,
                        PlannedTerm = textBoxPlannedTerm.Text,
                        ReadyMark = checkBoxReadyMark.Checked
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