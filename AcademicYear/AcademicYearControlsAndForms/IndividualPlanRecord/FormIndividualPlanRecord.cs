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

namespace AcademicYearControlsAndForms.IndividualPlanRecord
{
    public partial class FormIndividualPlanRecord : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IIndividualPlanRecordService _service;

        private Guid _ipId;

        public FormIndividualPlanRecord(IIndividualPlanRecordService service, Guid ipId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ipId = ipId;
        }

        protected override bool LoadComponents()
        {
            if (_ipId == null)
            {
                MessageBox.Show("Не указан индивидуальный план", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var resultIP = _service.GetIndividualPlans(new IndividualPlanGetBindingModel { Id = _ipId });
            if (!resultIP.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке индивидуальных планов возникла ошибка: ", resultIP.Errors);
                return false;
            }
            var resultIPKOF = _service.GetIndividualPlanKindOfWorks(new IndividualPlanKindOfWorkGetBindingModel { Id = _ipId });
            if (!resultIPKOF.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке видов работ возникла ошибка: ", resultIPKOF.Errors);
                return false;
            }

            comboBoxIndividualPlan.ValueMember = "Value";
            comboBoxIndividualPlan.DisplayMember = "Display";
            comboBoxIndividualPlan.DataSource = resultIP.Result.List
                .Select(x => new { Value = x.Id, Display = x.AcademicYearsTitle + " - " + x.LecturerName }).ToList();
            comboBoxIndividualPlan.SelectedValue = _ipId;

            comboBoxIndividualPlanKindOfWork.ValueMember = "Value";
            comboBoxIndividualPlanKindOfWork.DisplayMember = "Display";
            comboBoxIndividualPlanKindOfWork.DataSource = resultIPKOF.Result.List
                .Select(x => new { Value = x.Id, Display = x.Name }).ToList();
            comboBoxIndividualPlanKindOfWork.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetIndividualPlanRecord(new IndividualPlanRecordGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxIndividualPlan.SelectedValue = entity.IndividualPlanId;
            comboBoxIndividualPlanKindOfWork.SelectedValue = entity.IndividualPlanKindOfWorkId;
            textBoxPlanAutumn.Text = entity.PlanAutumn.ToString();
            textBoxFactAutumn.Text = entity.FactAutumn.ToString();
            textBoxPlanSpring.Text = entity.PlanSpring.ToString();
            textBoxFactSpring.Text = entity.FactSpring.ToString();
        }

        protected override bool CheckFill()
        {
            if (comboBoxIndividualPlan.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxIndividualPlanKindOfWork.SelectedValue == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxPlanAutumn.Text))
            {
                if (!double.TryParse(textBoxPlanAutumn.Text, out double plan))
                {
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(textBoxFactAutumn.Text))
            {
                if (!double.TryParse(textBoxFactAutumn.Text, out double fact))
                {
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(textBoxPlanSpring.Text))
            {
                if (!double.TryParse(textBoxPlanSpring.Text, out double plan))
                {
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(textBoxFactSpring.Text))
            {
                if (!double.TryParse(textBoxFactSpring.Text, out double fact))
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateIndividualPlanRecord(new IndividualPlanRecordSetBindingModel
                {
                    IndividualPlanId = new Guid(comboBoxIndividualPlan.SelectedValue.ToString()),
                    IndividualPlanKindOfWorkId = new Guid(comboBoxIndividualPlanKindOfWork.SelectedValue.ToString()),
                    PlanAutumn = Convert.ToDouble(textBoxPlanAutumn.Text),
                    FactAutumn = Convert.ToDouble(textBoxFactAutumn.Text),
                    PlanSpring = Convert.ToDouble(textBoxPlanSpring.Text),
                    FactSpring = Convert.ToDouble(textBoxFactSpring.Text)
                });
            }
            else
            {
                result = _service.UpdateIndividualPlanRecord(new IndividualPlanRecordSetBindingModel
                {
                    Id = _id.Value,
                    IndividualPlanId = new Guid(comboBoxIndividualPlan.SelectedValue.ToString()),
                    IndividualPlanKindOfWorkId = new Guid(comboBoxIndividualPlanKindOfWork.SelectedValue.ToString()),
                    PlanAutumn = Convert.ToDouble(textBoxPlanAutumn.Text),
                    FactAutumn = Convert.ToDouble(textBoxFactAutumn.Text),
                    PlanSpring = Convert.ToDouble(textBoxPlanSpring.Text),
                    FactSpring = Convert.ToDouble(textBoxFactSpring.Text)
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
    }
}