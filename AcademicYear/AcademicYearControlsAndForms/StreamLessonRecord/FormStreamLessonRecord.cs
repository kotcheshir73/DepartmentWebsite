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

namespace AcademicYearControlsAndForms.StreamLessonRecord
{
    public partial class FormStreamLessonRecord : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStreamLessonRecordService _service;

        private Guid _sId;

        private Guid _ayId;

        public FormStreamLessonRecord(IStreamLessonRecordService service, Guid sId, Guid ayId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _sId = sId;
            _ayId = ayId;
        }

        protected override bool LoadComponents()
        {
            if (_sId == null)
            {
                MessageBox.Show("Неуказан поток", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var resultSL = _service.GetStreamLessons(new StreamLessonGetBindingModel { AcademicYearId = _ayId });
            if (!resultSL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке потоков возникла ошибка: ", resultSL.Errors);
                return false;
            }

            var resultAP = _service.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = _ayId });
            if (!resultAP.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных планов возникла ошибка: ", resultAP.Errors);
                return false;
            }

            comboBoxStreamLesson.ValueMember = "Value";
            comboBoxStreamLesson.DisplayMember = "Display";
            comboBoxStreamLesson.DataSource = resultSL.Result.List
                .Select(x => new { Value = x.Id, Display = x.StreamLessonName }).ToList();
            comboBoxStreamLesson.SelectedValue = _sId;

            comboBoxAcademicPlan.ValueMember = "Value";
            comboBoxAcademicPlan.DisplayMember = "Display";
            comboBoxAcademicPlan.DataSource = resultAP.Result.List
                .Select(x => new { Value = x.Id, Display = string.Format("{0}. {1} курсы", x.EducationDirection, x.AcademicCoursesStrings) }).ToList();
            comboBoxAcademicPlan.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetStreamLessonRecord(new StreamLessonRecordGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxStreamLesson.SelectedValue = entity.StreamLessonId;
            comboBoxAcademicPlan.SelectedValue = entity.AcademicPlanId;
            comboBoxAcademicPlanRecord.SelectedValue = entity.AcademicPlanRecordId;
            comboBoxAcademicPlanRecordElement.SelectedValue = entity.AcademicPlanRecordElementId;
            checkBoxIsMain.Checked = entity.IsMain;
        }

        protected override bool CheckFill()
        {
            if (comboBoxStreamLesson.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxAcademicPlanRecordElement.SelectedValue == null)
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
                result = _service.CreateStreamLessonRecord(new StreamLessonRecordSetBindingModel
                {
                    StreamLessonId = new Guid(comboBoxStreamLesson.SelectedValue.ToString()),
                    AcademicPlanRecordElementId = new Guid(comboBoxAcademicPlanRecordElement.SelectedValue.ToString()),
                    IsMain = checkBoxIsMain.Checked
                });
            }
            else
            {
                result = _service.UpdateStreamLessonRecord(new StreamLessonRecordSetBindingModel
                {
                    Id = _id.Value,
                    StreamLessonId = new Guid(comboBoxStreamLesson.SelectedValue.ToString()),
                    AcademicPlanRecordElementId = new Guid(comboBoxAcademicPlanRecordElement.SelectedValue.ToString()),
                    IsMain = checkBoxIsMain.Checked
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

        private void ComboBoxAcademicPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicPlan.SelectedValue != null)
            {
                Guid apId = new Guid(comboBoxAcademicPlan.SelectedValue.ToString());
                var resultAPR = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = apId });
                if (!resultAPR.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке записей учебных планов возникла ошибка: ", resultAPR.Errors);
                    return;
                }

                comboBoxAcademicPlanRecord.ValueMember = "Value";
                comboBoxAcademicPlanRecord.DisplayMember = "Display";
                comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
                    .Select(x => new { Value = x.Id, Display = x.Disciplne }).ToList();
                comboBoxAcademicPlanRecord.SelectedItem = null;
            }
        }

        private void ComboBoxAcademicPlanRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicPlanRecord.SelectedValue != null)
            {
                Guid aprId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString());
                var resultAPRE = _service.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = aprId });
                if (!resultAPRE.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке нагрузок записей учебных планов возникла ошибка: ", resultAPRE.Errors);
                    return;
                }
                comboBoxAcademicPlanRecordElement.ValueMember = "Value";
                comboBoxAcademicPlanRecordElement.DisplayMember = "Display";
                comboBoxAcademicPlanRecordElement.DataSource = resultAPRE.Result.List
                    .Select(x => new { Value = x.Id, Display = x.KindOfLoadName }).ToList();
                comboBoxAcademicPlanRecordElement.SelectedItem = null;
            }
        }
    }
}