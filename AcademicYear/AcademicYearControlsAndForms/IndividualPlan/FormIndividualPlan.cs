using AcademicYearControlsAndForms.IndividualPlanNIRContractualWork;
using AcademicYearControlsAndForms.IndividualPlanNIRScientificArticle;
using AcademicYearControlsAndForms.IndividualPlanRecord;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.IndividualPlan
{
    public partial class FormIndividualPlan : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IIndividualPlanService _service;

        private Guid _ayId;

        public FormIndividualPlan(IIndividualPlanService service, Guid ayId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
        }

        protected override bool LoadComponents()
        {
            if (_ayId == null)
            {
                MessageBox.Show("Не указан учебный год", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return false;
            }
            var resultL = _service.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return false;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(x => new { Value = x.Id, Display = x.Title }).ToList();
            comboBoxAcademicYear.SelectedValue = _ayId;

            comboBoxLecturer.ValueMember = "Value";
            comboBoxLecturer.DisplayMember = "Display";
            comboBoxLecturer.DataSource = resultL.Result.List
                .Select(x => new { Value = x.Id, Display = x.FullName }).ToList();
            comboBoxLecturer.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlIndividualPlanRecord>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlIndividualPlanRecord).LoadData(_id.Value);


            if (tabPageNIRContractualWork.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlIndividualPlanNIRContractualWork>();
                control.Dock = DockStyle.Fill;
                tabPageNIRContractualWork.Controls.Add(control);
            }
            (tabPageNIRContractualWork.Controls[0] as ControlIndividualPlanNIRContractualWork).LoadData(_id.Value);


            if (tabPageNIRScientificArticle.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlIndividualPlanNIRScientificArticle>();
                control.Dock = DockStyle.Fill;
                tabPageNIRScientificArticle.Controls.Add(control);
            }
            (tabPageNIRScientificArticle.Controls[0] as ControlIndividualPlanNIRScientificArticle).LoadData(_id.Value);

            var result = _service.GetIndividualPlan(new IndividualPlanGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
            comboBoxLecturer.SelectedValue = entity.LecturerId;
        }

        protected override bool CheckFill()
        {
            if (comboBoxAcademicYear.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxLecturer.SelectedValue == null)
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
                result = _service.CreateIndividualPlan(new IndividualPlanSetBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString())
                });
            }
            else
            {
                result = _service.UpdateIndividualPlan(new IndividualPlanSetBindingModel
                {
                    Id = _id.Value,
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString())
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