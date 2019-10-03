using AcademicYearControlsAndForms.AcademicPlanRecordMission;
using AcademicYearControlsAndForms.DisciplineTimeDistribution;
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

namespace AcademicYearControlsAndForms.AcademicPlanRecordElement
{
    public partial class FormAcademicPlanRecordElement : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAcademicPlanRecordElementService _service;

        private Guid _aprId;

        public FormAcademicPlanRecordElement(IAcademicPlanRecordElementService service, Guid aprId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _aprId = aprId;
        }

        protected override bool LoadComponents()
        {
            if (_aprId == null)
            {
                MessageBox.Show("Неуказана запись учебного плана", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var resultAPR = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { Id = _aprId });
            if (!resultAPR.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке записей учебных планов возникла ошибка: ", resultAPR.Errors);
                return false;
            }

            var resultTN = _service.GetTimeNorms(new TimeNormGetBindingModel { AcademicPlanRecordId = _aprId });
            if (!resultTN.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке норм времени возникла ошибка: ", resultTN.Errors);
                return false;
            }

            comboBoxAcademicPlanRecord.ValueMember = "Value";
            comboBoxAcademicPlanRecord.DisplayMember = "Display";
            comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
                .Select(ap => new { Value = ap.Id, Display = ap.Disciplne }).ToList();
            comboBoxAcademicPlanRecord.SelectedValue = _aprId;

            comboBoxTimeNorm.ValueMember = "Value";
            comboBoxTimeNorm.DisplayMember = "Display";
            comboBoxTimeNorm.DataSource = resultTN.Result.List
                .Select(d => new { Value = d.Id, Display = d.KindOfLoadName }).ToList();
            comboBoxTimeNorm.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlAcademicPlanRecordMission>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlAcademicPlanRecordMission).LoadData(_id.Value);

            if (tabPageDisciplineTimeDistribution.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlDisciplineTimeDistribution>();
                control.Dock = DockStyle.Fill;
                tabPageDisciplineTimeDistribution.Controls.Add(control);
            }
            (tabPageDisciplineTimeDistribution.Controls[0] as ControlDisciplineTimeDistribution).LoadData(_id.Value, null);

            var result = _service.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicPlanRecord.SelectedValue = entity.AcademicPlanRecordId;
            comboBoxTimeNorm.SelectedValue = entity.TimeNormId;
            textBoxPlanHours.Text = entity.PlanHours.ToString();
            textBoxFactHours.Text = entity.FactHours.ToString();
        }

        protected override bool CheckFill()
        {
            if (comboBoxAcademicPlanRecord.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxTimeNorm.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxPlanHours.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxFactHours.Text))
            {
                return false;
            }
            if (!decimal.TryParse(textBoxPlanHours.Text, out decimal hours))
            {
                return false;
            }
            if (!decimal.TryParse(textBoxFactHours.Text, out hours))
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                {
                    AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                    TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                    PlanHours = Convert.ToDecimal(textBoxPlanHours.Text),
                    FactHours = Convert.ToDecimal(textBoxFactHours.Text)
                });
            }
            else
            {
                result = _service.UpdateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                {
                    Id = _id.Value,
                    AcademicPlanRecordId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString()),
                    TimeNormId = new Guid(comboBoxTimeNorm.SelectedValue.ToString()),
                    PlanHours = Convert.ToDecimal(textBoxPlanHours.Text),
                    FactHours = Convert.ToDecimal(textBoxFactHours.Text)
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